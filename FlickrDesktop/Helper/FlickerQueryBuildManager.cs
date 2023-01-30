using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrDesktop.Helper
{
    internal class FlickerQueryBuildManager
    {
        #region Private Properties

        private FlickrRestQueryBuilder _queryBuilder;
        private readonly string _baseAddress;
        private bool _isDisposed;

        #endregion

        #region Public methods

        public FlickrRestQueryBuilder QueryBuilder
        {
            set => _queryBuilder = value;
        }

        public FlickerQueryBuildManager(FlickrRestQueryBuilder buildHelper)
        {
            QueryBuilder = buildHelper;
            _baseAddress = "https://api.flickr.com/services/";
        }


        public string GetBaseAddress()
        {
            return _baseAddress;
        }


        public string GetImageQuery(string apiKey, SearchParameters searchConfig)
        {
            _queryBuilder.SetApiKey(apiKey);
            _queryBuilder.AddGetImageMethodQuery();
            _queryBuilder.SetSearchTags(new List<string> { searchConfig.SearchTag });
            _queryBuilder.SetSearchText(searchConfig.SearchText);
            _queryBuilder.SetItemsPerPageQuery(searchConfig.ItemsPerPage);
            _queryBuilder.SetCurrentPageQuery(searchConfig.CurrentPage);
            _queryBuilder.SetSafeSearchQuery(1);
            _queryBuilder.SetExtras("url_t,url_s,url_l,url_m");
            _queryBuilder.SetFormat("json");
            _queryBuilder.AddNoJsonCallBack();

            return _queryBuilder.Build();

        }

        public string GetCommentsQuery(string apiKey, string imageId)
        {
            _queryBuilder.SetApiKey(apiKey);
            _queryBuilder.AddGetCommentsMethodQuery();
            _queryBuilder.SetPhotoId(imageId);
            _queryBuilder.SetFormat("json");
            _queryBuilder.AddNoJsonCallBack();

            return _queryBuilder.Build();
        }

        public string GetImageInfoQuery(string apiKey, string imageId)
        {
            _queryBuilder.SetApiKey(apiKey);
            _queryBuilder.AddGetImageInfoMethodQuery();
            _queryBuilder.SetPhotoId(imageId);
            _queryBuilder.SetFormat("json");
            _queryBuilder.AddNoJsonCallBack();

            return _queryBuilder.Build();

        }

        #endregion
        
    }
}
