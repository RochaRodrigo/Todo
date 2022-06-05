using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Commands;
using Todo.Data;
using Todo.Dto;
using Todo.Models;
using Todo.Services;

namespace Todo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TodoDataContext _context;

        public AuthenticationController(TodoDataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<CommandResult>> Authentication([FromBody] AuthenticationDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == model.Email && x.PasswordHash == model.PasswordHash);

                if (user == null)
                    return NotFound(new { message = "Usuário não encontrado na base de dados." });

                var token = TokenServices.GenerateToken(user);

                return new CommandResult(true, "Usuário autenticado com sucesso", new { User = user, Token = token });

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}