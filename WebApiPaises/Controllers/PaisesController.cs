using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Context;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly WebApiPaisesContext _context;

        public PaisesController(WebApiPaisesContext context)
        {
            _context = context;
        }

        // GET: api/Paises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPais()
        {
            return await _context.Pais.Include(x=>x.Estados).ToListAsync();
        }

        // GET: api/Paises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pais>> GetPais(int id)
        {
            var pais = await _context.Pais.Include(x => x.Estados).FirstOrDefaultAsync(x=>x.Id==id);

            if (pais == null)
            {
                return NotFound();
            }

            return pais;
        }

        // PUT: api/Paises/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPais(int id, Pais pais)
        {
            if (id != pais.Id)
            {
                return BadRequest();
            }

            _context.Entry(pais).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaisExists(id))
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

        // POST: api/Paises
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pais>> PostPais(Pais pais)
        {
            _context.Pais.Add(pais);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPais", new { id = pais.Id }, pais);
        }

        // DELETE: api/Paises/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pais>> DeletePais(int id)
        {
            var pais = await _context.Pais.Include(x => x.Estados).FirstOrDefaultAsync(x => x.Id == id);
            if (pais == null)
            {
                return NotFound();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in pais.Estados)
                    {
                        _context.Estados.Remove(item);
                    }

                    _context.Pais.Remove(pais);

                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {

                    transaction.Rollback();
                }
            }
            return NoContent();
        }
        [HttpGet("{id}/estados")]
        public async Task<ActionResult> GetEstados(int id)
        {
            var pais = await _context.Pais.Include(x => x.Estados).FirstOrDefaultAsync(x => x.Id == id);

            if (pais == null)
                return NotFound();

            return Ok(pais);
        }
        private bool PaisExists(int id)
        {
            return _context.Pais.Any(e => e.Id == id);
        }
    }
}
