using FleetManager.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManager.Data.Entity.Map
{
    public class VehicleMap : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("VehiclePk");

            builder.Property(e => e.Id);

            builder.Property(e => e.Chassi)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Ignore(e => e.Passengers);

            builder.Property(e => e.Color)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.HasIndex(e => e.Chassi)
                .IsUnique()
                .HasName("VehicleNk");
        }
    }
}
