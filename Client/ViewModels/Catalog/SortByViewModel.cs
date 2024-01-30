using _3legant.Shared.Models;
using Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _3legant.Client.ViewModels.Catalog
{
    public class SortByViewModel : ISortByViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ICatalogService _catalogService;

        private string _selectedSort;
        private IList<SortOptionModel> _sortOptions;

        public SortByViewModel(ICatalogService catalogService)
        {
            _catalogService = catalogService;
            _selectedSort = string.Empty;
            _sortOptions = new List<SortOptionModel>();
        }

        public string SelectedSort
        {
            get { return _selectedSort; }
            set
            {
                if (_selectedSort != value)
                {
                    _selectedSort = value;
                    OnPropertyChanged(nameof(SelectedSort));
                }
            }
        }

        public IList<SortOptionModel> SortOptions
        {
            get { return _sortOptions; }
            set
            {
                if (_sortOptions != value)
                {
                    _sortOptions = value;
                    OnPropertyChanged(nameof(SortOptions));
                }
            }
        }

        public async Task Initialize()
        {
            SortOptions = await _catalogService.GetSortObtions();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
