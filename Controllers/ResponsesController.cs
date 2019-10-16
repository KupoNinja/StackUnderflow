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
    public class ResponsesController : ControllerBase
    {
        private readonly ResponsesService _rs;

        [HttpGet]
        public ActionResult<IEnumerable<Response>> GetAll()
        {
            try
            {
                return Ok(_rs.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Response> GetById(string id)
        {
            try
            {
                return Ok(_rs.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Response> AddResponse([FromBody] Response response)
        {
            try
            {
                response.AuthorId = HttpContext.User.FindFirst("Id").Value;
                Response postedResponse = _rs.AddResponse(response);

                return Created("api/questions/" + postedResponse.Id, postedResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Response> EditResponse(string id, [FromBody] Response responseData)
        {
            try
            {
                responseData.Id = id;
                return Ok(_rs.UpdateResponse(responseData));
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
                return Ok(_rs.DeleteResponse(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public ResponsesController(ResponsesService rs)
        {
            _rs = rs;
        }
    }
}