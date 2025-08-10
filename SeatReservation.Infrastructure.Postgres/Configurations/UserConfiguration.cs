using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain;

namespace SeatReservation.Infrastructure.Postgres.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id).HasName("pk_users");

            builder.Property(u => u.Id)
                .HasConversion(u => u.Value, id => new UserId(id))
                .HasColumnName("id")
                .IsRequired()
                .HasColumnOrder(0);

            builder.OwnsOne(u => u.Details, db =>
            {
                db.ToJson("details");

                db.Property(u => u.FIO).IsRequired().HasMaxLength(LengthConstants.LENGTH500).HasColumnName("fio");

                db.Property(u => u.Description).IsRequired().HasMaxLength(LengthConstants.LENGTH500).HasColumnName("description");

                db.OwnsMany(d => d.Socials, sb =>
                {
                    sb.Property(u => u.Link)
                    .IsRequired()
                    .HasMaxLength(LengthConstants.LENGTH500)
                    .HasColumnName("link");

                    sb.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(LengthConstants.LENGTH500)
                    .HasColumnName("name");
                });
       
            });

            
        }
    }
}
