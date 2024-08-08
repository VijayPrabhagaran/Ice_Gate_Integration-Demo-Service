using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IceGate_Demo.Dbcontext;
using IceGate_Demo.Entities;

namespace IceGate_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationResponsesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IntegrationResponsesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/IntegrationResponses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntegrationResponse>>> GetIntegrationResponse()
        {
            return await _context.IntegrationResponse.ToListAsync();
        }

        // GET: api/IntegrationResponses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IntegrationResponse>> GetIntegrationResponse(int id)
        {
            var integrationResponse = await _context.IntegrationResponse.FindAsync(id);

            if (integrationResponse == null)
            {
                return NotFound();
            }

            return integrationResponse;
        }

        // PUT: api/IntegrationResponses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntegrationResponse(int id, IntegrationResponse integrationResponse)
        {
            if (id != integrationResponse.IntegrationResponseId)
            {
                return BadRequest();
            }

            _context.Entry(integrationResponse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntegrationResponseExists(id))
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

        // POST: api/IntegrationResponses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IntegrationResponse>> PostIntegrationResponse(IntegrationResponse integrationResponse)
        {
            _context.IntegrationResponse.Add(integrationResponse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIntegrationResponse", new { id = integrationResponse.IntegrationResponseId }, integrationResponse);
        }

        // DELETE: api/IntegrationResponses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntegrationResponse(int id)
        {
            var integrationResponse = await _context.IntegrationResponse.FindAsync(id);
            if (integrationResponse == null)
            {
                return NotFound();
            }

            _context.IntegrationResponse.Remove(integrationResponse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IntegrationResponseExists(int id)
        {
            return _context.IntegrationResponse.Any(e => e.IntegrationResponseId == id);
        }
    }
}
