using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace WordLengthAnalyzer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Dictionary<string, int> wordLengthDict = new Dictionary<string, int>();
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            ListView_Words.Items.Clear();

            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            };

            picker.FileTypeFilter.Add(".txt");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                await ReadFileAsync(file);
                ListView_Words.Items.Add("Please click the display word length button");
            }
        }

        private async Task ReadFileAsync(StorageFile file)
        {
            wordLengthDict.Clear();

            string[] lines = (await FileIO.ReadLinesAsync(file)).ToArray();

            char[] delim = {' ','.'};

            foreach (string line in lines)
            {              

                string[] tokens = line.Split(delim);

                for (int i = 0; i < tokens.Length; i++)
                {
                    string word = tokens[i].Trim();
                    int wordLength = word.Length;

                    wordLengthDict[word] = wordLength;
                }               
            }
        }

        private void Button_DisplayWordLength_Click(object sender, RoutedEventArgs e)
        {
            ListView_Words.Items.Clear();

            int dictlength = wordLengthDict.Count;
            int total = 0;

            foreach (var element in wordLengthDict)
            {
                total += element.Value;
                ListView_Words.Items.Add($"{element.Key}: {element.Value}");
            }

            double average = total / dictlength;

            Output_AveWordLength.Text = average.ToString("F2");
        }
    }
}
