using _3legant.Shared.Models;

namespace Interfaces
{
    public interface IWishlistRepository
    {
        Task<List<WishlistItemModel>> GetWishlistAsync();
        Task UpdateWishlistAsync(List<WishlistItemModel> Wishlist);
        Task<bool> IsProductInWishlist(int ProductId);
        Task AddToWishlistAsync(WishlistItemModel WishlistItem);
        Task RemoveFromWishlistAsync(int ProductId);
    }
}
