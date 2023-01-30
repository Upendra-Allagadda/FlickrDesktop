using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FlickrDesktop.Helper
{
    public class BitmapHelper
    {
        public BitmapImage image;
        public BitmapImage Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }
        public BitmapHelper(BitmapImage bitmapImage)
        {
            Image = bitmapImage;
        }
    }
}
