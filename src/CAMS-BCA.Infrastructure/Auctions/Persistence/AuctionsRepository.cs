using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Domain.Auctions;
using CAMS_BCA.Infrastructure.Common;

using Microsoft.EntityFrameworkCore;

namespace CAMS_BCA.Infrastructure.Auctions.Persistence
{
    public class AuctionsRepository(AppDbContext _dbContext) : IAuctionsRepository
    {
        public async Task AddAsync(Auction auction, CancellationToken cancellationToken)
        {
            await _dbContext.Auctions.AddAsync(auction, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Auction>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Auctions.Include(v => v.Vehicle).ToListAsync(cancellationToken);
        }

        public async Task<Auction?> GetByIdAsync(Guid auctionId, CancellationToken cancellationToken)
        {
            return await _dbContext.Auctions.Include(v => v.Vehicle).FirstOrDefaultAsync(auction => auction.Id == auctionId, cancellationToken);
        }

        public async Task<List<Auction>> GetAllByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken)
        {
            return await _dbContext.Auctions.Include(v => v.Vehicle).Where(auction => auction.Vehicle.Id == vehicleId).ToListAsync(cancellationToken);
        }

        public async Task RemoveAsync(Auction auction, CancellationToken cancellationToken)
        {
            _dbContext.Remove(auction);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Auction auction, CancellationToken cancellationToken)
        {
            _dbContext.Update(auction);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}