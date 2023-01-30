using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlickrDesktop.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FlickrDesktop.ViewModels
{

    public partial class PhotoFeedViewPageModel : ObservableObject
    {
        #region Fields
        private const string FlickrURL = "http://api.flickr.com/services/feeds/photos_public.gne?tags={0}&tagmode=any&format=json";        
        public Navigation<string> navigation = new Navigation<string>();
        SearchParameters searchParameters = new SearchParameters();
        const string API = "Your - API - Key - Goes - Here";
        private const string FlickrUrlWithAPI = "https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&format=json&nojsoncallback=1";

        [ObservableProperty]
        bool canPressNext;

        [ObservableProperty]        
        bool canPressPrevious;

        [ObservableProperty]
        bool canPressNextPage;

        [ObservableProperty]
        bool canPressPreviousPage;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private List<BitmapHelper> listViewfeedData;



        // for back and next page buttons, to enable the functionality, we need to have the following
        //current page number
        //total number of pages

        private int currentPage =0;
        //public int CurrentPage => currentPage;
        private int totalPages = 0;


        #endregion Fields        


        #region methods

        /// <summary>
        /// This method helps in enabling and disabling of search navigation buttons
        /// </summary>
        public void updateSearchNavigateButtons()
        {
            CanPressNext = navigation.CanGoForward;
            CanPressPrevious = navigation.CanGoBack;
            CanPressNextPage = totalPages > 1;
            CanPressPreviousPage = currentPage > 1;
        }

        /// <summary>
        /// This method helps in parsing the response from Flickr API and loads images to a list
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<List<BitmapHelper>> LoadImages(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            var list = new List<BitmapHelper>();
            try
            {
                string responseString = new WebClient().DownloadString(url);
                dynamic response = JsonConvert.DeserializeObject(responseString);
                currentPage = response.photos.page;
                totalPages= response.photos.pages;
                foreach (var photo in response.photos.photo)
                {
                    string title = photo.title;
                    string photoUrl = $"https://farm{photo.farm}.staticflickr.com/{photo.server}/{photo.id}_{photo.secret}.jpg";

                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnDemand;
                    bmp.DecodePixelWidth = 300; /*dimensions[0]*/
                    bmp.DecodePixelHeight = 300;/*dimensions[1];*/
                    bmp.UriSource = new Uri(photoUrl, UriKind.Absolute);
                    bmp.EndInit();
                    list.Add(new BitmapHelper(bmp));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            updateSearchNavigateButtons();
            return list;
        }

        /// <summary>
        /// Loads Images from public Flickr API to a list after parsing the response from Flickr API
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public List<BitmapHelper> LoadImagesFromPublicAPI(string url)
        {
            var flickrUrl = url;
            if (flickrUrl == null) { flickrUrl = "http://api.flickr.com/services/feeds/photos_public.gne?tags=&tagmode=any&format=json"; }
            List<BitmapHelper> list = new List<BitmapHelper>();
            try
            {
                using (var webClient = new HttpClient())
                {
                    var res = webClient.GetStringAsync(flickrUrl);
                    var json = res.Result; 
                    string responseBody = json[json.IndexOf("{")..];

                    string feedText = responseBody.Remove(responseBody.LastIndexOf(")"));
                    dynamic data = JsonConvert.DeserializeObject(feedText);

                    foreach (var item in data.items)
                    {
                        dynamic str = JsonConvert.DeserializeObject(item.ToString());
                        BitmapImage bmp = new BitmapImage();
                        bmp.BeginInit();
                        //var dimensions = GetWidthHeight(str["description"].ToString());
                        bmp.CacheOption = BitmapCacheOption.OnDemand;
                        bmp.DecodePixelWidth = 300; /*dimensions[0]*/
                        bmp.DecodePixelHeight = 300;/*dimensions[1];*/
                        bmp.UriSource = new Uri(str.media.m.ToString(), UriKind.Absolute);
                        bmp.EndInit();
                        list.Add(new BitmapHelper(bmp));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.Resource.ErrorString);
            }
            return list;
        }

        /// <summary>
        /// Gets the height and width of the minimal image
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<int> GetWidthHeight(string str)
        {

            List<int> dimensions = new List<int>();
            try
            {
                str = str.Substring(str.IndexOf("width"));
                str = str.Substring(0, str.IndexOf("height") + 13);
                string width = str.Substring(str.IndexOf('=') + 2, 3);
                string height = str.Substring(str.LastIndexOf('=') + 2, 3);
                dimensions.Add(int.Parse(width, NumberStyles.AllowThousands, new CultureInfo("en-au")));
                dimensions.Add(int.Parse(height, NumberStyles.AllowThousands, new CultureInfo("en-au")));
            }
            catch(Exception ex)
            {
                dimensions.Add(300);
                dimensions.Add(300);
                //MessageBox.Show(ex.Message, Resource.Resource.ErrorString);
            }
            return dimensions;
        }

        [RelayCommand]
        public async void SearchButtonAction()
        {
            //string url = string.Format(FlickrURL, this.SearchText);
            //ListViewfeedData = LoadImagesFromPublicAPI(url);
            string requestUrl = string.Format(FlickrUrlWithAPI, API, searchText);
            ListViewfeedData = await LoadImages(requestUrl);
            navigation.ExecuteAction(requestUrl);
            updateSearchNavigateButtons();
        }

        [RelayCommand]
        public void ClearButtonAction()
        {
            SearchText= string.Empty;
        }

        [RelayCommand]
        public async void NextButtonAction()
        {
            if (navigation.CanGoForward)
            {
                navigation.GoForward(); 
                SetSearchTextBoxText(navigation?.CurrentAction);
            }
            updateSearchNavigateButtons();
        }

        async void SetSearchTextBoxText(string url)
        {
            if (string.IsNullOrEmpty(url)) 
            {
                ListViewfeedData = new List<BitmapHelper>();
                currentPage = 0;
                totalPages = 0;
                SearchText = "";
                return; 
            }
            try
            {
                Uri uri = new Uri(url);
                NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
                SearchText = query.Get("text");
                ListViewfeedData = await LoadImages(url);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Search Text Error");
            }            
        }

        [RelayCommand]
        public async void PrevButtonAction()
        {
            if (navigation.CanGoBack)
            {
                navigation.GoBack();
                SetSearchTextBoxText(navigation?.CurrentAction);
            }
            updateSearchNavigateButtons();
        }

        [RelayCommand]
        public void ItemClickAction(string imageUrl)
        {
            try
            {
                string uri = imageUrl;
                
                //the commented section is for enlarging images from public api
                //int lastindex = uri.LastIndexOf('m');
                //uri = uri.Remove(lastindex, 1);
                //uri = uri.Insert(lastindex, "b");

                EnlargedImage enlargedImage = new EnlargedImage();                
                enlargedImage.Title = uri.Substring(uri.LastIndexOf('/') + 1);
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnDemand;
                bmp.UriSource = new Uri(uri, UriKind.Absolute);
                bmp.EndInit();
                enlargedImage.ExpandedImage.Source = bmp;
                enlargedImage.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Resource.Resource.ErrorString);
            }
            
        }

        [RelayCommand]
        public async void NextPageAction()
        {
            try
            {
                string url = string.Format(FlickrUrlWithAPI, API, searchText);
                currentPage++;
                string temp = string.Format("&page={0}", currentPage);
                url = url + temp;
                ListViewfeedData = await LoadImages(url);
                updateSearchNavigateButtons();
            }
            catch
            {
                MessageBox.Show("Error while Next Page Action", Resource.Resource.ErrorString);
            }
            
        }

        [RelayCommand]
        public async void PrevPageAction()
        {
            try
            {
                string url = string.Format(FlickrUrlWithAPI, API, searchText);
                currentPage--;
                string temp = string.Format("&page={0}", currentPage);
                url = url + temp;
                ListViewfeedData = await LoadImages(url);
                updateSearchNavigateButtons();
            }
            catch
            {
                MessageBox.Show("Error while Next Page Action", Resource.Resource.ErrorString);
            }            
        }
        
        #endregion

    }
}
