﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swapy.Common.Entities;

namespace Swapy.DAL.Configurations
{
    public class UserLikeConfiguration : IEntityTypeConfiguration<UserLike>
    {
        public void Configure(EntityTypeBuilder<UserLike> builder)
        {
            builder.ToTable("UsersLikes");
            builder.HasKey(ul => ul.Id);

            builder.Property(ul => ul.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.HasOne(ul => ul.Like)
                   .WithOne(l => l.UserLike)
                   .HasForeignKey<Like>(l => l.UserLikeId)
                   .IsRequired();

            builder.HasOne(ul => ul.Recipient)
                   .WithMany(s => s.LikesRecipient)
                   .HasForeignKey(ul => ul.RecipientId)
                   .IsRequired();
        }
    }
}
