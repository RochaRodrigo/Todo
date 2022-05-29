using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly TodoDataContext _context;
        public TodoController(TodoDataContext context)
        {
            _context = context;
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> Get(int workspaceId)
        {
            try
            {
                var todos = await _context.TodoItems.AsNoTracking()
                .Where(x => x.WorkspaceId == workspaceId).ToListAsync();

                if (todos == null || todos.Count < 1)
                    return NotFound(new { message = "Nenhuma tarefa encontrada" });

                return Ok(todos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{userId:int}/{id:int}")]
        public async Task<ActionResult<TodoItem>> GetById(int workspaceId, int id)
        {
            try
            {
                var todo = await _context.TodoItems.AsNoTracking()
                .FirstOrDefaultAsync(x => x.WorkspaceId == workspaceId && x.Id == id);

                if (todo == null)
                    return NotFound(new { message = "Nenhuma tarefa encontrado" });

                return Ok(todo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post([FromBody] TodoItem model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _context.TodoItems.AddAsync(model);
                await _context.SaveChangesAsync();

                var result = new
                {
                    title = "Tarefa cadastrada com sucesso",
                    status = true,
                    data = model
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TodoItem>> Put(int id, [FromBody] TodoItem model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var todo = await _context.TodoItems.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.WorkspaceId == model.WorkspaceId);

                if (todo == null)
                    return NotFound(new { message = "Nenhuma tarefa encontrado" });

                _context.TodoItems.Update(model);
                await _context.SaveChangesAsync();

                var result = new
                {
                    title = "Tarefa atualizada com sucesso",
                    status = true,
                    data = model
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("Done/{id:int}")]
        public async Task<ActionResult<TodoItem>> Done(int id)
        {
            try
            {
                var todo = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
                if (todo == null)
                    return NotFound(new { message = "Tarefa não encontrada" });

                todo.Status = true;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Tarefa finalizada com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Undone/{id:int}")]
        public async Task<ActionResult<TodoItem>> UnDone(int id)
        {
            try
            {
                var todo = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
                if (todo == null)
                    return NotFound(new { message = "Tarefa não encontrada" });

                todo.Status = false;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Tarefa atualizada para pendente com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}