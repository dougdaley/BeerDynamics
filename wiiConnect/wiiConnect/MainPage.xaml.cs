using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.HumanInterfaceDevice;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.Storage;
using System.Diagnostics;
using Windows.Storage.Streams;
using Windows.UI.Core;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace wiiConnect
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {        
        private HidDevice device;
        private wiiDevice wiiDevice;
        //private MainPage rootPage = MainPage.

        public MainPage()
        {
            this.InitializeComponent();

            this.connectToDevice();
            
        }

        private async void connectToDevice()
        {
            string selector = HidDevice.GetDeviceSelector(0x001, 0x005, 0x057e, 0x0306);
            Debug.WriteLine("Looking for devices");
            var devices = await DeviceInformation.FindAllAsync(selector);
            Debug.WriteLine(string.Format("Devices found: {0}", devices.Count));
            if(devices.Count > 0)
            {
                Debug.WriteLine(devices.ElementAt(0).Id);
                
                device = await HidDevice.FromIdAsync(devices.ElementAt(0).Id, FileAccessMode.ReadWrite);
                //device.
                if (device != null)
                {
                    this.wiiDevice = new wiiDevice(device, this);
                }
                
                //device.InputReportReceived +=  wiiDevice.inputReportHandler;
            }

           // return await HidDevice.FromIdAsync(devices.ElementAt(0).Id, FileAccessMode.Read);
        }

        internal async void UpdateDeviceOutput(string text)
        {
            await this.ReportOutput.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    new DispatchedHandler(() =>
                    {
                        // If we navigated away from this page, do not print anything. The dispatch may be handled after
                        // we move to a different page.
                        this.ReportOutput.Text = text;
                    }));
        }


    }
}
