using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrDesktop.Helper
{
    public class FlickrRestQueryBuilder
    {
        private IList<string> _parametersList = new List<string>();

        #region Builder Functions

        public void AddGetImageMethodQuery()
        {
            _parametersList.Add("method=flickr.photos.search");
        }

        public void AddGetCommentsMethodQuery()
        {
            _parametersList.Add("method=flickr.photos.comments.getList");
        }

        public void AddGetImageInfoMethodQuery()
        {
            _parametersList.Add("method=flickr.photos.getInfo");
        }

        public void AddNoJsonCallBack()
        {
            _parametersList.Add("nojsoncallback=1");
        }

        public void SetExtras(string extras)
        {
            if (!string.IsNullOrEmpty(extras))
            {
                _parametersList.Add($"extras={extras}");
            }
        }

        public void SetFormat(string format)
        {
            _parametersList.Add($"format={format}");
        }

        public void SetApiKey(string apiKey)
        {
            _parametersList.Add($"api_key={apiKey}");
        }

        public void SetPhotoId(string photoId)
        {
            _parametersList.Add($"photo_id={photoId}");
        }

        public void SetSearchText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                _parametersList.Add($"text={text}");
            }
        }

        public void SetSearchTags(List<string> tags)
        {
            if (tags != null && tags.Count > 0)
            {
                var values = string.Join(",", tags);
                _parametersList.Add($"tags={values}");
            }
        }

        public void SetItemsPerPageQuery(int itemsPerPage)
        {
            if (itemsPerPage > 0)
            {
                _parametersList.Add($"per_page={itemsPerPage}");
            }
        }

        public void SetCurrentPageQuery(int currentPage)
        {
            if (currentPage > 1)
            {
                _parametersList.Add($"page={currentPage}");
            }
        }

        public void SetSafeSearchQuery(int id)
        {
            _parametersList.Add($"safe_search={id}");
        }

        public string Build()
        {
            string result = "rest/?";
            if (_parametersList.Count > 0)
            {
                result += string.Join("&", _parametersList);
            }
            Reset();
            return result;
        }

        public void Reset()
        {
            _parametersList.Clear();
        }
    }
    #endregion
}