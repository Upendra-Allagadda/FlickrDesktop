using FlickrDesktop.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System;
using System.Threading;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Policy;
using System.Windows.Media;
using System.IO;
using System.Windows.Controls;
using System.Globalization;
using FlickrDesktop.Helper;
using static System.Net.Mime.MediaTypeNames;

namespace FlickrDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PhotoFeedViewPageModel photoFeedViewPageModel;
        public MainWindow()
        {
            InitializeComponent();
            photoFeedViewPageModel= new PhotoFeedViewPageModel();
            this.DataContext = photoFeedViewPageModel;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            photoFeedViewPageModel.ItemClickAction(((System.Windows.Controls.Image)sender).Source.ToString());                
        }
    }    
}
