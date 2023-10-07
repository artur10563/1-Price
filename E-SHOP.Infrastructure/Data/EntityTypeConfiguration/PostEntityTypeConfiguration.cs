using E_SHOP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;


namespace E_SHOP.Infrastructure.Data.EntityTypeConfiguration
{
    internal class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title)
                .HasMaxLength(50)
                .HasAnnotation("MinLength", new MinLengthAttribute(3))
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(250)
                .HasAnnotation("MinLength", new MinLengthAttribute(50))
                .IsRequired();

            builder.Property(p => p.Price)
                .HasAnnotation("Range", new RangeAttribute(0, double.MaxValue)
                { ErrorMessage = "Price must be a non-negative value" })
                .IsRequired();

            builder.Property(p => p.Currency)
                .IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.IdCategory);

            builder.HasMany(p => p.Tags)
                .WithMany(t => t.Posts);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.ImgPath)
                .IsRequired(false);


            /*
			 * builder.HasOne(p=>p.User)
			 *	.WithMany(u=>u.Posts)
			 *	.HasForeignKey(p=>p.IdCreator);
			 */
        }
    }
}
