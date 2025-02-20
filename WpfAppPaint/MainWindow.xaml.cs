using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppPaint;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool isDrawing;
    private Point startPoint;
    private Line? currentLine; // Allow currentLine to be nullable
    private Brush selectedColor = Brushes.Black; // Default color

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            isDrawing = true;
            startPoint = e.GetPosition(paintCanvas);

            currentLine = new Line
            {
                Stroke = selectedColor,
                StrokeThickness = 2,
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = startPoint.X,
                Y2 = startPoint.Y
            };

            paintCanvas.Children.Add(currentLine);
        }
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDrawing && currentLine != null)
        {
            Point currentPoint = e.GetPosition(paintCanvas);
            currentLine.X2 = currentPoint.X;
            currentLine.Y2 = currentPoint.Y;
        }
    }

    private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Released)
        {
            isDrawing = false;
            currentLine = null;
        }
    }

    private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
    {
        if (e.NewValue.HasValue)
        {
            selectedColor = new SolidColorBrush(e.NewValue.Value);
        }
    }
}
