using _3legant.Shared.Models;
using Blazored.LocalStorage;
using Interfaces;

namespace Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ILocalStorageService _localStorage;
        private const string CartKey = "cart";

        public event Action<int> CartChanged;

        public void NotifyCartChanged(int cartQuantity)
        {
            CartChanged?.Invoke(cartQuantity);
        }

        public CartRepository(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<CartItemModel>> GetCartAsync()
        {
            return await _localStorage.GetItemAsync<List<CartItemModel>>(CartKey) ?? new List<CartItemModel>();
        }

        public async Task UpdateCartAsync(List<CartItemModel> cart)
        {
            await _localStorage.SetItemAsync(CartKey, cart);
        }

        public async Task<int> GetCartQuantityAsync()
        {
            var cart = await GetCartAsync();
            return cart.Sum(item => item.Quantity);
        }

    }
}
