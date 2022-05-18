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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        // GET: api/<CategorysController>
        [HttpGet]
        public ActionResult<IEnumerable<CategoryModel>> Get()
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

            var categoryList = categoryService.GetAllCategorys();
            return Ok(categoryList);
        }

        // GET api/<CategorysController>/5
        [HttpGet("{id:int}")]
        public ActionResult<CategoryModel> GetCategory(int id)
        {
            var result = categoryService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // POST api/<CategorysController>
        [HttpPost]
        public IActionResult Post([FromBody] CategoryModel newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //failfast -> return
            //categoryService.Add();
            var addedCategory = categoryService.AddCategory(newCategory);

            return CreatedAtAction("Get", new { id = addedCategory.Categoryid }, addedCategory);
        }

        // PUT api/<CategorysController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] //,Type=typeof(Category) daca vreau sa adaug obiectul la return
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CategoryModel))]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Put(int id, [FromBody] CategoryModel categoryToUpdate)
        {
            //exists by 
            if (id != categoryToUpdate.Categoryid)
            {
                return BadRequest();
            }
            if (!categoryService.Exists(id))
            {
                return NotFound();
            }
            categoryService.UpdateCategory(categoryToUpdate);
            return NoContent();
        }

        // DELETE api/<CategorysController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(CategoryModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CategoryModel))]
        public IActionResult Delete(int id)
        {
            if (!categoryService.Exists(id))
            {
                return NotFound();
            }
            categoryService.Delete(id);
            return NoContent();
        }
    }
}
