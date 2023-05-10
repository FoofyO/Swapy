using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;

namespace Swapy.DAL.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(r => r.Token);

            builder.Property(r => r.Token).IsRequired();

            builder.HasOne(r => r.User)
                   .WithOne(u => u.RefreshToken)
                   .HasForeignKey<User>(u => u.RefreshTokenId)
                   .IsRequired();
        }
    }
}
