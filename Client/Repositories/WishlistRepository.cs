using _3legant.Shared.Models;
using Blazored.LocalStorage;
using Interfaces;

namespace Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ILocalStorageService _localStorage;
        private const string WishlistKey = "wishlist";

        public WishlistRepository(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<List<WishlistItemModel>> GetWishlistAsync()
        {
            return await _localStorage.GetItemAsync<List<WishlistItemModel>>(WishlistKey) ?? new List<WishlistItemModel>();
        }

        public async Task UpdateWishlistAsync(List<WishlistItemModel> Wishlist)
        {
            await _localStorage.SetItemAsync(WishlistKey, Wishlist);
        }

        public async Task<bool> IsProductInWishlist(int ProductId)
        {
            var wishlist = await GetWishlistAsync();
            return wishlist.Any(item => item.ProductId == ProductId);
        }

        public async Task AddToWishlistAsync(WishlistItemModel WishlistItem)
        {
            var wishlist = await GetWishlistAsync();
            wishlist.Add(WishlistItem);
            await UpdateWishlistAsync(wishlist);
        }

        public async Task RemoveFromWishlistAsync(int ProductId)
        {
            var wishlist = await GetWishlistAsync();
            var existingWishlistItem = wishlist.FirstOrDefault(item => item.ProductId == ProductId);

            if (existingWishlistItem != null)
            {
                wishlist.Remove(existingWishlistItem);
                await UpdateWishlistAsync(wishlist);
            }
        }
    }
}
