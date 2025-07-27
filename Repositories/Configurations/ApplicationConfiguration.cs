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
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.ApplicationState)
                   .IsRequired();

            builder.HasOne<Applicant>()
                   .WithMany()
                   .HasForeignKey(a => a.ApplicantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Bootcamp>()
                   .WithMany()
                   .HasForeignKey(a => a.BootcampId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}