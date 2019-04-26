using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorSampleApp.Api.Filters;
using BlazorSampleApp.Api.Models.Data;
using BlazorSampleApp.Api.Models.Dto;
using BlazorSampleApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorSampleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public DataTable<TodoDataModel> Todos { get; }
        public Database Database { get; }

        public TodoController(DataTable<TodoDataModel> todos, Database database)
        {
            Todos = todos;
            Database = database;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TodoDataModel>> Get()
        {
            return Ok(Todos.AsNoTracking());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<TodoDataModel> Get(string id)
        {
            var todoResult = Todos.FirstOrDefault(t => t.Id == id);

            if (todoResult is null)
            {
                return NotFound();
            }

            return todoResult;
        }

        // POST api/values
        [HttpPost, SaveOnSuccess]
        public void Post([FromBody] TodoPostDtoModel model)
        {
            var newTodo = new TodoDataModel
            {
                Id = model.Id ?? Guid.NewGuid().ToString(),
                CreatedOn = model.CreatedOn ?? DateTime.UtcNow,
                CompletedOn = model.CompletedOn,
                Text = model.Text
            };

            Todos.Data.Add(newTodo);
        }

        // PUT api/values/5
        [HttpPatch("{id}"), SaveOnSuccess]
        public ActionResult Patch(string id, [FromBody] TodoPatchDtoModel model)
        {
            var matchingTodo = Todos.FirstOrDefault(t => t.Id == id);

            if (matchingTodo is null)
            {
                return NotFound();
            }

            if (model.Text != null)
            {
                matchingTodo.Text = model.Text;
            }

            if (matchingTodo.CompletedOn is null)
            {
                matchingTodo.CompletedOn = model.CompletedOn;
            }

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}"), SaveOnSuccess]
        public void Delete(string id)
        {
            var matchingTodo = Todos.FirstOrDefault(t => t.Id == id);

            if (matchingTodo is null)
            {
                return;
            }

            Todos.Data.Remove(matchingTodo);
        }
    }
}
