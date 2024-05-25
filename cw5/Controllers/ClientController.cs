using cw5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cw5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly Context.Context _context;

    public ClientController(Context.Context context)
    {
        _context = context;
    }

    // GET: api/Client
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClients()
    {
        return await _context.Client.ToListAsync();
    }

    // GET: api/Client/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(int id)
    {
        var client = await _context.Client.FindAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        return client;
    }
    // PUT: api/Client/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClient(int id, Client client)
    {
        if (id != client.IdClient)
        {
            return BadRequest();
        }

        _context.Entry(client).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClientExists(id))
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
    // POST: api/Client
    [HttpPost]
    public async Task<ActionResult<Client>> PostClient(Client client)
    {
        _context.Client.Add(client);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
    }

    // DELETE: api/Client/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _context.Client.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        _context.Client.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool ClientExists(int id)
    {
        return _context.Client.Any(e => e.IdClient == id);
    }
}