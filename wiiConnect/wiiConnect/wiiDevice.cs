using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.HumanInterfaceDevice;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace wiiConnect
{
    class wiiDevice
    {
        private HidDevice device;
        private MainPage rootPage;
        private UInt32 numInputReportEeventsReceived;
        private UInt32 totalNumberBytesReceived;
        public TypedEventHandler<HidDevice, HidInputReportReceivedEventArgs> inputReportHandler;


        public wiiDevice(HidDevice _device, MainPage _rootPage)
        {
            this.device = _device;
            this.rootPage = _rootPage;
            this.numInputReportEeventsReceived = 0;

            registerEventHandlers();
        }

        private void registerEventHandlers()
        {
            this.inputReportHandler = new TypedEventHandler<HidDevice, HidInputReportReceivedEventArgs>(onInputReportReceived);

            this.device.InputReportReceived += this.inputReportHandler;
        }

        public void onInputReportReceived(HidDevice sender, HidInputReportReceivedEventArgs args)
        {
            numInputReportEeventsReceived++;
            HidInputReport inputReport = args.Report;
            IBuffer buffer = inputReport.Data;
            totalNumberBytesReceived += buffer.Length;

            Debug.WriteLine(numInputReportEeventsReceived);
            string text = "Total number of input report events received: " + numInputReportEeventsReceived.ToString()
                            + "\nTotal number of bytes received: " + totalNumberBytesReceived.ToString();

            rootPage.UpdateDeviceOutput(text);
        }
    }
}
