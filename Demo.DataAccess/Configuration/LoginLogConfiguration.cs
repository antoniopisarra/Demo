using Demo.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DataAccess.Configuration;

public class LoginLogConfiguration : IEntityTypeConfiguration<LoginLog>
{
    public void Configure(EntityTypeBuilder<LoginLog> builder)
    {
        builder.HasKey(log => log.Id);

        builder.Property(log => log.Username).IsRequired().HasMaxLength(100);
        builder.Property(log => log.IpAddress).IsRequired().HasMaxLength(45);
        builder.Property(log => log.DataLogin).IsRequired();
        builder.Property(log => log.Successo).IsRequired();
        builder.Property(log => log.MotivoFallimento).HasMaxLength(255);
        builder.Property(log => log.UserAgent).HasMaxLength(255);
    }
}