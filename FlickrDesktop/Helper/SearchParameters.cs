using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrDesktop.Helper
{
    public partial class SearchParameters : ObservableObject
    {
        #region Private Properties
        [ObservableProperty]
        private string _searchTag;
        [ObservableProperty]
        private string _searchText;
        [ObservableProperty]
        private int _itemsPerPage;
        [ObservableProperty]
        private int _currentPage;

        #endregion
    }
}
