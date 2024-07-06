using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Casting;
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

namespace StudentGrades
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<(string Name, double AverageGrade)> studentAverages = new List<(string, double)>();
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            ListView_TopFiveStudents.Items.Clear();

            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            };

            picker.FileTypeFilter.Add(".txt");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null )
            {
                await ReadFileAsync(file);
                ListView_TopFiveStudents.Items.Add("Please click the Display Top 5 Students");
            }
        }

        private async Task ReadFileAsync(StorageFile file)
        {
            

            using (Stream fileStream = await file.OpenStreamForReadAsync())
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] tokens = line.Split(':');
                    if (tokens.Length == 2)
                    {
                        string name = tokens[0].Trim();
                        string[] grades = tokens[1].Split(",");
                        
                        double total = 0;

                        foreach (string grade in grades)
                        {
                            total += double.Parse(grade);
                        }

                        double average = total / grades.Length;

                        studentAverages.Add((name, average));

                    }
                }
            }

            
        }

        private void Button_DisplayTopFive_Click(object sender, RoutedEventArgs e)
        {
            var topFiveStudents = studentAverages.OrderByDescending(x => x.AverageGrade)
                                                .Take(5)
                                                .ToList();

            foreach (var student in topFiveStudents)
            {
                ListView_TopFiveStudents.Items.Add($"{student.Name}: {student.AverageGrade:F2}");
            }
        }
    }
}
