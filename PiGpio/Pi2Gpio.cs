using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using System.IO;
using Windows.Storage;

namespace PiGpio
{
    public class Pi2Gpio
    {
        public static Dictionary<string, int> Pins = new Dictionary<string, int>
        {
            {"GPIO_5", 5},
            {"GPIO_6", 6},
            {"GPIO_12", 12},
            {"GPIO_13",13},
            {"GPIO_16", 16},
            {"GPIO_18", 18},
            {"GPIO_22", 22},
            {"GPIO_23", 23},
            {"GPIO_24", 24},
            {"GPIO_25", 25},
            {"GPIO_26", 26},
            {"GPIO_27", 27},
            {"GPIO_35", 35},
            {"GPIO_47", 47}

        };

        public async static Task<int> WritePinValues(StorageFile file)
        {
            string result = "";
            result += "==========================================\n";
            result += DateTime.Now + "\n";
            var pins = GetPinValues();
            foreach (var key in pins.Keys)
            {
                result += string.Format("{0} = {1}\n", key, pins[key]);
            }

            await Windows.Storage.FileIO.WriteTextAsync(file, result);

            return 0;
        }

        public static Dictionary<string, string> GetPinValues()
        {
            Dictionary<string, string> pins = new Dictionary<string, string>();

            var gpio = GpioController.GetDefault();
            foreach (var key in Pins.Keys)
            {
                var pin = gpio.OpenPin(Pins[key]);
                pins.Add(key, pin.Read().ToString());
            }

            return pins;
        }

        public static void SetupGpioListeners(Windows.Foundation.TypedEventHandler<GpioPin, GpioPinValueChangedEventArgs> callback)
        {
            var gpio = GpioController.GetDefault();
            foreach (var key in Pins.Keys)
            {
                var pin = gpio.OpenPin(Pins[key]);
                pin.ValueChanged += callback;
            }
        }
    }
}
