using Inventory_Management_WebAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// connection string added in this.
builder.Services.AddDbContext<InventryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

// to establish connection between angular and dot net (This is alternate code).
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp",
//        policy =>
//        {
//            policy
//                .WithOrigins("http://localhost:4200")
//                .AllowAnyHeader()
//                .AllowAnyMethod();
//        }
//    );
//});

// to establish connection between angular and dotNet.
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

var app = builder.Build(); // This will be already in code.

// to establish connection between angular and dotNet.
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

//app.UseCors("AllowAngularApp");

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
