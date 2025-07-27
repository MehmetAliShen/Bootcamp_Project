using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Configurations
{
    public class BlacklistConfiguration : IEntityTypeConfiguration<Blacklist>
    {
        public void Configure(EntityTypeBuilder<Blacklist> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Reason)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(b => b.Date)
                   .IsRequired();

            builder.HasOne<Applicant>()
                   .WithMany()
                   .HasForeignKey(b => b.ApplicantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
