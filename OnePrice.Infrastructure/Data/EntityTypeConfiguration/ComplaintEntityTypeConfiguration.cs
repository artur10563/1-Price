using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnePrice.Domain.Entities;


namespace OnePrice.Infrastructure.Data.EntityTypeConfiguration
{
	internal class ComplaintEntityTypeConfiguration : IEntityTypeConfiguration<Complaint>
	{
		public void Configure(EntityTypeBuilder<Complaint> builder)
		{

			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnAdd();

			builder.Property(x => x.LastModifiedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnUpdate()
				.IsRequired(false);

			builder.Property(x => x.Content)
				.HasMaxLength(255)
				.IsRequired(true);

			builder.HasOne(x => x.Post)
				.WithMany(p => p.Complaints)
				.HasForeignKey(x => x.PostId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(x => x.Author)
				.WithMany(a => a.Complaints)
				.HasForeignKey(x => x.AuthorId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);

			builder.HasOne(x => x.Status)
				.WithMany(s => s.Complaints)
				.HasForeignKey(x => x.StatusId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(x => x.Type)
				.WithMany(t => t.Complaints)
				.HasForeignKey(x => x.TypeId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}