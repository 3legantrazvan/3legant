using _3legant.Shared.Models;
using Interfaces;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace _3legant.Client.ViewModels.Product
{
    public class CartViewModel : ICartViewModel
    {
        private ICartRepository _cartRepository;
        private IList<CartItemModel> _cart;
        private int _productId;
        private int _cartQuantity;

        public CartViewModel(ICartRepository cartRepository)
        {
            _cart = new List<CartItemModel>();
            _productId = ProductId;
            _cartQuantity = 0;
            _cartRepository = cartRepository;
        }

        public IList<CartItemModel> Cart
        {
            get => _cart;
            set
            {
                if (_cart != value)
                {
                    _cart = value;
                    OnPropertyChanged(nameof(Cart));
                }
            }
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

        public int CartQuantity
        {
            get => _cartQuantity;
            set
            {
                if (_cartQuantity != value)
                {
                    _cartQuantity = value;
                    OnPropertyChanged(nameof(CartQuantity));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task IncrementQuantity()
        {
            var existingCartItem = Cart.FirstOrDefault(item => item.ProductId == ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else
            {
                Cart.Add(new CartItemModel
                {
                    ProductId = ProductId,
                    Quantity = 1
                });
            }

            await _cartRepository.UpdateCartAsync(Cart);
            await UpdateCartQuantity();
            OnPropertyChanged(nameof(CartQuantity));
        }

        public async Task DecrementQuantity()
        {
            var existingCartItem = Cart.FirstOrDefault(item => item.ProductId == ProductId);

            if (existingCartItem != null && existingCartItem.Quantity > 0)
            {
                existingCartItem.Quantity--;
                await _cartRepository.UpdateCartAsync(Cart);
                await UpdateCartQuantity();
                OnPropertyChanged(nameof(CartQuantity));
            }
        }

        public async Task<int> GetCartQuantity()
        {
            var cartItems = await _cartRepository.GetCartAsync();
            var result = cartItems.FirstOrDefault(item => item.ProductId == ProductId)?.Quantity ?? 0;
            CartQuantity = result;
            return result;
        }

        public async Task UpdateCartQuantity()
        {
            CartQuantity = await GetCartQuantity();
        }
    }

}