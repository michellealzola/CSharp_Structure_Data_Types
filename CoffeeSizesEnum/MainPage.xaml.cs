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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CoffeeSizesEnum
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        enum CupSizes
        {
            small,
            medium,
            large,
        }
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void small_tapped(object sender, TappedRoutedEventArgs e)
        {
            OutputSize.Text = CupSizes.small.ToString();
        }

        private void medium_tapped(object sender, TappedRoutedEventArgs e)
        {
            OutputSize.Text = CupSizes.medium.ToString();
        }

        private void large_tapped(object sender, TappedRoutedEventArgs e)
        {
            OutputSize.Text = CupSizes.large.ToString();
        }
    }
}
