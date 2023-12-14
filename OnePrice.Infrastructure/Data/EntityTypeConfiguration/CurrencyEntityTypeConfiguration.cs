using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnePrice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Infrastructure.Data.EntityTypeConfiguration
{
	internal class CurrencyEntityTypeConfiguration : IEntityTypeConfiguration<Currency>
	{
		public void Configure(EntityTypeBuilder<Currency> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnAdd();

			builder.Property(x => x.LastModifiedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnUpdate()
				.IsRequired(false);

			builder.Property(x => x.Symbol)
				.HasMaxLength(1)
				.IsRequired(true);

			builder.Property(x => x.FullName)
				.HasMaxLength(20)
				.IsRequired(true);

			builder.Property(x => x.Code)
				.HasMaxLength(3);

			builder.HasMany(c => c.Posts)
				.WithOne(p => p.Currency)
				.HasForeignKey(p => p.CurrencyId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
