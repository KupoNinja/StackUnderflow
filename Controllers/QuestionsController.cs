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
        private readonly ResponsesService _rs;
        private readonly CategoriesService _cs;

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

        [HttpGet("{id}/responses")]
        public ActionResult<IEnumerable<Response>> GetAllResponses(string id)
        {
            try
            {
                return Ok(_rs.GetAllByQuestion(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Question> CreateQuestion([FromBody] Question questionData)
        {
            try
            {
                questionData.AuthorId = HttpContext.User.FindFirst("Id").Value;
                Question postedQuestion = _qs.AddQuestion(questionData);

                return Created("api/questions/" + postedQuestion.Id, postedQuestion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/categories")]
        public ActionResult<bool> AddCategoryToQuestion(string id, [FromBody] Category categoryData)
        {
            try
            {
                categoryData.Id = id;
                return Ok(_cs.AddCategoryToQuestion(categoryData.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<Question> UpdateQuestion(string id, [FromBody] Question questionData)
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

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteQuestion(string id)
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

        public QuestionsController(QuestionsService qs, ResponsesService rs, CategoriesService cs)
        {
            _qs = qs;
            _rs = rs;
            _cs = cs;
        }
    }
}