using _3legant.Shared.Utils;
using Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _3legant.Client.ViewModels.Catalog
{
    public class CategoryFilterViewModel : ICategoryFilterViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ICatalogService _catalogService;
        private IList<string> _categoryOptions;
        private string _selectedCategory;

        public CategoryFilterViewModel(ICatalogService catalogService)
        {
            _catalogService = catalogService;
            _categoryOptions = new List<string>();
            _selectedCategory = CatalogConstants.AllRooms;
        }

        public IList<string> CategoryOptions
        {
            get => _categoryOptions;
            set
            {
                _categoryOptions = value;
                OnPropertyChanged(nameof(CategoryOptions));
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public async Task Initialize()
        {
            CategoryOptions = await _catalogService.GetCategoryFilters();
        }

        public async Task ChangeCategory(string selectedCategory)
        {
            _selectedCategory = selectedCategory;
            OnPropertyChanged(nameof(SelectedCategory));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}