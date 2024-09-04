using CAMS_BCA.Domain.Vehicles;

namespace CAMS_BCA.Application.Common.Interfaces
{
    public interface IVehiclesRepository
    {
        Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
        Task<Vehicle?> GetByIdAsync(Guid vehicleId, CancellationToken cancellationToken);
        Task<Vehicle?> GetByUniqueIdentifierAsync(string uniqueIdentifier, CancellationToken cancellationToken);
        Task<List<Vehicle>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Vehicle>> SearchAsync(string? model, string? manufacturer, int? year, VehicleType? type, CancellationToken cancellationToken);
        Task RemoveAsync(Vehicle vehicle, CancellationToken cancellationToken);
        Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken);
    }
}