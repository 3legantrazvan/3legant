using _3legant.Client;
using _3legant.Client.Services;
using _3legant.Client.ViewModels.Catalog;
using _3legant.Client.ViewModels.Product;
using Blazored.LocalStorage;
using Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Repositories;
using ViewModels.Catalog;
using ViewModels.Product;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#region Services
builder.Services.AddScoped<ICatalogService, CatalogService>();
#endregion

#region Repositories
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<LocalStorageRepository>();
#endregion

#region ViewModels
builder.Services.AddScoped<ICatalogViewModel, CatalogViewModel>();
builder.Services.AddScoped<IProductViewModel, ProductViewModel>();
builder.Services.AddScoped<ICartViewModel, CartViewModel>();
builder.Services.AddScoped<IWishlistViewModel, WishlistViewModel>();
builder.Services.AddScoped<ICategoryFilterViewModel, CategoryFilterViewModel>();
builder.Services.AddScoped<IPriceFilterViewModel, PriceFilterViewModel>();
builder.Services.AddScoped<ISortByViewModel, SortByViewModel>();
#endregion

await builder.Build().RunAsync();
