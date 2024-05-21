using cw5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cw5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryTripsController : ControllerBase
{
    private readonly Context.Context _context;
    
    public CountryTripsController(Context.Context context)
    {
        _context = context;
    }

    // GET: api/CountryTrip
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country_Trip>>> GetCountryTrips()
    {
        return await _context.Country_Trip.ToListAsync();
    }

    // GET: api/CountryTrip/5/1
    [HttpGet("{idCountry}/{idTrip}")]
    public async Task<ActionResult<Country_Trip>> GetCountryTrip(int idCountry, int idTrip)
    {
        var countryTrip = await _context.Country_Trip
            .Where(ct => ct.IdCountry == idCountry && ct.IdTrip == idTrip)
            .FirstOrDefaultAsync();

        if (countryTrip == null)
        {
            return NotFound();
        }

        return countryTrip;
    }

    // PUT: api/CountryTrip/5/1
    [HttpPut("{idCountry}/{idTrip}")]
    public async Task<IActionResult> PutCountryTrip(int idCountry, int idTrip, Country_Trip countryTrip)
    {
        if (idCountry != countryTrip.IdCountry || idTrip != countryTrip.IdTrip)
        {
            return BadRequest();
        }

        _context.Entry(countryTrip).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CountryTripExists(idCountry, idTrip))
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

    // POST: api/CountryTrip
    [HttpPost]
    public async Task<ActionResult<Country_Trip>> PostCountryTrip(Country_Trip countryTrip)
    {
        _context.Country_Trip.Add(countryTrip);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCountryTrip", new { idCountry = countryTrip.IdCountry, idTrip = countryTrip.IdTrip }, countryTrip);
    }

    // DELETE: api/CountryTrip/5/1
    [HttpDelete("{idCountry}/{idTrip}")]
    public async Task<IActionResult> DeleteCountryTrip(int idCountry, int idTrip)
    {
        var countryTrip = await _context.Country_Trip
            .Where(ct => ct.IdCountry == idCountry && ct.IdTrip == idTrip)
            .FirstOrDefaultAsync();

        if (countryTrip == null)
        {
            return NotFound();
        }

        _context.Country_Trip.Remove(countryTrip);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CountryTripExists(int idCountry, int idTrip)
    {
        return _context.Country_Trip.Any(e => e.IdCountry == idCountry && e.IdTrip == idTrip);
    }
}