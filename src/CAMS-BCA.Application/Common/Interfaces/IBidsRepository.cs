using CAMS_BCA.Domain.Bids;

namespace CAMS_BCA.Application.Common.Interfaces
{
    public interface IBidsRepository
    {
        Task AddAsync(Bid bid, CancellationToken cancellationToken);
        Task<Bid?> GetByIdAsync(Guid bidId, CancellationToken cancellationToken);
        Task<List<Bid>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Bid>> GetAllAsync(Guid auctionId, CancellationToken cancellationToken);
        Task RemoveAsync(Bid bid, CancellationToken cancellationToken);
        Task UpdateAsync(Bid bid, CancellationToken cancellationToken);
    }
}