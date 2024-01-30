using _3legant.Client.ViewModels.Catalog;
using _3legant.Server.Models;
using _3legant.Shared.Models;
using Interfaces;
using Moq;
using ViewModels.Catalog;

namespace _3legantTest
{
    public class UnitTest
    {
        [Fact]
        public async Task HandlePricesChanged_ShouldUpdatePriceRangesAndLoadDataWithNewPriceRanges()
        {
            // Arrange
            var catalogServiceMock = new Mock<ICatalogService>();
            var newPriceRanges = new List<string> { "00.00-99", "300-399" };

            var responseModel = new ProductsResponseModel
            {
                Products = new List<ProductModel>(),
                PaginationInfo = new PaginationInfoModel(),
            };
            catalogServiceMock.Setup(x => x.GetProducts(It.IsAny<CatalogQueryParametersModel>()))
                             .Returns(Task.FromResult(responseModel));

            var catalogViewModel = new CatalogViewModel(catalogServiceMock.Object);
            catalogViewModel.HandlePricesChanged(newPriceRanges);

            // Act
            await catalogViewModel.LoadData(new CatalogQueryParametersModel());

            // Assert
            Assert.Equal(newPriceRanges, catalogViewModel.SelectedPriceRanges);
        }

        [Fact]
        public async Task ChangeCategory_ShouldUpdateSelectedCategoryAndLoadDataWithSelectedCategory()
        {
            // Arrange
            var catalogServiceMock = new Mock<ICatalogService>();
            var selectedCategory = "All Rooms";

            var responseModel = new ProductsResponseModel
            {
                Products = new List<ProductModel>(),
                PaginationInfo = new PaginationInfoModel(),
            };

            catalogServiceMock
                .Setup(x => x.GetProducts(It.IsAny<CatalogQueryParametersModel>()))
                .Returns(Task.FromResult(responseModel));

            var catalogViewModel = new CatalogViewModel(catalogServiceMock.Object);

            // Act
            catalogViewModel.HandleSortChange(selectedCategory);

            // Assert
            Assert.Equal(selectedCategory, catalogViewModel.SelectedCategory);
            Assert.Equal(responseModel.Products, catalogViewModel.Products);
        }

        [Fact]
        public async Task InitializeAsync_ShouldRetrievePriceRangeFiltersFromService()
        {
            // Arrange
            var catalogServiceMock = new Mock<ICatalogService>();
            var priceOptions = new List<OptionsModel>
                    {
                new OptionsModel { Value = "00.00-"+decimal.MaxValue, Text = "All Price", Selected = true},
                        new OptionsModel { Value = "00.00-99", Text = "0.00-99.99" },
                        new OptionsModel { Value = "100-199", Text = "$100.00-199.99" },
                        new OptionsModel { Value = "200-299", Text = "$200.00-299.99" },
                        new OptionsModel { Value = "300-399", Text = "$300.00-399.99" },
                        new OptionsModel { Value = "400-"+decimal.MaxValue, Text = "$400.00+" },
                    };

            IList<OptionsModel> result = priceOptions;
            catalogServiceMock
                .Setup(x => x.GetPriceRangeFilters())
                .Returns(Task.FromResult(result));

            var priceFilterViewModel = new PriceFilterViewModel(catalogServiceMock.Object);

            // Act
            await priceFilterViewModel.InitializeAsync();

            // Assert
            Assert.Equal(priceOptions, priceFilterViewModel.PriceOptions);
        }

        [Fact]
        public async Task LoadSortOptionsAsync_ShouldLoadSortOptionsFromSortService()
        {
            // Arrange
            var sortServiceMock = new Mock<ICatalogService>();
            var sortOptions = new List<SortOptionModel>
                        {
                             new SortOptionModel { Value = "PriceLowToHigh", Text = "Price Low to High" },
                             new SortOptionModel { Value = "PriceHighToLow", Text = "Price High to Low" },
                             new SortOptionModel { Value = "NameAZ", Text = "Name A-Z" },
                             new SortOptionModel { Value = "NameZA", Text = "Name Z-A" },
                        };
            IList<SortOptionModel> sortOptionModels = sortOptions;

            sortServiceMock
                .Setup(x => x.GetSortObtions())
                .Returns(Task.FromResult(sortOptionModels));

            var sortByViewModel = new SortByViewModel(sortServiceMock.Object);

            // Act
            await sortByViewModel.Initialize();

            // Assert
            Assert.Equal(sortOptions, sortByViewModel.SortOptions);
        }

    }
}