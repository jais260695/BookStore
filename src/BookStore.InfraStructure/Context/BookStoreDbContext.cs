using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.InfraStructure.Context
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions  options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //By default, if we forgot to define the size of any property of type string, it will be created as nvarchar(max), to avoid that,
            //we added a configuration that will create the size of the column in the database as varchar(150) for the columns of type string that were not mapped.
            //This is good to use in projects which you will have more classes.
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(150)");
            }

            //will access the BookStoreDbContext, and it will see all the entities that are mapping inside of the BookStoreDbContext
            //and search the classes that inherit from the IEntityTypeConfiguration, and then it will register all of them.
            //So for each mapping class that we had configured, it will be registered through this command.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreDbContext).Assembly);

            //we added the code to disable the cascade deletion.And that is because we do not want that when we delete one record from one table,
            //the relational data in the other table be removed too. We will have validation for that in our code later.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull; 
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
