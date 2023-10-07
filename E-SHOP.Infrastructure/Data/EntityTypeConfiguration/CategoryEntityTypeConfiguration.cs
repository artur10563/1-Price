using E_SHOP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace E_SHOP.Infrastructure.Data.EntityTypeConfiguration
{
    internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .HasAnnotation("MinLength", new MinLengthAttribute(3))
                .IsRequired();


            builder.Property(c => c.ImgPath)
                   .IsRequired(false);

        }
    }
}
