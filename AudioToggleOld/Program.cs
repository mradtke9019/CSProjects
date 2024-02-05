using AudioSwitcher;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using CoreAudio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AudioToggle
{
    internal class Program
    {
        static void Main(string[] args)
        {


            var flagArgs = args.Where(x => x.Contains("-")).ToList();
            var flaglessArgs = args.Where(x => !x.Contains("-")).ToList();
            bool recurse = flagArgs.Contains("-r");

            string deviceName1 = string.Empty;
            string deviceName2 = string.Empty;

#if DEBUG
            deviceName1 = "Yeti Stereo Microphone";
            deviceName2 = "Razer Nari - Chat";
#else
            if (flaglessArgs.Count != 2)
            {
                Console.WriteLine("Usage: AudioToggle device1 device2");
                return;
            }
            deviceName1 = flaglessArgs[0];
            deviceName2 = flaglessArgs[1];


#endif
            ToggleDevicesAudioSwitcher(deviceName1, deviceName2);
        }


        public static void ToggleDevicesAudioSwitcher(string deviceName1, string deviceName2)
        {

            CoreAudioController controller = new CoreAudioController();

            List<CoreAudioDevice> devices = controller.GetDevices().ToList();

            CoreAudioDevice device1 = devices.FirstOrDefault(x => x.IsCaptureDevice && x.InterfaceName.Contains(deviceName1));
            CoreAudioDevice device2 = devices.FirstOrDefault(x => x.IsCaptureDevice && x.InterfaceName.Contains(deviceName2));

            var activeDevice = controller.DefaultCaptureDevice;
            CoreAudioDevice newDevice = null;

            if (activeDevice.Id == device1.Id)
            {
                newDevice = device2;
            }
            else
            {
                newDevice = device1;
            }
            Console.WriteLine($"Using {newDevice.InterfaceName}");
            newDevice.SetAsDefault();
            //newDevice.SetAsDefaultCommunications();
        }
    }
}