using _3legant.Shared.Models;

namespace Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartItemModel>> GetCartAsync();
        Task UpdateCartAsync(List<CartItemModel> cart);
        Task<int> GetCartQuantityAsync();
        event Action<int> CartChanged;
        void NotifyCartChanged(int cartQuantity);

    }
}
