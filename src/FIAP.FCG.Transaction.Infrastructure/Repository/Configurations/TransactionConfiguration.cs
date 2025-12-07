using FIAP.FCG.Transaction.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.FCG.Transaction.Infrastructure.Repository.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnType("bigint")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(p => p.GameId)
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(p => p.UserId)
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(p => p.Type)
            .HasConversion<string>()
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasColumnType("varchar(30)")
            .IsRequired();

       
    }
}
