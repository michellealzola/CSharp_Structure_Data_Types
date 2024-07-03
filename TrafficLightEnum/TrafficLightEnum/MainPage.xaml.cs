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
using Windows.UI;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TrafficLightEnum
{
    /*Problem:
     * Define an enumeration for traffic light states (Red, Yellow, Green).
     * Create a XAML application with a button to simulate a traffic light, 
     * cycling through the states and displaying the current state.
     */



    public sealed partial class MainPage : Page
    {

        enum TrafficStates
        {
            Red,
            Orange,
            Green
        }

        int click = 0;

        public MainPage()
        {
            this.InitializeComponent();


            CircleColor.Fill = new SolidColorBrush(GetColor(TrafficStates.Red));
            Output_State.Text = GetState(TrafficStates.Red);

        }

        private void ChangeColor_Click(object sender, RoutedEventArgs e)
        {
            click++;



            if (click == (int)TrafficStates.Red)
            {
                CircleColor.Fill = new SolidColorBrush(GetColor(TrafficStates.Red));
                Output_State.Text = GetState(TrafficStates.Red);

            }
            else if (click == (int)TrafficStates.Orange)
            {
                CircleColor.Fill = new SolidColorBrush(GetColor(TrafficStates.Orange));
                Output_State.Text = GetState(TrafficStates.Orange);

            }
            else if (click == (int)TrafficStates.Green)
            {
                CircleColor.Fill = new SolidColorBrush(GetColor(TrafficStates.Green));
                Output_State.Text = GetState(TrafficStates.Green);

            }
            else
            {
                click = 0;
                CircleColor.Fill = new SolidColorBrush(GetColor(TrafficStates.Red));
                Output_State.Text = GetState(TrafficStates.Red);

            }


        }

        private Color GetColor(TrafficStates state)
        {
            switch (state)
            {
                case TrafficStates.Red:
                    return Colors.Red;
                case TrafficStates.Orange:
                    return Colors.Orange;
                case TrafficStates.Green:
                    return Colors.Green;
                default:
                    return Colors.Red;

            }
        }

        private string GetState(TrafficStates state)
        {
            switch (state)
            {
                case TrafficStates.Red:
                    return "STOP";
                case TrafficStates.Orange:
                    return "WAIT";
                case TrafficStates.Green:
                    return "GO";
                default:
                    return "STOP";
            }
        }

    }
}
