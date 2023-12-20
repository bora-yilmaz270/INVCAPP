using INVCAPP.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVCAPP.Repository.Configurations;

public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
{
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
    {
        builder.HasKey(l => l.Id); 
      
        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(l => l.Quantity)
            .IsRequired();

        builder.Property(l => l.UnitCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(l => l.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");
    }
}