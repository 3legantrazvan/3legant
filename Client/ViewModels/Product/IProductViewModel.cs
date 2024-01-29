using _3legant.Shared.Models;
using System.ComponentModel;

namespace ViewModels.Product
{
    public interface IProductViewModel : INotifyPropertyChanged
    {
        int ProductId { get; set; }
        ProductModel Product { get; set; }
        DateTime TargetDateTime { get; set; }
        int Days { get; set; }
        int Hours { get; set; }
        int Minutes { get; set; }
        int Seconds { get; set; }
        void LoadProductData(int productId);
    }
}
