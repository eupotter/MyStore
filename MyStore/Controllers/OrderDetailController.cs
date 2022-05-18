using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
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
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            this.orderDetailService = orderDetailService;
        }

        // GET: api/<OrderDetailsController>
        [HttpGet]
        public ActionResult<IEnumerable<OrderDetailModel>> Get()
        {
            string[] games = { "Morrowind", "BioShock", "Daxter", "The Darkness", "Half Life", "System Shock 2" };

            IEnumerable<string> subset =
                from g in games
                where g.Length > 6
                && g.Substring(0, 1) == "M"
                orderby g
                select g;
            IEnumerable<string> subset2 =
                games.Where(x => x.Length > 6)
                .OrderBy(x => x)
                .Select(x => x);

            var orderDetailList = orderDetailService.GetAllOrderDetails();
            return Ok(orderDetailList);
        }

        // GET api/<OrderDetailsController>/5
        [HttpGet("{id:int}")]
        public ActionResult<OrderDetailModel> GetOrderDetail(int id)
        {
            var result = orderDetailService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // POST api/<OrderDetailsController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDetailModel newOrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //failfast -> return
            //orderDetailService.Add();
            var addedOrderDetail = orderDetailService.AddOrderDetail(newOrderDetail);

            return CreatedAtAction("Get", new { id = addedOrderDetail.Orderid }, addedOrderDetail);
        }

        // PUT api/<OrderDetailsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //,Type=typeof(OrderDetail) daca vreau sa adaug obiectul la return
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(OrderDetailModel))]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, [FromBody] OrderDetailModel orderDetailToUpdate)
        {
            //exists by 
            if (id != orderDetailToUpdate.Orderid)
            {
                return BadRequest();
            }
            if (!orderDetailService.Exists(id))
            {
                return NotFound();
            }
            orderDetailService.UpdateOrderDetail(orderDetailToUpdate);
            return NoContent();
        }

        // DELETE api/<OrderDetailsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(OrderDetailModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(OrderDetailModel))]
        public IActionResult Delete(int id)
        {
            if (!orderDetailService.Exists(id))
            {
                return NotFound();
            }
            orderDetailService.Delete(id);
            return NoContent();
        }
    }
}
