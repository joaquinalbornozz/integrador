using Microsoft.EntityFrameworkCore;

namespace PeliculaApi.Models;

public class PeliculaContext : DbContext
{
    public PeliculaContext(DbContextOptions<PeliculaContext> options)
        : base(options)
    {
    }

    public DbSet<PeliculaItem> PeliculaItems { get; set; } = null!;
}