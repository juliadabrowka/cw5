using cw5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cw5.Controllers;

[Route("api/[controller]")]
    [ApiController]
    public class ClientTripController : ControllerBase
    {
        private readonly Context.Context _context;

        public ClientTripController(Context.Context context)
        {
            _context = context;
        }

        // GET: api/ClientTrip
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client_Trip>>> GetClientTrips()
        {
            return await _context.Client_Trip.ToListAsync();
        }

        // GET: api/ClientTrip/5/1
        [HttpGet("{idClient}/{idTrip}")]
        public async Task<ActionResult<Client_Trip>> GetClientTrip(int idClient, int idTrip)
        {
            var clientTrip = await _context.Client_Trip
                .Where(ct => ct.IdClient == idClient && ct.IdTrip == idTrip)
                .FirstOrDefaultAsync();

            if (clientTrip == null)
            {
                return NotFound();
            }

            return clientTrip;
        }

        // PUT: api/ClientTrip/5/1
        [HttpPut("{idClient}/{idTrip}")]
        public async Task<IActionResult> PutClientTrip(int idClient, int idTrip, Client_Trip clientTrip)
        {
            if (idClient != clientTrip.IdClient || idTrip != clientTrip.IdTrip)
            {
                return BadRequest();
            }

            _context.Entry(clientTrip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientTripExists(idClient, idTrip))
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

        // POST: api/ClientTrip
        [HttpPost]
        public async Task<ActionResult<Client_Trip>> PostClientTrip(Client_Trip clientTrip)
        {
            _context.Client_Trip.Add(clientTrip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientTrip", new { idClient = clientTrip.IdClient, idTrip = clientTrip.IdTrip }, clientTrip);
        }

        // DELETE: api/ClientTrip/5/1
        [HttpDelete("{idClient}/{idTrip}")]
        public async Task<IActionResult> DeleteClientTrip(int idClient, int idTrip)
        {
            var clientTrip = await _context.Client_Trip
                .Where(ct => ct.IdClient == idClient && ct.IdTrip == idTrip)
                .FirstOrDefaultAsync();

            if (clientTrip == null)
            {
                return NotFound();
            }

            _context.Client_Trip.Remove(clientTrip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientTripExists(int idClient, int idTrip)
        {
            return _context.Client_Trip.Any(e => e.IdClient == idClient && e.IdTrip == idTrip);
        }
    }