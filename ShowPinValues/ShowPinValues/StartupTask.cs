using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using PiGpio;
using Windows.Storage;
using System.Threading.Tasks;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace ShowPinValues
{
    public sealed class StartupTask : IBackgroundTask
    {

        public void Run(IBackgroundTaskInstance taskInstance)
        {

            WritePins();
        }

        public async void WritePins()
        {
            StorageFolder folder =
            Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile pinFile =
                await folder.CreateFileAsync("log.txt", CreationCollisionOption.ReplaceExisting);

            await Pi2Gpio.WritePinValues(pinFile);
        }
    }
}
