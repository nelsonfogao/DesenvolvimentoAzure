using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using Domain;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigosController : ControllerBase
    {
        private readonly WebApiContext _context;

        public AmigosController(WebApiContext context)
        {
            _context = context;
        }

        // GET: api/Amigos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amigo>>> GetAmigo()
        {
            return await _context.Amigo.FromSqlRaw("exec BuscarAmigo").ToListAsync();
        }

        // GET: api/Amigos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amigo>> GetAmigo(int id)
        {
            var amigo = await _context.Amigo.FindAsync(id);

            if (amigo == null)
            {
                return NotFound();
            }

            return amigo;
        }

        // PUT: api/Amigos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmigo(int id, Amigo amigo)
        {
            if (id != amigo.Id)
            {
                return BadRequest();
            }

            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                $"exec AlterarAmigo {id}, {amigo.Nome}, {amigo.Sobrenome}, {amigo.Email}, {amigo.Telefone}, {amigo.DataDeAniversario}");

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmigoExists(id))
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

        // POST: api/Amigos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Amigo>> PostAmigo(Amigo amigo)
        {
             await _context.Database.ExecuteSqlInterpolatedAsync(
                $"exec AdicionarAmigo {amigo.Nome}, {amigo.Sobrenome}, {amigo.Email}, {amigo.Telefone}, {amigo.DataDeAniversario}");

            return CreatedAtAction("GetAmigo", new { id = amigo.Id }, amigo);
        }

        // DELETE: api/Amigos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amigo>> DeleteAmigo(int id)
        {
            var amigo = await _context.Amigo.FindAsync(id);
            if (amigo == null)
            {
                return NotFound();
            }

            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"exec ExcluirAmigo {id}");

            return amigo;
        }

        private bool AmigoExists(int id)
        {
            return _context.Amigo.Any(e => e.Id == id);
        }
    }
}
