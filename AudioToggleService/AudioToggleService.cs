using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NAudio;

namespace AudioToggleService
{
    public partial class AudioToggleService : ServiceBase
    {
        CoreAudioController Controller { get; set; }
        List<string> AudioDeviceNamesToCycle = new List<string>() {"Yeti Classic", /*"Yeti Stereo Microphone", */ "Razer Nari - Chat" };
        const string EventSourceName = "AudioToggleSource";
        const string EventLogName = "AudioToggleLog";
        EventLog eventLog { get; set; }
        private int eventId = 1;

        public AudioToggleService()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            // Create the source, if it does not already exist.
            if (!EventLog.SourceExists(EventSourceName))
            {
                //An event log source should not be created and immediately used.
                //There is a latency time to enable the source, it should be created
                //prior to executing the application that uses the source.
                //Execute this sample a second time to use the new source.
                EventLog.CreateEventSource(EventSourceName, EventLogName);
                Console.WriteLine("CreatedEventSource");
                Console.WriteLine("Exiting, execute the application a second time to use the source.");
                // The source is created.  Exit the application to allow it to be registered.
            }

            // Create an EventLog instance and assign its source.
            eventLog = new EventLog();
            eventLog.Source = EventSourceName;
            eventLog.Log = EventLogName;

            // Write an informational entry to the event log.
            eventLog.WriteEntry("Initializing service for custom audio toggle.", EventLogEntryType.Information, eventId++);

            Controller = new CoreAudioController();
            List<CoreAudioDevice> allDevices = Controller.GetDevices().ToList();

            eventLog.WriteEntry($"All available devices: {string.Join(", ", allDevices.Select(x => x.InterfaceName)).Trim()}.", EventLogEntryType.Information, eventId++);
        }

        protected override void OnCustomCommand(int command)
        {
            eventLog.WriteEntry($"Running custom command {command}.", EventLogEntryType.Information, eventId++);
            switch (command)
            {
                case (int)Command.SetAudio:
                    SetNextRecordingDevice();
                    break;
                default:
                    break;
            }
        }

        public void SetNextRecordingDevice()
        {
            eventLog.WriteEntry($"SetNextRecordingDevice executing.", EventLogEntryType.Information, eventId++);

            try
            {
                List<CoreAudioDevice> allDevices = Controller.GetDevices().ToList();
                List<CoreAudioDevice> devices = allDevices.Where(
                    x =>
                    x.IsCaptureDevice &&
                    AudioDeviceNamesToCycle.Any(y => x.InterfaceName.ToLower().Contains(y.ToLower()) || y.Equals(x.InterfaceName, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                CoreAudioDevice activeDevice = Controller.DefaultCaptureDevice;
                int currDeviceIndex = devices.Select(x => x.FullName).ToList().IndexOf(activeDevice.FullName);

                int nextDeviceIndex = 0;
                if (currDeviceIndex == -1 || currDeviceIndex >= devices.Count - 1)
                {
                    nextDeviceIndex = 0;
                }
                else
                {
                    nextDeviceIndex = currDeviceIndex + 1;
                }

                eventLog.WriteEntry($"Current Device Index: {currDeviceIndex}. Next Device Index: {nextDeviceIndex}.", EventLogEntryType.Information, eventId++);

                CoreAudioDevice newDevice = devices[nextDeviceIndex];


                using (var waveOut = new NAudio.Wave.WaveOutEvent())
                using (var wavReader = new NAudio.Wave.WaveFileReader(@"C:\Windows\Media\notify.wav"))
                {
                    waveOut.Init(wavReader);
                    waveOut.Play();
                }

                eventLog.WriteEntry($"Using {newDevice.InterfaceName}", EventLogEntryType.Information, eventId++);
                newDevice.SetAsDefault();
                newDevice.SetAsDefaultCommunications();
            }
            catch(Exception ex)
            {
                eventLog.WriteEntry($"Error: {ex.Message}. {Environment.NewLine}{Environment.NewLine}Stack Trace: {ex.StackTrace}.", EventLogEntryType.Error, eventId++);
            }
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);



            // Set up a timer that triggers every minute.
            Timer timer = new Timer();
            timer.Interval = 60000 * 10; // 60 seconds * 10 = 10 minutes
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();



            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        /// <summary>
        /// Monitoring activities here to refresh the core controller context.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            Controller = new CoreAudioController();
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Stopping service", EventLogEntryType.Information, eventId++);
            Controller.Dispose();
        }

        /// <summary>
        /// The Service Control Manager uses the dwWaitHint and dwCheckpoint members of the SERVICE_STATUS structure to determine
        /// how much time to wait for a Windows service to start or shut down. If your OnStart and OnStop methods run long, 
        /// your service can request more time by calling SetServiceStatus again with an incremented dwCheckPoint value.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="serviceStatus"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
    }
    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };
}
