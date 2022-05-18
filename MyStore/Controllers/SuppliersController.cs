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
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        // GET: api/<SuppliersController>
        [HttpGet]
        public ActionResult<IEnumerable<SupplierModel>> Get()
        {
            var supplierList = supplierService.GetAllSuppliers();
            return Ok(supplierList);
        }

        // GET api/<SuppliersController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Supplier> GetSupplier(int id)
        {
            var result = supplierService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // POST api/<SuppliersController>
        [HttpPost]
        public IActionResult Post([FromBody] SupplierModel newSupplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //failfast -> return
            //supplierService.Add();
            var addedSupplier = supplierService.AddSupplier(newSupplier);
            
            return CreatedAtAction("Get", new { id = addedSupplier.Supplierid }, addedSupplier);
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //,Type=typeof(Supplier) daca vreau sa adaug obiectul la return
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type=typeof(SupplierModel))]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, [FromBody] SupplierModel supplierToUpdate)
        {
            //exists by 
            if (id!=supplierToUpdate.Supplierid)
            {
                return BadRequest();
            }
            if (!supplierService.Exists(id))
            {
                return NotFound();
            }
            supplierService.UpdateSupplier(supplierToUpdate);
            return NoContent();
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!supplierService.Exists(id))
            {
                return NotFound();
            }
            supplierService.Delete(id);
            return NoContent();
        }
    }
}
