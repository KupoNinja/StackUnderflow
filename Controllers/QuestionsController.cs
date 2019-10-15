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
        public ActionResult<Question> GetById(string id)
        {
            try
            {
                return Ok(_qs.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Question> Post([FromBody] Question question)
        {
            try
            {
                question.AuthorId = HttpContext.User.FindFirst("Id").Value;
                Question postedQuestion = _qs.AddQuestion(question);

                return Created("api/questions/" + postedQuestion.Id, postedQuestion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Question> Put(string id, [FromBody] Question questionData)
        {
            try
            {
                questionData.Id = id;
                return Ok(_qs.UpdateQuestion(questionData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(string id)
        {
            try
            {
                return Ok(_qs.DeleteQuestion(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public QuestionsController(QuestionsService qs)
        {
            _qs = qs;
        }
    }
}