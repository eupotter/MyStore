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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeModel>> Get()
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

            var employeeList = employeeService.GetAllEmployees();
            return Ok(employeeList);
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id:int}")]
        public ActionResult<EmployeeModel> GetEmployee(int id)
        {
            var result = employeeService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeModel newEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //failfast -> return
            //employeeService.Add();
            var addedEmployee = employeeService.AddEmployee(newEmployee);

            return CreatedAtAction("Get", new { id = addedEmployee.Empid }, addedEmployee);
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //,Type=typeof(Employee) daca vreau sa adaug obiectul la return
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EmployeeModel))]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, [FromBody] EmployeeModel employeeToUpdate)
        {
            //exists by 
            if (id != employeeToUpdate.Empid)
            {
                return BadRequest();
            }
            if (!employeeService.Exists(id))
            {
                return NotFound();
            }
            employeeService.UpdateEmployee(employeeToUpdate);
            return NoContent();
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EmployeeModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(EmployeeModel))]
        public IActionResult Delete(int id)
        {
            if (!employeeService.Exists(id))
            {
                return NotFound();
            }
            employeeService.Delete(id);
            return NoContent();
        }
    }
}
