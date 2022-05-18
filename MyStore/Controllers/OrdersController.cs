using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
using MyStore.Services;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService productService)
        {
            this.orderService = productService;
        }

        [HttpGet]

        public ActionResult<IEnumerable<OrderModel>> Get([FromQuery] List<string> listOfTowns, [FromQuery] string townsStringList) //http://localhost:5000/api/orders?listOfTowns=Warszawa&listOfTowns=Reims&townsStringList=Warszawa,Reims
        {
            var orderList = orderService.GetAll(listOfTowns,townsStringList);
            return Ok(orderList);
        }
        [HttpGet]
        [Route("GetOrdersByCountry/{country}")]
        public ActionResult<IEnumerable<OrderModel>> Get(string country)   //http://localhost:5000/api/orders/GetOrdersByCountry/Poland
        {
            var orderList = orderService.GetAll(country);
            return Ok(orderList);
        }
        //Exemplu Irinei: (string? city, List<string>? country,Shippers  shipper)

        //public IEnumerable<Order> Get([FromQuery] List<string> listOfTowns, [FromQuery] string country)
        //{
        //    if (listOfTowns==null)
        //    {
        //        var orderList = orderService.GetAll(country);
        //        return orderList;
        //    } else
        //    {
        //      var orderList = orderService.GetAll(listOfTowns);
        //      return orderList;
        //    }
        //}


        // GET api/<OrdersController>/5
        [HttpGet("{id:int}")]
        public ActionResult<OrderModel> GetOrder(int id)
        {
            var result = orderService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        //POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderModel newOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //failfast -> return
            //orderService.Add();
            var addedOrder = orderService.AddOrder(newOrder);

            return CreatedAtAction("Post", new { id = addedOrder.Orderid }, addedOrder);
            //return CreatedAtAction("Post", newOrder,new { id = newOrder.Orderid });
        }
        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //,Type=typeof(Order) daca vreau sa adaug obiectul la return
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(OrderModel))]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, [FromBody] OrderModel orderToUpdate)
        {
            //exists by 
            if (id != orderToUpdate.Orderid)
            {
                return BadRequest();
            }
            if (!orderService.Exists(id))
            {
                return NotFound();
            }
            orderService.UpdateOrder(orderToUpdate);
            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!orderService.Exists(id))
            {
                return NotFound();
            }
            orderService.Delete(id);
            return NoContent();
        }
    }
}
