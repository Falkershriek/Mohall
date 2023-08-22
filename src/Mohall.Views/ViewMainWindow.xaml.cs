using Mohall.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mohall.Views
{
    /// <summary>
    /// Interaction logic for ViewMainWindow.xaml
    /// </summary>
    public partial class ViewMainWindow : Window, ICloseable, IWindowDataContext
    {
        public ViewMainWindow()
        {
            InitializeComponent();
        }
    }

    public class StyleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FrameworkElement targetElement = values[0] as FrameworkElement;
            string styleName = values[1] as string;

            if (styleName == null)
                return null;

            Style newStyle = (Style)targetElement.TryFindResource(styleName);

            if (newStyle == null)
                newStyle = (Style)targetElement.TryFindResource("MyDefaultStyleName");

            return newStyle;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
