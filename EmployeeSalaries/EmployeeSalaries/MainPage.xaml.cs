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

namespace EmployeeSalaries
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<(string Name, double AverageSalary)> employeeAverageSalaries = new List<(string, double)>();
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            ListView_LowestSalaries.Items.Clear();

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
                ListView_LowestSalaries.Items.Add("Please click the Display 5 Lowest Salaries Button");
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
                        var name = tokens[0];

                        string[] salaries = tokens[1].Split(',');

                        double total = 0;

                        foreach (var salary in salaries)
                        {
                            total += double.Parse(salary);
                        }

                        double average = total / salaries.Length;   

                        employeeAverageSalaries.Add((name, average));

                        
                    }
                }
            }
        }

        private void Button_DisplayLowestSalaries_Click(object sender, RoutedEventArgs e)
        {
            var fiveLowestSalaries = employeeAverageSalaries.OrderBy(x => x.AverageSalary).Take(5).ToList();

            foreach (var employee in fiveLowestSalaries)
            {
                ListView_LowestSalaries.Items.Add($"{employee.Name}: {employee.AverageSalary:F2}");
            }
        }
    }
}
