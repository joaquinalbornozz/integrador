using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculaApi.Models;

namespace PeliculaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeliculaItemsController : ControllerBase
{
    private readonly PeliculaContext _context;

    public PeliculaItemsController(PeliculaContext context)
    {
        _context = context;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PeliculaItemDTO>>> GetPeliculaItems()
    {
        return await _context.PeliculaItems
            .Select(x => ItemToDTO(x))
            .ToListAsync();
    }

    // GET: api/TodoItems/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<ActionResult<PeliculaItemDTO>> GetPeliculaItem(long id)
    {
        var peliculaItem = await _context.PeliculaItems.FindAsync(id);

        if (peliculaItem == null)
        {
            return NotFound();
        }

        return ItemToDTO(peliculaItem);
    }
    // </snippet_GetByID>

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPeliculaItem(long id, PeliculaItemDTO peliculaDTO)
    {
        if (id != peliculaDTO.Id)
        {
            return BadRequest();
        }

        var peliculaItem = await _context.PeliculaItems.FindAsync(id);
        if (peliculaItem == null)
        {
            return NotFound();
        }

        peliculaItem.Name = peliculaDTO.Name;
        peliculaItem.IsComplete = peliculaDTO.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!PeliculaItemExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }
    // </snippet_Update>

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult<PeliculaItemDTO>> PostTodoItem(PeliculaItemDTO peliculaDTO)
    {
        var peliculaItem = new PeliculaItem
        {
            IsComplete = peliculaDTO.IsComplete,
            Name = peliculaDTO.Name,
            Description= peliculaDTO.Description
        };

        _context.PeliculaItems.Add(peliculaItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPeliculaItem),
            new { id = peliculaItem.Id },
            ItemToDTO(peliculaItem));
    }
    // </snippet_Create>

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePeliculaItem(long id)
    {
        var peliculaItem = await _context.PeliculaItems.FindAsync(id);
        if (peliculaItem == null)
        {
            return NotFound();
        }

        _context.PeliculaItems.Remove(peliculaItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PeliculaItemExists(long id)
    {
        return _context.PeliculaItems.Any(e => e.Id == id);
    }

    private static PeliculaItemDTO ItemToDTO(PeliculaItem peliculaItem) =>
       new PeliculaItemDTO
       {
           Id = peliculaItem.Id,
           Name = peliculaItem.Name,
           Description =peliculaItem.Description,
           IsComplete = peliculaItem.IsComplete
       };
}