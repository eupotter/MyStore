using Moq;
using MyStore.Domain.Entities;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Tests.Moks.Services
{
    public class MockProductService:Mock<IProductService>
    {
        public MockProductService MockGetAllProducts(List<ProductModel> results)
        {
            Setup(x => x.GetAllProducts())
                .Returns(results);
            return this;
            //this.Metoda1().Metoda2().().()... fluent
        }

        public MockProductService MockGetById(ProductModel product)
        {
            Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(product);
                //.Throws(new Exception("Product with Id not found."));
            return this;
        }
        public MockProductService MockGetByIDInvalid()
        {
            Setup(x => x.GetById(It.IsAny<int>()))
                .Throws(new Exception("Product  with that ID was not found!"));

            return this;
        }
    }
}
