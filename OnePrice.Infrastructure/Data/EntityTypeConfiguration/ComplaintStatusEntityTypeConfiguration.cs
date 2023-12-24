using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnePrice.Domain.Entities;


namespace OnePrice.Infrastructure.Data.EntityTypeConfiguration
{
	internal class ComplaintStatusEntityTypeConfiguration : IEntityTypeConfiguration<ComplaintStatus>
	{
		public void Configure(EntityTypeBuilder<ComplaintStatus> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnAdd();

			builder.Property(x => x.LastModifiedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnUpdate()
				.IsRequired(false);

			builder.Property(x => x.Name)
				.HasMaxLength(50)
				.IsRequired(true);
		}
	}
}
