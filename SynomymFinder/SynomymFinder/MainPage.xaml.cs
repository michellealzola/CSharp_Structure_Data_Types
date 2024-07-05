using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SynomymFinder
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Dictionary<string, List<string>> wordSynonyms = new Dictionary<string, List<string>>();
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
                ListView_Words.Items.Add("Please enter a word to search.");
            }
        }

        private async Task ReadFileAsync(StorageFile file)
        {
            wordSynonyms.Clear();

            string[] lines = (await FileIO.ReadLinesAsync(file)).ToArray();

            foreach (string line in lines)
            {
                string[] tokens = line.Split(':');
                if (tokens.Length == 2)
                {
                    string word = tokens[0].Trim();
                    List<string> synomyms = new List<string>();
                    synomyms.Add(tokens[1]);

                    wordSynonyms[word] = synomyms;
                }
            }
        }

        private void Button_SearchSynonym_Click(object sender, RoutedEventArgs e)
        {
            string wordTarget = InputTargetWord.Text.Trim().ToLower();

            ListView_Words.Items.Clear();

            foreach (string word in wordSynonyms.Keys)
            {
                if (wordTarget == word)
                {
                    ListView_Words.Items.Add($"{word}:");
                    foreach (var synonyms in wordSynonyms[word])
                    {
                        ListView_Words.Items.Add($"{synonyms}");
                        return;
                    }
                }
                else
                {
                    ListView_Words.Items.Add("No synonyms found.");
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().Title = "Synonym Finder";
            ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = Colors.AliceBlue;
            ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveBackgroundColor = Colors.AliceBlue;
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.AliceBlue;
            ListView_Words.Items.Add("Please Open a Text File.");
            
        }
    }
}
