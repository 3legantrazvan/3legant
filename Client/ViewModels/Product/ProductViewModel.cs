using System.ComponentModel;
using System.Runtime.CompilerServices;
using _3legant.Shared.Models;
using Interfaces;

namespace ViewModels.Product
{
    public class ProductViewModel : IProductViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _productId;
        private ProductModel _product;
        private DateTime _targetDateTime;
        private int _days;
        private int _hours;
        private int _minutes;
        private int _seconds;
        private readonly ICatalogService _catalogService;
        private Timer _countdownTimer;

        public ProductViewModel(ICatalogService catalogService)
        {
            _catalogService = catalogService;
            ProductId = 0;
            TargetDateTime = DateTime.MinValue;
            Product = new ProductModel();
            Days = 0;
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            _countdownTimer = new Timer(_ => UpdateCountdown(), null, 0, 1000);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ProductId
        {
            get => _productId;
            set
            {
                if (_productId != value)
                {
                    _productId = value;
                    OnPropertyChanged();
                    LoadProductData(value);
                }
            }
        }

        public ProductModel Product
        {
            get => _product;
            set
            {
                if (_product != value)
                {
                    _product = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime TargetDateTime
        {
            get => _targetDateTime;
            set
            {
                if (_targetDateTime != value)
                {
                    _targetDateTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Days
        {
            get => _days;
            set
            {
                if (_days != value)
                {
                    _days = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Hours
        {
            get => _hours;
            set
            {
                if (_hours != value)
                {
                    _hours = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Minutes
        {
            get => _minutes;
            set
            {
                if (_minutes != value)
                {
                    _minutes = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Seconds
        {
            get => _seconds;
            set
            {
                if (_seconds != value)
                {
                    _seconds = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Dispose()
        {
            _countdownTimer?.Dispose();
        }

        public async void LoadProductData(int productId)
        {
            Product = await _catalogService.GetProductById(productId);
            TargetDateTime = Product.CountDownOffer.AddDays(50);
        }

        private void UpdateCountdown()
        {
            TimeSpan remainingTime = TargetDateTime - DateTime.Now;

            Days = remainingTime.Days;
            Hours = remainingTime.Hours;
            Minutes = remainingTime.Minutes;
            Seconds = remainingTime.Seconds;
        }
    }
}
