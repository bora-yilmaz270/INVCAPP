using INVCAPP.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace INVCAPP.Repository.Configurations;

public class InvoiceHeaderConfiguration : IEntityTypeConfiguration<InvoiceHeader>
{
    public void Configure(EntityTypeBuilder<InvoiceHeader> builder)
    {
        builder.HasKey(h => h.Id); 
      
        builder.Property(h => h.InvoiceId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(h => h.SenderTitle)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(h => h.ReceiverTitle)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(h => h.Date)
            .IsRequired();

        builder.Property(h => h.Email)
            .IsRequired()
            .HasMaxLength(100);
      
    }
}