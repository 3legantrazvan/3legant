using _3legant.Shared.Models;
using _3legant.Shared.Utils;
using Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _3legant.Client.ViewModels.Catalog
{
    public class PriceFilterViewModel : IPriceFilterViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ICatalogService _catalogService;
        private IList<OptionsModel> _priceOptions = new List<OptionsModel>();
        private IList<string> _selectedPriceRanges;

        public PriceFilterViewModel(ICatalogService catalogService)
        {
            _catalogService = catalogService;
            _priceOptions = new List<OptionsModel>();
            _selectedPriceRanges = new List<string>();
        }

        public IList<string> SelectedPriceRanges
        {
            get => _selectedPriceRanges;
            set
            {
                if (_selectedPriceRanges != value)
                {
                    _selectedPriceRanges = value;
                    OnPropertyChanged(nameof(SelectedPriceRanges));
                }
            }
        }

        public IList<OptionsModel> PriceOptions
        {
            get => _priceOptions;
            set
            {
                if (_priceOptions != value)
                {
                    _priceOptions = value;
                    OnPropertyChanged(nameof(PriceOptions));
                }
            }
        }

        public async Task InitializeAsync()
        {
            PriceOptions = await _catalogService.GetPriceRangeFilters();
            SelectedPriceRanges = new List<string>();
        }

        public void OnPriceOptionChanged(OptionsModel priceOption)
        {
            if (priceOption.Text == CatalogConstants.AllPrice && priceOption.Selected)
            {
                foreach (var option in PriceOptions.Where(o => o != priceOption))
                {
                    option.Selected = false;
                }
            }
            else
            {
                var allPriceOption = PriceOptions.First(o => o.Text == CatalogConstants.AllPrice);
                allPriceOption.Selected = false;
            }


            if (!SelectedPriceRanges.Contains(priceOption.Value))
            {
                SelectedPriceRanges.Add(priceOption.Value);
            }
            else
            {
                SelectedPriceRanges.Remove(priceOption.Value);
            }
            OnPropertyChanged("SelectedPriceRanges");
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
