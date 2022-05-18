using MyStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
    public class ProductModelTests
    {
        public const string ProductRequiredMessage = "The Productname field is required.";
        public const int ValidCategoryId = 2;
        [Fact]
        public void FailingTest()
        {
            Assert.Equal(3,3);
        }
        [Fact]
        public void Should_Pass()
        {
            //arrange
            var sut = new ProductModel() 
            { 
                Categoryid=ValidCategoryId,
                Productid=Constants.TestProduct,
                Supplierid=Constants.TestSupplierId,
                Unitprice=Constants.TestUnitPrice,
                Discontinued=true,
                Productname="Test Product Name"
            };
            //act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);
            //assert
            Assert.True(actual, "Expected to succeed.");
        }
        [Fact]
        public void Should_Fail_When_ProductName_Is_Empty()
        {
            //arrange
            var sut = new ProductModel()
            {
                Categoryid = ValidCategoryId,
                Productid = 2,
                Supplierid = 2,
                //Unitprice = 10,
                Discontinued = true,
                Productname = ""
            };
            //act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            var message = validationResults[0];
            //assert
            Assert.Equal(ProductRequiredMessage, message.ErrorMessage);
        }
    }
}
