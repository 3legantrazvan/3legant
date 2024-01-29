using System.ComponentModel;

namespace _3legant.Client.ViewModels.Product
{
    public interface IWishlistViewModel : INotifyPropertyChanged
    {
        int ProductId { get; set; }
        bool IsAddedToWishlist { get; set; }
        Task Initialize();
        Task ToggleWishlist();
    }
}
