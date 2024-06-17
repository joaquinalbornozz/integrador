namespace PeliculaApi.Models;

public class PeliculaItemDTO
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; }

}