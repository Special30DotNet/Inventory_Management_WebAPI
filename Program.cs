<<<<<<< Updated upstream
=======
using Inventory_Management_WebAPI;
using Inventory_Management_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

>>>>>>> Stashed changes
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

<<<<<<< Updated upstream
var app = builder.Build();
=======
builder.Services.AddDbContext<InventoryDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));




//to establish connection between angular and dotnet
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>

        {
            builder.WithOrigins("*")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
        });

});
var app = builder.Build();
//to establish connection between angular and dotnet
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});


>>>>>>> Stashed changes

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
