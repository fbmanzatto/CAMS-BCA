using CAMS_BCA.Domain.Vehicles;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CAMS_BCA.Infrastructure.Vehicles.Persistence
{
    public class VehicleConfigurations : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever();

            builder.UseTptMappingStrategy();

            builder.Property(p => p.Type)
                    .HasConversion(
                        v => v.Name,
                        v => VehicleType.FromName(v, false));
        }
    }
}