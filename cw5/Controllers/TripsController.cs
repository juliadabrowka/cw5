using cw5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cw5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    private readonly Context.Context _context;

    public TripController(Context.Context context)
    {
        _context = context;
    }

    // GET: api/Trip
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
    {
        return await _context.Trip.ToListAsync();
    }

    // GET: api/Trip/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Trip>> GetTrip(int id)
    {
        var trip = await _context.Trip.FindAsync(id);

        if (trip == null)
        {
            return NotFound();
        }

        return trip;
    }

    // PUT: api/Trip/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTrip(int id, Trip trip)
    {
        if (id != trip.IdTrip)
        {
            return BadRequest();
        }

        _context.Entry(trip).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TripExists(id))
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

    // POST: api/Trip
    [HttpPost]
    public async Task<ActionResult<Trip>> PostTrip(Trip trip)
    {
        _context.Trip.Add(trip);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTrip", new { id = trip.IdTrip }, trip);
    }

    // DELETE: api/Trip/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrip(int id)
    {
        var trip = await _context.Trip.FindAsync(id);
        if (trip == null)
        {
            return NotFound();
        }

        _context.Trip.Remove(trip);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TripExists(int id)
    {
        return _context.Trip.Any(e => e.IdTrip == id);
    }
}