using _3legant.Shared.Models;
using System.ComponentModel;

namespace _3legant.Client.ViewModels.Product
{
    public interface ICartViewModel : INotifyPropertyChanged
    {
        IList<CartItemModel> Cart { get; set; }
        int ProductId { get; set; }
        int CartQuantity { get; set; }
        Task IncrementQuantity();
        Task DecrementQuantity();
        Task<int> GetCartQuantity();
        Task UpdateCartQuantity();
    }
}
