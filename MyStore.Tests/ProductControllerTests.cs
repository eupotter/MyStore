using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MyStore.Controllers;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Xunit;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace MyStore.Tests
{
    public class ProductControllerTests//:Controller
    {
        private Mock<IProductService> mockProductService;

        public ProductControllerTests()
        {

            mockProductService = new Mock<IProductService>();

        }
        [Fact]
        public async void Integration_Should_Return_Count_andOK_OnGetAll()
        {//on integration tests we need to run the application from 2 separate instances so that we have also an active response swagger together with the run test.
            HttpClient client = new HttpClient();
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            var openTestOrdersUrl = config.GetValue<string>("MySettings:OpenTestOrdersUrl");
            //HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/orders?listOfTowns=Warszawa&listOfTowns=Reims");
            HttpResponseMessage response = await client.GetAsync($"{openTestOrdersUrl}");
            List<Order> ordersList = new List<Order>();
            var contentData = string.Empty;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                ordersList = JsonConvert.DeserializeObject<List<Order>>(jsonString.Result);
            }
            Assert.Equal(12, ordersList.Count);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public void Should_Return_OK_OnGetByID()
        {
            //arrange
            int testSessionId = 1;
            mockProductService.Setup(x => x.GetById(testSessionId))
                .Returns(MultipleProducts()[testSessionId]);

            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.GetProduct(testSessionId);

            var result = response.Result as OkObjectResult;
            var actualData = result.Value as ProductModel;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ProductModel>(actualData);
        }

        [Fact]
        public void Should_Return_NotFoundResult_OnGetByID()
        {
            //arrange
            int testSessionId = 10;
            mockProductService.Setup(x => x.GetById(testSessionId)).Returns(NullProduct());

            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.GetProduct(testSessionId);

            //assert
            Assert.IsType<NotFoundResult>(response.Result);
            Assert.Null(response.Value);
        }
        private ProductModel? NullProduct()
        {
            return null;
        }
        [Fact]
        public void Should_Return_OK_OnGetAll()
        {
           //arrange
            mockProductService.Setup(x => x.GetAllProducts())
                .Returns(MultipleProducts());
            var controller = new ProductsController(mockProductService.Object);
            //act
            var response = controller.Get();
            var result = response.Result as OkObjectResult;
            var actualData = result.Value as IEnumerable<ProductModel>;
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ProductModel>>(actualData);
        }
        [Fact]
        public void ShouldReturn_AllProducts()
        {
            //arrange
            mockProductService.Setup(x => x.GetAllProducts())
                .Returns(MultipleProducts());
            var controller = new ProductsController(mockProductService.Object);
            //act
            var response = controller.Get();
            var result = response.Result as OkObjectResult;
            var actualData = result.Value as IEnumerable<ProductModel>;
            //assert
            Assert.Equal(MultipleProducts().Count(), actualData.Count());
        }

        [Fact]
        public void ShouldReturn_NoContent_On_Post()
        {
            ////arrange       
            mockProductService.Setup(x => x.AddProduct(It.IsAny<ProductModel>())).Returns(MultipleProducts()[1]);
            // Arrange
            var controller = new ProductsController(mockProductService.Object);
            // Act
            var response = controller.Post(MultipleProducts()[1]);
            // Assert
            Assert.IsType<CreatedAtActionResult>(response);
        }

        [Fact]
        public void ShouldReturn_OK_On_Put()
        {
            ProductModel model = new ProductModel
            {
                Productid = Constants.TestProduct,
                Productname = Constants.ProductName,
                Supplierid = Constants.TestSupplierId,
                Categoryid = (int)Constants.Categories.Condiments,
                Unitprice = Constants.TestUnitPrice,
                Discontinued = true
            };

            //arrange

            mockProductService.Setup(x => x.UpdateProduct(It.IsAny<ProductModel>())).Returns(model);
            mockProductService.Setup(x => x.Exists(Constants.TestProduct)).Returns(true);
            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.Put(Constants.TestProduct, model);
            var result = response.Result as OkObjectResult;
            var actualData = result.Value as ProductModel;

            //assert
            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public void ShouldReturn_NoContent_On_Delete()
        {
            ////arrange                     
            mockProductService.Setup(x => x.Delete(MultipleProducts()[1].Productid)).Returns(true);
            mockProductService.Setup(x => x.Exists(MultipleProducts()[1].Productid)).Returns(true);
            // Arrange
            var controller = new ProductsController(mockProductService.Object);
            // Act
            var response = controller.Delete(MultipleProducts()[1].Productid);            
            // Assert
            Assert.IsType<NoContentResult>(response);            
        }

        private List<ProductModel> MultipleProducts()
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
