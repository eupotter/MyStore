using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public ActionResult<IEnumerable<CustomerModel>> Get()
        {
            var customerList = customerService.GetAllCustomers();
            return Ok(customerList);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var result = customerService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // POST api/<CustomersController>
        [HttpPost]
        public IActionResult Post([FromBody] CustomerModel newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //failfast -> return
            //customerService.Add();
            var addedCustomer = customerService.AddCustomer(newCustomer);
            
            return CreatedAtAction("Get", new { id = addedCustomer.Custid }, addedCustomer);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //,Type=typeof(Customer) daca vreau sa adaug obiectul la return
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type=typeof(CustomerModel))]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, [FromBody] CustomerModel customerToUpdate)
        {
            //exists by 
            if (id!=customerToUpdate.Custid)
            {
                return BadRequest();
            }
            if (!customerService.Exists(id))
            {
                return NotFound();
            }
            customerService.UpdateCustomer(customerToUpdate);
            return NoContent();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!customerService.Exists(id))
            {
                return NotFound();
            }
            customerService.Delete(id);
            return NoContent();
        }
    }
}
