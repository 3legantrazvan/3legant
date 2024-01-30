using _3legant.Shared.Models;

namespace Interfaces
{
    public interface IWishlistRepository
    {
        Task<IList<WishlistItemModel>> GetWishlistAsync();
        Task UpdateWishlistAsync(IList<WishlistItemModel> Wishlist);
        Task<bool> IsProductInWishlist(int ProductId);
        Task AddToWishlistAsync(WishlistItemModel WishlistItem);
        Task RemoveFromWishlistAsync(int ProductId);
    }
}
