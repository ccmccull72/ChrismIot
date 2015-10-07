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
using PiGpio;
using Windows.UI;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShowPinValuesUi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Dictionary<string, TextBlock> uiGpioMap;

        public MainPage()
        {
            this.InitializeComponent();

            uiGpioMap = new Dictionary<string, TextBlock>
            {
                {"GPIO_5", gpio5TextBlock},
                {"GPIO_6", gpio6TextBlock},
                {"GPIO_12", gpio12TextBlock},
                {"GPIO_13", gpio13TextBlock},
                {"GPIO_16", gpio16TextBlock},
                {"GPIO_18", gpio18TextBlock},
                {"GPIO_22", gpio22TextBlock},
                {"GPIO_23", gpio23TextBlock},
                {"GPIO_24", gpio24TextBlock},
                {"GPIO_25", gpio25TextBlock},
                {"GPIO_26", gpio26TextBlock},
                {"GPIO_27", gpio27TextBlock}
            };

            ShowPinValues();
        }

        public void ShowPinValues()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var pinValues = Pi2Gpio.GetPinValues();
                    UpdatePinValues(pinValues);
                    Task.Delay(500);
                }
            });
        }

        public void UpdatePinValues(Dictionary<string, GpioPinValue> pinValues)
        {
            var t = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                foreach (var key in uiGpioMap.Keys)
                {
                    uiGpioMap[key].Text = pinValues[key].ToString();
                    if (pinValues[key].ToString().ToLower() == "low")
                    {
                        uiGpioMap[key].Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        uiGpioMap[key].Foreground = new SolidColorBrush(Colors.Green);
                    }
                }
            });
        }

        private void textBlock_Copy_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void gpio27TextBlock_Copy2_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
