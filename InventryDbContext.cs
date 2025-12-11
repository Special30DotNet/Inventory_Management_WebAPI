using Inventory_Management_WebAPI;
using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System.Runtime.Intrinsics.X86;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


/*
    * What is DbContext?
       A class that manages:
        = Database connection
        = Maps tables using DbSets
        = Runs queries using EF Core
    */
//internal class InventryDbContext :DbContext
namespace Inventory_Management_WebAPI
{
    public class InventoryDbContext : DbContext     
    {

//You are creating a new class called InventoryDbContext which inherits from DbContext.
//DbContext is the main class used by Entity Framework Core to:
//open a connection to the database
//map C# classes to database tables
//perform CRUD operations(Create, Read, Update, Delete)
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options) { }

// This constructor receives options from Dependency Injection, usually provided in Program.cs.
//The constructor passes these options to the base DbContext.
//Options include things like:
//connection string
//database provider(SQL Server, MySQL, etc.)

        public DbSet<EconomicModels> tblProductMasters { get; set; }   // DbSet : to map the table
// This line tells Entity Framework:
//EconomicModels → this C# class represents one table
//tblProductMasters → the name EF will use to create or query that table
//In EF Core:
//A DbSet represents a database table
//A model class (EconomicModels) represents a row/record in the table
    }
}