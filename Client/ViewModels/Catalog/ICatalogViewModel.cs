using _3legant.Shared.Models;
using System.ComponentModel;

namespace ViewModels.Catalog
{
    public interface ICatalogViewModel : INotifyPropertyChanged
    {
        List<ProductModel> Products { get; set; }
        PaginationInfoModel PaginationInfo { get; set; }
        int PageSize { get; set; }
        string SelectedSortOrder { get; set; }
        string SelectedCategory { get; set; }
        List<string> SelectedPriceRanges { get; set; }
        Task HandlePageChange(int newPage);
        Task HandleSortChange(string newSortBy);
        void HandleCategoryChanged(string newCategories);
        void HandlePricesChanged(List<string> newPriceRanges);
        Task LoadData(CatalogQueryParametersModel catalogQueryParameters);
    }
}
