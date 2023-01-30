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
using System.Windows.Shapes;

namespace FlickrDesktop
{
    /// <summary>
    /// Interaction logic for Image.xaml
    /// </summary>
    public partial class EnlargedImage : Window
    {
        int maxWidth = 1000;
        int minWidth = 300;
        int imgDelta = 25;
        public EnlargedImage()
        {
            InitializeComponent();
        }

        private void ExpandedImage_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ChangeWidth(e.Delta);
        }

        public void ChangeWidth(int delta)
        {
            if (delta > 0)
            {
                TestImageSlider.Value += imgDelta;
            }
            else
            {
                TestImageSlider.Value -= imgDelta;
            }
        }
    }
}
