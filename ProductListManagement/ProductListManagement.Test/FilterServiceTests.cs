using ProductListManagement.Model;
using ProductListManagement.Service;
using ProductListManagement.Service.Dtos;

namespace ProductListManagement.Test
{
    public class FilterServiceTests
    {
        private readonly FilterService _filterService;

        public FilterServiceTests()
        {
            _filterService = new FilterService();
        }

        [Fact]
        public async Task CreateProductFilter_AddProductName_ReturnsDataByName()
        {
            //Arrange
            var expected = 2;
            var testData = new List<Product>()
            {
                new Product() { Name = "Milk" },
                new Product() { Name = "Apple" },
                new Product() { Name = "Fish" },
                new Product() { Name = "Bread" },
                new Product() { Name = "Milk" },
                new Product() { Name = "Fish" },
            };
            var filrer = new FilterDto()
            {
                Name = "Milk"
            };

            // Act
            var predicate = await _filterService.CreateProductFilter(filrer);
            var filtered = testData.Where(predicate.Compile()).ToList();
            var actual = filtered.Count();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateProductFilter_AddProductName_ReturnsAllNameContainsProductName()
        {
            //Arrange
            var testData = new List<Product>()
            {
                new Product() { Name = "Milk" },
                new Product() { Name = "Apple" },
                new Product() { Name = "Fish" },
                new Product() { Name = "Bread" },
                new Product() { Name = "Milk" },
                new Product() { Name = "Fish" },
            };
            var filrer = new FilterDto()
            {
                Name = "Milk"
            };

            // Act
            var predicate = await _filterService.CreateProductFilter(filrer);
            var filtered = testData.Where(predicate.Compile()).ToList();

            // Assert
            Assert.True(filtered.All(p => p.Name.Contains(filrer.Name)));
        }

        [Fact]
        public async Task CreateProductFilter_AddPrice_ReturnsAllPriceEqualFilterPrice()
        {
            //Arrange
            var expected = 233.43435;
            var testData = new List<Product>()
            {
                new Product() { Price = 123.12 },
                new Product() { Price = 123.183 },
                new Product() { Price = 123222.12 },
                new Product() { Price = expected },
                new Product() { Price = 124443.42 },
                new Product() { Price = 1211.12 },
            };
            var filrer = new FilterDto()
            {
                Price = expected
            };

            // Act
            var predicate = await _filterService.CreateProductFilter(filrer);
            var filtered = testData.Where(predicate.Compile()).ToList();

            // Assert
            Assert.True(filtered.All(p => p.Price == expected));
        }

        [Fact]
        public async Task CreateProductFilter_AddPrice_ReturnsAllPriceMoreFilterPrice()
        {
            //Arrange
            var expected = 233.43435;
            var testData = new List<Product>()
            {
                new Product() { Price = 123.12 },
                new Product() { Price = 123.183 },
                new Product() { Price = 123222.12 },
                new Product() { Price = expected },
                new Product() { Price = 124443.42 },
                new Product() { Price = 1211.12 },
            };
            var filrer = new FilterDto()
            {
                MinPrice = expected
            };

            // Act
            var predicate = await _filterService.CreateProductFilter(filrer);
            var filtered = testData.Where(predicate.Compile()).ToList();

            // Assert
            Assert.True(filtered.All(p => p.Price >= expected));
        }

        [Fact]
        public async Task CreateProductFilter_AddPrice_ReturnsAllPriceLessFilterPrice()
        {
            //Arrange
            var expected = 233.43435;
            var testData = new List<Product>()
            {
                new Product() { Price = 123.12 },
                new Product() { Price = 123.183 },
                new Product() { Price = 123222.12 },
                new Product() { Price = expected },
                new Product() { Price = 124443.42 },
                new Product() { Price = 1211.12 },
            };
            var filrer = new FilterDto()
            {
                MaxPrice = expected
            };

            // Act
            var predicate = await _filterService.CreateProductFilter(filrer);
            var filtered = testData.Where(predicate.Compile()).ToList();

            // Assert
            Assert.True(filtered.All(p => p.Price <= expected));
        }
    }   
}
