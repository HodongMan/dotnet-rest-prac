using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace dotnet_todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoModelsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoModelsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoModel>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoModel>> GetTodoModel(long id)
        {
            var todoModel = await _context.TodoItems.FindAsync(id);

            if (todoModel == null)
            {
                return NotFound();
            }

            return todoModel;
        }

        // PUT: api/TodoModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoModel(long id, TodoModel todoModel)
        {
            if (id != todoModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TodoModel>> PostTodoModel(TodoModel todoModel)
        {
            _context.TodoItems.Add(todoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoModel", new { id = todoModel.Id }, todoModel);
            //return CreatedAtAction(nameof(GetTodoItem), new { id = todoModel.Id }, todoModel);
        }

        // DELETE: api/TodoModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoModel>> DeleteTodoModel(long id)
        {
            var todoModel = await _context.TodoItems.FindAsync(id);
            if (todoModel == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoModel);
            await _context.SaveChangesAsync();

            return todoModel;
        }

        private bool TodoModelExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
