using _3legant.Shared.Models;
using System.ComponentModel;

namespace _3legant.Client.ViewModels.Catalog
{
    public interface IPriceFilterViewModel : INotifyPropertyChanged
    {
        List<string> SelectedPriceRanges { get; set; }
        List<OptionsModel> PriceOptions { get; }
        Task InitializeAsync();
        void OnPriceOptionChanged(OptionsModel priceOption);
    }
}