using System.ComponentModel;
using System.Runtime.CompilerServices;
using _3legant.Shared.Models;
using Interfaces;

namespace ViewModels.Catalog
{
    public class CatalogViewModel : ICatalogViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<ProductModel> _products;
        private PaginationInfoModel _paginationInfo;
        private int _pageSize;
        private string _selectedSortOrder;
        private string _selectedCategory;
        private List<string> _selectedPriceRanges;
        private readonly ICatalogService _catalogService;

        public CatalogViewModel(ICatalogService catalogService)
        {
            _catalogService = catalogService;
            Products = new List<ProductModel>();
            PaginationInfo = new PaginationInfoModel();
            PageSize = 10;
            SelectedSortOrder = "PriceLowToHigh";
            SelectedCategory = "All Rooms";
            SelectedPriceRanges = new List<string> { "All Price" };
        }

       
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<ProductModel> Products
        {
            get => _products;
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged();
                }
            }
        }

        public PaginationInfoModel PaginationInfo
        {
            get => _paginationInfo;
            set
            {
                if (_paginationInfo != value)
                {
                    _paginationInfo = value;
                    OnPropertyChanged();
                }
            }
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedSortOrder
        {
            get => _selectedSortOrder;
            set
            {
                if (_selectedSortOrder != value)
                {
                    _selectedSortOrder = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> SelectedPriceRanges
        {
            get => _selectedPriceRanges;
            set
            {
                if (_selectedPriceRanges != value)
                {
                    _selectedPriceRanges = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task HandlePageChange(int newPage)
        {
            await LoadData(new CatalogQueryParametersModel()
            {
                Page = newPage,
                PageSize = PageSize,
                SortBy = SelectedSortOrder,
                PriceRanges = SelectedPriceRanges,
                Category = SelectedCategory
            });
        }

        public async Task HandleSortChange(string newSortBy)
        {
            if (SelectedSortOrder != newSortBy)
            {
                SelectedSortOrder = newSortBy;
                await LoadData(new CatalogQueryParametersModel()
                {
                    Page = 1,
                    PageSize = PageSize,
                    SortBy = SelectedSortOrder,
                    PriceRanges = SelectedPriceRanges,
                    Category = SelectedCategory
                });
            }
        }

        public async void HandleCategoryChanged(string newCategories)
        {
            SelectedCategory = newCategories;
            await LoadData(new CatalogQueryParametersModel()
            {
                Page = 1,
                PageSize = PageSize,
                SortBy = SelectedSortOrder,
                PriceRanges = SelectedPriceRanges,
                Category = SelectedCategory
            });
        }

        public async void HandlePricesChanged(List<string> newPriceRanges)
        {
            SelectedPriceRanges = newPriceRanges;
            await LoadData(new CatalogQueryParametersModel()
            {
                Page = 1,
                PageSize = PageSize,
                SortBy = SelectedSortOrder,
                PriceRanges = SelectedPriceRanges,
                Category = SelectedCategory
            });
        }

        public async Task LoadData(CatalogQueryParametersModel catalogQueryParameters)
        {
            var response = await _catalogService.GetProducts(catalogQueryParameters);

            Products = response.Products;
            PaginationInfo = response.PaginationInfo;
        }
    }
}
