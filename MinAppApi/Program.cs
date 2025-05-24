using Microsoft.EntityFrameworkCore;
using MinAppApi.Data;
using MinAppApi.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<MiniAppApiDbContext>(options =>
{
    options.UseSqlServer("Server=LAPTOP-Q4SUHALF\\SQLEXPRESS;Database=MiniAppDb;Trusted_Connection=True;TrustServerCertificate=True;");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
