using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Models;
using StackUnderflow.Services;

namespace StackUnderflow.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesService _cs;

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            try
            {
                return Ok(_cs.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetById(string id)
        {
            try
            {
                return Ok(_cs.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Category> Create([FromBody] Category category)
        {
            try
            {
                Category postedCategory = _cs.AddCategory(category);

                return Created("api/questions/" + postedCategory.Id, postedCategory);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Category> Edit(string id, [FromBody] Category categoryData)
        {
            try
            {
                categoryData.Id = id;
                return Ok(_cs.UpdateCategory(categoryData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(string id)
        {
            try
            {
                return Ok(_cs.DeleteCategory(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public CategoriesController(CategoriesService cs)
        {
            _cs = cs;
        }
    }
}