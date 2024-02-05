using AudioToggleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Media;
using NAudio.Wave;
using System.Threading;
using System.Runtime.InteropServices;
using System.Management;
using NAudio.CoreAudioApi;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

namespace AudioToggle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string serviceName = "AudioToggleService";
            Dictionary<string, string> DeviceSoundDictionary = new Dictionary<string, string>()
            {
                /*{ "Yeti Classic", @"C:\Windows\Media\Speech On.wav" },
                { "Razer Nari - Chat", @"C:\Windows\Media\Speech Off.wav" }*/
                // We reverse these, because the audio device will not have switched till after the sound bits play. So we need to play these in reverse.
                { "Yeti Classic", @"C:\Windows\Media\Speech Off.wav"},
                { "Razer Nari - Chat", @"C:\Windows\Media\Speech On.wav"  }
            };

            string targetSound = @"C:\Windows\Media\Windows Default.wav";

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            var currAudioDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
            enumerator.Dispose();

            var targetPair = DeviceSoundDictionary.FirstOrDefault(x => x.Key.Contains(currAudioDevice.DeviceFriendlyName) || currAudioDevice.DeviceFriendlyName.Contains(x.Key));
            if(!string.IsNullOrEmpty(targetPair.Key))
            {
                targetSound = targetPair.Value;
            }

            SoundPlayer sound = new SoundPlayer(targetSound);
            sound.SoundLocation = targetSound;
            sound.Play();

            ServiceController sc = new ServiceController(serviceName, Environment.MachineName);

            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                if (!IsRunAsAdmin())
                {
                    ProcessStartInfo proc = new ProcessStartInfo();
                    proc.UseShellExecute = true;
                    proc.WorkingDirectory = Environment.CurrentDirectory;
                    proc.FileName = Assembly.GetEntryAssembly().CodeBase;

                    proc.Verb = "runas";

                    try
                    {
                        Process.Start(proc);
                        return;// Application.Current.Shutdown();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("This program must be run as an administrator! \n\n" + ex.ToString());
                    }
                }
                else
                {
                    sc.Start();
                }
            }

            sc.WaitForStatus(ServiceControllerStatus.Running);

            ServiceControllerPermission scp = new ServiceControllerPermission(ServiceControllerPermissionAccess.Control, Environment.MachineName, serviceName);//this will grant permission to access the Service
            scp.Assert();
            sc.Refresh();

            sc.ExecuteCommand((int)Command.SetAudio);

            // Hide
            ShowWindow(GetConsoleWindow(), SW_HIDE);
            Thread.Sleep(1000);
        }


        private static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void PlayExamples()
        {
            // Declare the first few notes of the song, "Mary Had A Little Lamb".
            Note[] Mary =
            {
                new Note(Tone.B, Duration.QUARTER),
                new Note(Tone.A, Duration.QUARTER),
                new Note(Tone.GbelowC, Duration.QUARTER),
                new Note(Tone.A, Duration.QUARTER),
                new Note(Tone.B, Duration.QUARTER),
                new Note(Tone.B, Duration.QUARTER),
                new Note(Tone.B, Duration.HALF),
                new Note(Tone.A, Duration.QUARTER),
                new Note(Tone.A, Duration.QUARTER),
                new Note(Tone.A, Duration.HALF),
                new Note(Tone.B, Duration.QUARTER),
                new Note(Tone.D, Duration.QUARTER),
                new Note(Tone.D, Duration.HALF)
            };
            // Play the song
            //Play(Mary);
            Task t = new Task(() =>
            {
            });
            //t.Start();
            Play(new Note[]
            {
                    new Note(Tone.A, Duration.EIGHTH) ,
                    new Note(Tone.B, Duration.EIGHTH) ,
                    new Note(Tone.A, Duration.QUARTER) ,
                    new Note(Tone.GbelowC, Duration.QUARTER) ,
                    new Note(Tone.FSharpBelowG, Duration.QUARTER) ,
            });
            //Play(new Note[] { new Note(Tone.A, Duration.SIXTEENTH)});
        }

        // Play the notes in a song.
        protected static void Play(Note[] tune)
        {
            foreach (Note n in tune)
            {
                if (n.NoteTone == Tone.REST)
                    Thread.Sleep((int)n.NoteDuration);
                else
                    Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
            }
        }

        // Define the frequencies of notes in an octave, as well as
        // silence (rest).
        protected enum Tone
        {
            REST = 0,
            FSharpBelowG = 183,
            GbelowC = 196,
            A = 220,
            Asharp = 233,
            B = 247,
            C = 262,
            Csharp = 277,
            D = 294,
            Dsharp = 311,
            E = 330,
            F = 349,
            Fsharp = 370,
            G = 392,
            Gsharp = 415,
        }

        // Define the duration of a note in units of milliseconds.
        protected enum Duration
        {
            WHOLE = 1600,
            HALF = WHOLE / 2,
            QUARTER = HALF / 2,
            EIGHTH = QUARTER / 2,
            SIXTEENTH = EIGHTH / 2,
            THIRTYSECOND = SIXTEENTH / 2,
        }

        // Define a note as a frequency (tone) and the amount of
        // time (duration) the note plays.
        protected struct Note
        {
            Tone toneVal;
            Duration durVal;

            // Define a constructor to create a specific note.
            public Note(Tone frequency, Duration time)
            {
                toneVal = frequency;
                durVal = time;
            }

            // Define properties to return the note's tone and duration.
            public Tone NoteTone { get { return toneVal; } }
            public Duration NoteDuration { get { return durVal; } }
        }
    }
}