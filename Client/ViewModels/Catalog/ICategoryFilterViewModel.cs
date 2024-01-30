using System.ComponentModel;

namespace _3legant.Client.ViewModels.Catalog
{
    public interface ICategoryFilterViewModel : INotifyPropertyChanged
    {
        IList<string> CategoryOptions { get; }
        string SelectedCategory { get; set; }
        Task Initialize();
        Task ChangeCategory(string selectedCategory);
    }
}
