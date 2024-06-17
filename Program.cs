using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using PeliculaApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<PeliculaContext>(opt =>
    opt.UseInMemoryDatabase("PeliculaList"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles(); // Added to serve default files like index.html
app.UseStaticFiles();  // Added to serve static files

app.UseAuthorization();

app.MapControllers();

app.Run();
