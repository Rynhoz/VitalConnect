using Microsoft.EntityFrameworkCore;
using VitalConnect_API.Data;
using Scalar.AspNetCore; //libreria de escalar

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<VT_DbContext>(options =>
    options.UseSqlite("Data Source=VitalConnect.db"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //Para probar en scalar
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
