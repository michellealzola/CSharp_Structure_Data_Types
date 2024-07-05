using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WordFrequencyCounter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            };

            picker.FileTypeFilter.Add(".txt");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                string fileContent = await FileIO.ReadTextAsync(file);

                var wordFrequency = CountWordFrequency(fileContent);

                WordFrequencyListView.ItemsSource = wordFrequency.ToList();
            }
        }

        private Dictionary<string, int> CountWordFrequency(string text)
        {
            var wordFrequency = new Dictionary<string, int>();

            string normalizedText = Regex.Replace(text.ToLower(), @"\W+", " ");

            var words = normalizedText.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach ( var word in words )
            {
                if ( wordFrequency.ContainsKey(word.ToLower()))
                {
                    wordFrequency[word.ToLower()]++;
                }
                else
                {
                    wordFrequency[word.ToLower()] = 1;
                }
            }

            return wordFrequency
                .OrderByDescending(pair => pair.Value)
                .Take(10)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
