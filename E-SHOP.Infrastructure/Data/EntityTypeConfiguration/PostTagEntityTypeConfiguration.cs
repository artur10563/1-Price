﻿using Microsoft.EntityFrameworkCore;
using E_SHOP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_SHOP.Infrastructure.Data.EntityTypeConfiguration
{
	internal class PostTagEntityTypeConfiguration : IEntityTypeConfiguration<PostTag>
	{
		public void Configure(EntityTypeBuilder<PostTag> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Tag)
				.WithMany(x => x.Posts)
				.HasForeignKey(x => x.TagId);

			builder.HasOne(x => x.Post)
				.WithMany(x => x.Tags)
				.HasForeignKey(x => x.PostId);
		}
	}
}
