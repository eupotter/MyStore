using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
using MyStore.Infrastructure.Attributes;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        [ResponseHeader("Awesome-Header","From-Web-Api-Filter")]
        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            string[] games = {"Morrowind", "BioShock", "Daxter","The Darkness", "Half Life", "System Shock 2"};

            IEnumerable<string> subset =
                from g in games
                where g.Length > 6
                && g.Substring(0,1)=="M"
                orderby g
                select g;
            IEnumerable<string> subset2 =
                games.Where(x => x.Length > 6)
                .OrderBy(x=>x)
                .Select(x=>x);

            var productList = productService.GetAllProducts();
            return Ok(productList);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id:int}")]
        public ActionResult<ProductModel> GetProduct(int id)
        {
            var result = productService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductModel newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //failfast -> return
            //productService.Add();
            var addedProduct = productService.AddProduct(newProduct);
            
            return CreatedAtAction("Get", new { id = addedProduct.Productid }, addedProduct);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //,Type=typeof(Product) daca vreau sa adaug obiectul la return
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type=typeof(ProductModel))]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<ProductModel> Put(int id, [FromBody] ProductModel productToUpdate)
        {
            //exists by 
            if (id!=productToUpdate.Productid)
            {
                return BadRequest();
            }
            if (!productService.Exists(id))
            {
                return NotFound();
            }
            var updatedProduct = productService.UpdateProduct(productToUpdate);
            return Ok(updatedProduct);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProductModel))]
        public IActionResult Delete(int id)
        {
            if (!productService.Exists(id))
            {
                return NotFound();
            }
            productService.Delete(id);
            return NoContent(); 

        }
    }
}
