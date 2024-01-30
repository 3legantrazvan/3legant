using _3legant.Shared.Models;
using System.ComponentModel;

namespace _3legant.Client.ViewModels.Catalog
{
    public interface ISortByViewModel : INotifyPropertyChanged
    {
        string SelectedSort { get; set; }
        IList<SortOptionModel> SortOptions { get; set; }
        Task Initialize();
    }
}
