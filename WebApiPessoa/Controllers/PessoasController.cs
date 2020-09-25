using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPessoa.Context;
using WebApiPessoa.Models;

namespace WebApiPessoa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly WebApiPessoaContext _context;
        private readonly IMapper mapper;

        public PessoasController(WebApiPessoaContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Pessoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoa()
        {
            var pessoas = await _context.Pessoa.ToListAsync();
            var response = mapper.Map<List<PessoaResponse>>(pessoas);
            return Ok(response);
        }

        // GET: api/Pessoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }

        // PUT: api/Pessoas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
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

        // POST: api/Pessoas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
        {
            _context.Pessoa.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPessoa", new { id = pessoa.Id }, pessoa);
        }

        // DELETE: api/Pessoas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pessoa>> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoa.Remove(pessoa);
            await _context.SaveChangesAsync();

            return pessoa;
        }

        [HttpGet("{id}/amigos")]
        public async Task<ActionResult> GetAmigos(int id)
        {
            var pessoa = await _context.Pessoa.Include(x => x.Amigos).FirstOrDefaultAsync(x => x.Id == id);

            if (pessoa == null)
                return NotFound();

            var response = mapper.Map<List<PessoaResponse>>(pessoa.Amigos);

            return Ok(response);
        }

        [HttpPost("{id}/amigos")]
        public async Task<ActionResult> PostAmigos([FromRoute] int id, [FromBody] PostAmigosRequest request)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);

            if (pessoa == null)
                return NotFound();

            var amigos = await _context.Pessoa.Where(x => request.Ids.Contains(x.Id)).ToListAsync();

            pessoa.Amigos = amigos;

            _context.Pessoa.Update(pessoa);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.Id == id);
        }
    }
}
