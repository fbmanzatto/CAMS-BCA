using CAMS_BCA.Domain.Auctions;

namespace CAMS_BCA.Application.Common.Interfaces
{
    public interface IAuctionsRepository
    {
        Task AddAsync(Auction auction, CancellationToken cancellationToken);
        Task<Auction?> GetByIdAsync(Guid auctionId, CancellationToken cancellationToken);
        Task<List<Auction>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Auction>> GetAllByVehicleIdAsync(Guid vehicleId, CancellationToken cancellationToken);
        Task RemoveAsync(Auction auction, CancellationToken cancellationToken);
        Task UpdateAsync(Auction auction, CancellationToken cancellationToken);
    }
}