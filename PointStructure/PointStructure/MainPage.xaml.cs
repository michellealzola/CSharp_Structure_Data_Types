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

namespace PointStructure
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(Point other)
        {
            double deltaX = X - other.X;
            double deltaY = Y - other.Y;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void CalculateDistance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x1 = double.Parse(Point1X.Text);
                double y1 = double.Parse(Point1Y.Text);

                double x2 = double.Parse(Point2X.Text);
                double y2 = double.Parse(Point2Y.Text);

                Point point1 = new Point(x1, y1);
                Point point2 = new Point(x2, y2);

                double distance = point1.DistanceTo(point2);

                ResultTextBlock.Text = $"Distance: {distance:F2}";

                DrawPointsAndLine(point1, point2);
            }
            catch (FormatException)
            {
                ResultTextBlock.Text = "Please enter valid numbers for the coordinates.";
            }
        }

        private void DrawPointsAndLine(Point point1, Point point2)
        {
            //DrawingCanvas.Children.Clear();

            double scale = 10;
            double offsetX = 50;
            double offsetY = 50;

            double canvasX1 = point1.X * scale + offsetX;
            double canvasY1 = point1.Y * scale + offsetY;

            double canvasX2 = point2.X * scale + offsetX;
            double canvasY2 = point2.Y * scale + offsetY;

            DrawPoint(canvasX1, canvasY1, Colors.Red);
            DrawPoint(canvasX2, canvasY2, Colors.Blue);

            Line line = new Line 
            {
                X1 = canvasX1,
                Y1 = canvasY1,
                X2 = canvasX2,
                Y2 = canvasY2,
                Stroke = new SolidColorBrush(Colors.Green),
                StrokeThickness = 2
            };

            DrawingCanvas.Children.Add(line);
            
        }

        private void DrawPoint(double x, double y, Color color)
        {
            Ellipse ellipse = new Ellipse 
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(color)
            };

            Canvas.SetLeft(ellipse, x - ellipse.Width / 2);
            Canvas.SetTop(ellipse, y - ellipse.Height / 2);
            DrawingCanvas.Children.Add(ellipse);

        }
    }
}
