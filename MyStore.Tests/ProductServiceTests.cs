using AutoMapper;
using Moq;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Models;
using MyStore.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyStore.Tests
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> mockProductRepository;
        private Mock<IMapper> mockMapper;
        public ProductServiceTests()
        {
            mockProductRepository = new Mock<IProductRepository>();
            mockMapper = new Mock<IMapper>();
        }
        [Fact]
        public void Should_Return_Count_and_Type_OnGetAll()
        {
            //arrange
            mockProductRepository.Setup(x => x.GetAll()).Returns(MultipleProducts());
            mockMapper.Setup(x => x.Map<IEnumerable<ProductModel>>(It.IsAny<IEnumerable<Product>>())).Returns(MultipleProductsModel());
            var service = new ProductService(mockProductRepository.Object, mockMapper.Object);
            //act
            var response = service.GetAllProducts();
            //assert
            Assert.IsType<List<ProductModel>>(response);
            Assert.Equal(MultipleProducts().Count(), response.Count());
        }
        [Fact]
        public void ShouldReturn_Type_On_Post()
        {
            ////Arrange       
            mockProductRepository.Setup(x => x.Add(MultipleProducts()[1])).Returns(MultipleProducts()[1]);
            mockMapper.Setup(x => x.Map<ProductModel>(It.IsAny<Product>())).Returns(MultipleProductsModel()[1]);
            var service = new ProductService(mockProductRepository.Object,mockMapper.Object);
            // Act
            var response = service.AddProduct(MultipleProductsModel()[1]);
            // Assert
            Assert.IsType<ProductModel>(response);
        }
        [Fact]
        public void ShouldReturn_Type_On_GetById()
        {
            ////Arrange       
            mockProductRepository.Setup(x => x.GetById(MultipleProducts()[1].Productid)).Returns(MultipleProducts()[1]);
            mockMapper.Setup(x => x.Map<ProductModel>(It.IsAny<Product>())).Returns(MultipleProductsModel()[1]);
            var service = new ProductService(mockProductRepository.Object, mockMapper.Object);
            // Act
            var response = service.GetById(MultipleProductsModel()[1].Productid);
            // Assert
            Assert.IsType<ProductModel>(response);
        }
        [Fact]
        public void ShouldReturn_Type_On_Exists()
        {
            ////Arrange       
            mockProductRepository.Setup(x => x.Exists(MultipleProducts()[1].Productid)).Returns(true);
            var service = new ProductService(mockProductRepository.Object, mockMapper.Object);
            // Act
            var response = service.Exists(MultipleProductsModel()[1].Productid);
            // Assert
            Assert.Equal(true,response);
        }
        [Fact]
        public void ShouldReturn_true_On_Delete()
        {
            ////arrange                     
            mockProductRepository.Setup(x => x.Delete(It.IsAny<Product>())).Returns(true);
            mockMapper.Setup(x => x.Map<ProductModel>(It.IsAny<Product>())).Returns(MultipleProductsModel()[1]);
            // Arrange
            var service = new ProductService(mockProductRepository.Object,mockMapper.Object);
            // Act
            var response = service.Delete(MultipleProducts()[1].Productid);
            // Assert
            Assert.Equal(true,response);
        }
        private List<Product> MultipleProducts()
        {
            return new List<Product>() {
            new Product
            {
                Categoryid = (int)Constants.Categories.Condiments,
                Productid = Constants.TestProduct,
                Productname = Constants.ProductName,
                Discontinued = false,
                Supplierid=Constants.TestSupplierId,
                Unitprice=Constants.TestUnitPrice
            },
            new Product
            {
                Categoryid = (int)Constants.Categories.Condiments,
                Productid = Constants.TestProduct+1,
                Productname = Constants.ProductName,
                Discontinued = false,
                Supplierid=Constants.TestSupplierId,
                Unitprice=Constants.TestUnitPrice
            },
            new Product
            {
                Categoryid = (int)Constants.Categories.Condiments,
                Productid = Constants.TestProduct+2,
                Productname = Constants.ProductName,
                Discontinued = false,
                Supplierid=Constants.TestSupplierId,
                Unitprice=Constants.TestUnitPrice
            },
            };
        }
        private List<ProductModel> MultipleProductsModel()
        {
            return new List<ProductModel>() {
            new ProductModel
            {
                Categoryid = (int)Constants.Categories.Condiments,
                Productid = Constants.TestProduct,
                Productname = Constants.ProductName,
                Discontinued = false,
                Supplierid=Constants.TestSupplierId,
                Unitprice=Constants.TestUnitPrice
            },
            new ProductModel
            {
                Categoryid = (int)Constants.Categories.Condiments,
                Productid = Constants.TestProduct+1,
                Productname = Constants.ProductName,
                Discontinued = false,
                Supplierid=Constants.TestSupplierId,
                Unitprice=Constants.TestUnitPrice
            },
            new ProductModel
            {
                Categoryid = (int)Constants.Categories.Condiments,
                Productid = Constants.TestProduct+2,
                Productname = Constants.ProductName,
                Discontinued = false,
                Supplierid=Constants.TestSupplierId,
                Unitprice=Constants.TestUnitPrice
            },
            };
        }
    }
}
