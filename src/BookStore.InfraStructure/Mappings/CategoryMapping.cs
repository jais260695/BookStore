using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.InfraStructure.Mappings
{

        public class CategoryMapping : IEntityTypeConfiguration<Category>
        {
            public void Configure(EntityTypeBuilder<Category> builder)
            {
                //The ‘HasKey’ it’s to set the Id property as the Primary Key.
                //If we do not define that, EF would also use the Id as the Primary Key, but with this configuration,
                //we can be sure that it will use exactly how we configured.
                builder.HasKey(c => c.Id);

                builder.Property(c => c.Name)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                // 1 : N => Category : Books
                //We also defined the 1 : N relationship. A category can have many books, and the Book needs to have one category,
                //and we set the CategoryId property (that is inside the Book class) as the Foreign Key of this relationship.
                builder.HasMany(c => c.Books)
                    .WithOne(b => b.Category)
                    .HasForeignKey(b => b.CategoryId);

                builder.ToTable("Categories");
            }
        }
    
}
