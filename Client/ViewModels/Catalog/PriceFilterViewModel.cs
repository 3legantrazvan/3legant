using _3legant.Shared.Models;
using Interfaces;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _3legant.Client.ViewModels.Catalog
{
    public class PriceFilterViewModel : IPriceFilterViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ICatalogService _catalogService;
        private List<OptionsModel> _priceOptions = new List<OptionsModel>();
        private List<string> _selectedPriceRanges;

        public PriceFilterViewModel(ICatalogService catalogService)
        {
            _catalogService = catalogService;
            _priceOptions = new List<OptionsModel>();
            _selectedPriceRanges = new List<string>();
        }

        public List<string> SelectedPriceRanges
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

        public List<OptionsModel> PriceOptions
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
        }

        public void OnPriceOptionChanged(OptionsModel priceOption)
        {
            if (priceOption.Text == "All Price" && priceOption.Selected)
            {
                foreach (var option in PriceOptions.Where(o => o != priceOption))
                {
                    option.Selected = false;
                }
            }
            else
            {
                var allPriceOption = PriceOptions.First(o => o.Text == "All Price");
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
