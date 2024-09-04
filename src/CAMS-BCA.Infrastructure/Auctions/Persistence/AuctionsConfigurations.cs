using CAMS_BCA.Domain.Auctions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CAMS_BCA.Infrastructure.Auctions.Persistence
{
    public class AuctionsConfigurations : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever();

            builder.HasOne(v => v.Vehicle);
        }
    }
}