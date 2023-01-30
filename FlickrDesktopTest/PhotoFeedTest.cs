using FlickrDesktop.ViewModels;

namespace FlickrDesktopTest
{
    public class PhotoFeedTest
    {
        #region fields
        PhotoFeedViewPageModel photoFeedViewPageModel;
        List<string> phototags = new List<string>{ "car", "text", "cab" };
        const string API = "Your - API - Key - Goes - Here";
        private const string FlickrURL = "http://api.flickr.com/services/feeds/photos_public.gne?tags={0}&tagmode=any&format=json";
        private const string FlickrUrlWithAPI = "https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&text={1}&format=json&nojsoncallback=1";
        #endregion 

        [SetUp]
        public void Setup()
        {
            photoFeedViewPageModel = new PhotoFeedViewPageModel();
        }

        void NavigationSetup()
        {
            photoFeedViewPageModel = new PhotoFeedViewPageModel();
            foreach (string str in phototags)
            {
                var url = string.Format(FlickrURL, str);
                var list = photoFeedViewPageModel.LoadImagesFromPublicAPI(url);
                photoFeedViewPageModel.navigation.ExecuteAction(url);
            }
        }

        [Test]
        public void BackButtonTest()
        {
            NavigationSetup();
            Assert.That(photoFeedViewPageModel.navigation.CanGoBack, Is.True);
        }

        [Test]
        public void NextButtonTest()
        {

            NavigationSetup();
            photoFeedViewPageModel.navigation.GoBack();
            Assert.That(photoFeedViewPageModel.navigation.CanGoForward, Is.True);
        }

        [Test]
        public void LoadImageTestPublicAPI()
        {
            var list = photoFeedViewPageModel.LoadImagesFromPublicAPI(FlickrURL);
            Assert.That(list, Is.Not.Null);
        }

        [Test]
        public void EmptyURLTest()
        {
            Assert.IsNull(photoFeedViewPageModel.LoadImages("").Result);
        }

        [Test]
        public void LoadImageTest()
        {
            Assert.IsNotNull(photoFeedViewPageModel.LoadImages(String.Format(FlickrUrlWithAPI, API, "bat")));
        }
    }
}