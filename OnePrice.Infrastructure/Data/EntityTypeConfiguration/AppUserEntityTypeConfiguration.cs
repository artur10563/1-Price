﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnePrice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Infrastructure.Data.EntityTypeConfiguration
{
	internal class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnAdd();

			builder.Property(x => x.LastModifiedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnUpdate()
				.IsRequired(false);

			builder.Property(x => x.Nickname)
				.HasMaxLength(50)
				.IsRequired();

			builder.Property(x => x.PhoneNumber)
				.HasMaxLength(15)
				.IsRequired(true);

			builder.Property(x => x.UserName)
				.HasMaxLength(50)
				.IsRequired(true);

			builder.Property(x => x.NormalizedUserName)
				.HasMaxLength(50);

			builder.Property(x => x.Nickname)
				.HasMaxLength(50)
				.IsRequired(true);



			//TODO: add better configuration for phone and email


			builder.Property(c => c.ImgPath)
				   .IsRequired(false);

		}
	}
}
