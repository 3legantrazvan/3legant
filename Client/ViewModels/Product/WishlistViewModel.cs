using _3legant.Shared.Models;
using Interfaces;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _3legant.Client.ViewModels.Product
{
    public class WishlistViewModel : IWishlistViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IWishlistRepository _wishlistRepository;
        private int _productId;
        private bool _isAddedToWishlist;

        public WishlistViewModel(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
            _productId = 0;
        }

        [Parameter]
        public int ProductId
        {
            get => _productId;
            set
            {
                if (_productId != value)
                {
                    _productId = value;
                    OnPropertyChanged(nameof(ProductId));
                }
            }
        }

        public bool IsAddedToWishlist
        {
            get => _isAddedToWishlist;
            set
            {
                if (_isAddedToWishlist != value)
                {
                    _isAddedToWishlist = value;
                    OnPropertyChanged(nameof(IsAddedToWishlist));
                }
            }
        }

        public async Task Initialize()
        {
            _isAddedToWishlist = await _wishlistRepository.IsProductInWishlist(_productId);
        }

        public async Task ToggleWishlist()
        {
            if (_isAddedToWishlist)
            {
                await RemoveFromWishlist();
            }
            else
            {
                await AddToWishlist();
            }
            await Initialize();
        }

        private async Task AddToWishlist()
        {
            await _wishlistRepository.AddToWishlistAsync(new WishlistItemModel
            {
                ProductId = _productId
            });
        }

        private async Task RemoveFromWishlist()
        {
            await _wishlistRepository.RemoveFromWishlistAsync(_productId);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}