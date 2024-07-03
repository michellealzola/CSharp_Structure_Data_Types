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
using Windows.UI.Xaml.Shapes;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RectangleStructure
{
    /* Problem:
     * Define a Rectangle structure with Length and Width fields.
     * Add methods to calculate the area and perimeter.
     * Create a XAML application to input rectangle dimensions and display the area and perimeter.
     */

    public struct Rectangle2
    {
        public double Length { get; set; }
        public double Width { get; set; }

        public Rectangle2(double length, double width)
        {
            Length = length;
            Width = width;
        }

        public double CalculateArea()
        { 
            return Length * Width; 
        }

        public double CalculatePerimeter()
        { 
            return 2 * Length + 2 * Width; 
        }
    }
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void CalculateArea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double area;
                double width = double.Parse(InputWidth.Text);
                double length = double.Parse(InputLength.Text);

                Rectangle2 rect = GetRectangle(length, width);
                
                area = rect.CalculateArea();

                OutputArea.Text = area.ToString("F2");

                DrawRectangle(rect);
            }
            catch (FormatException)
            {
                OutputArea.Text = "Please enter valid numbers.";
            }
        }

        private void CalculatePerimeter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double perimeter;
                double width = double.Parse(InputWidth.Text);
                double length = double.Parse(InputLength.Text);

                Rectangle2 rect = GetRectangle(length, width);

                perimeter = rect.CalculatePerimeter();

                OutputPerimeter.Text = perimeter.ToString("F2");

            }
            catch (FormatException)
            {
                OutputPerimeter.Text = "Please enter valid numbers.";
            }
        }

        private Rectangle2 GetRectangle(double length, double width)
        {
            Rectangle2 rect = new Rectangle2
            {
                Length = length,
                Width = width,
            };

            return rect;
        }

        private void DrawRectangle(Rectangle2 rect)
        {
            DrawingCanvas.Children.Clear();

            Rectangle rectangleShape = new Rectangle
            {
                Width = rect.Width,
                Height = rect.Length,
                Stroke = new SolidColorBrush(Colors.Chocolate),
                Fill = new SolidColorBrush(Colors.BurlyWood),
                StrokeThickness = 3
            };

            Canvas.SetLeft(rectangleShape, 2);
            Canvas.SetTop(rectangleShape, 2);

            DrawingCanvas.Children.Add(rectangleShape);
        }
    }
}
