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
    public class WorkspaceController : ControllerBase
    {
        private readonly TodoDataContext _context;

        public WorkspaceController(TodoDataContext context)
        {
            _context = context;
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult<IEnumerable<Workspace>>> Get(int userId)
        {
            try
            {
                var workspaces = await _context.Workspaces.AsNoTracking()
                .Where(x => x.UserId == userId).Include(x => x.Todos).Include(x => x.User).ToListAsync();

                if (workspaces == null || workspaces.Count < 1)
                    return NotFound(new { message = "Nenhuma area de trabalho encontrada" });

                return Ok(workspaces);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Workspace>> Post([FromBody] Workspace model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _context.Workspaces.AddAsync(model);
                await _context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId:int}/{id:int}")]
        public async Task<ActionResult<Workspace>> Get(int userId, int id)
        {
            try
            {
                var workspace = await _context.Workspaces.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

                if (workspace == null)
                    return NotFound(new { message = "Nenhuma area de trabalho encontrada" });

                return Ok(workspace);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId:int}/{id:int}")]
        public async Task<ActionResult<Workspace>> Put([FromBody] Workspace model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var workspace = await _context.Workspaces.FirstOrDefaultAsync(x => x.Id == id);

                if (workspace == null)
                    return NotFound(new { message = "Nenhuma area de trabalho encontrada" });

                workspace = model;

                _context.Workspaces.Update(workspace);
                await _context.SaveChangesAsync();

                return Ok(workspace);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId:int}/{id:int}")]
        public async Task<ActionResult<Workspace>> Delete(int userId, int id)
        {
            try
            {
                var workspace = await _context.Workspaces
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

                if (workspace == null)
                    return NotFound(new { message = "Nenhuma area de trabalho encontrada" });

                _context.Workspaces.Remove(workspace);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Area de trabalho deletada com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}