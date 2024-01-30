using _3legant.Shared.Models;

namespace Interfaces
{
    public interface ICartRepository
    {
        Task<IList<CartItemModel>> GetCartAsync();
        Task UpdateCartAsync(IList<CartItemModel> cart);
        Task<int> GetCartQuantityAsync();
        event Action<int> CartChanged;
        void NotifyCartChanged(int cartQuantity);

    }
}
