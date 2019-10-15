using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using StackUnderflow.Data;
using StackUnderflow.Models;

namespace StackUnderflow.Services
{
    public class ResponsesService
    {
        private readonly ResponsesRepository _repo;

        public List<Response> GetAll()
        {
            var responses = _repo.GetAll().ToList();
            if (responses == null) { throw new Exception("No one wants to help you. There are no responses."); }

            return responses;
        }

        public Response GetById(string id)
        {
            var response = _repo.GetById(id);

            return response;
        }

        public List<Response> GetAllByQuestion(string id)
        {
            var responses = _repo.GetAllByQuestion(id).ToList();
            if (responses == null) { throw new Exception("No one wants to help you. There are no responses."); }

            return responses;
        }

        public Response AddResponse(Response responseData)
        {
            responseData.Id = Guid.NewGuid().ToString();
            responseData.DateReplied = DateTime.Now;
            var postedResponse = _repo.Create(responseData);

            return postedResponse;
        }

        public Response UpdateResponse(Response responseData)
        {
            var response = _repo.GetById(responseData.Id);
            response.Body = responseData.Body;
            response.DateEdited = DateTime.Now;
            var updatedResponse = _repo.Edit(response);

            return updatedResponse;
        }

        public string DeleteResponse(string id)
        {
            var response = _repo.GetById(id);
            var deleted = _repo.Delete(response.Id);
            if (!deleted) { throw new Exception("This response too STRONK! Unable to delete the response."); }

            return id;
        }

        public ResponsesService(ResponsesRepository repo)
        {
            _repo = repo;
        }
    }
}