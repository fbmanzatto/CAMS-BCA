using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Domain.Bids;
using CAMS_BCA.Infrastructure.Common;

using Microsoft.EntityFrameworkCore;

namespace CAMS_BCA.Infrastructure.Auctions.Persistence
{
    public class BidsRepository(AppDbContext _dbContext) : IBidsRepository
    {
        public async Task AddAsync(Bid bid, CancellationToken cancellationToken)
        {
            await _dbContext.Bids.AddAsync(bid, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Bid>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Bids.Include(v => v.Vehicle).ToListAsync(cancellationToken);
        }

        public async Task<List<Bid>> GetAllAsync(Guid auctionId, CancellationToken cancellationToken)
        {
            return await _dbContext.Bids.Include(v => v.Vehicle).Where(a => a.Auction.Id == auctionId).ToListAsync(cancellationToken);
        }

        public async Task<Bid?> GetByIdAsync(Guid bidId, CancellationToken cancellationToken)
        {
            return await _dbContext.Bids.Include(v => v.Vehicle).FirstOrDefaultAsync(bid => bid.Id == bidId, cancellationToken);
        }

        public async Task RemoveAsync(Bid bid, CancellationToken cancellationToken)
        {
            _dbContext.Remove(bid);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Bid bid, CancellationToken cancellationToken)
        {
            _dbContext.Update(bid);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}