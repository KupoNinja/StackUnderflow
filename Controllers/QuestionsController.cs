using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Models;
using StackUnderflow.Services;

namespace StackUnderflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionsService _qs;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Question>> GetAll()
        {
            try
            {
                return Ok(_qs.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }

        public QuestionsController(QuestionsService qs)
        {
            _qs = qs;
        }
    }
}