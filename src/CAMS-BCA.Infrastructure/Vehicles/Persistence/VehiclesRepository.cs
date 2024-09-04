using CAMS_BCA.Application.Common.Interfaces;
using CAMS_BCA.Domain.Vehicles;
using CAMS_BCA.Infrastructure.Common;

using Microsoft.EntityFrameworkCore;

namespace CAMS_BCA.Infrastructure.Vehicles.Persistence
{
    public class VehiclesRepository(AppDbContext _dbContext) : IVehiclesRepository
    {
        public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            await _dbContext.Vehicles.AddAsync(vehicle, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Vehicle>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Vehicles.ToListAsync(cancellationToken);
        }

        public async Task<Vehicle?> GetByIdAsync(Guid vehicleId, CancellationToken cancellationToken)
        {
            return await _dbContext.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == vehicleId, cancellationToken);
        }

        public async Task<Vehicle?> GetByUniqueIdentifierAsync(string uniqueIdentifier, CancellationToken cancellationToken)
        {
            return await _dbContext.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.UniqueIdentifier == uniqueIdentifier, cancellationToken);
        }

        public async Task RemoveAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            _dbContext.Remove(vehicle);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Vehicle>> SearchAsync(string? model, string? manufacturer, int? year, VehicleType? type, CancellationToken cancellationToken)
        {
            List<Vehicle> vehicles = await GetAllAsync(cancellationToken);

            if (!string.IsNullOrEmpty(model))
            {
                vehicles = vehicles.Where(v => v.Model == model).ToList();
            }

            if (!string.IsNullOrEmpty(manufacturer))
            {
                vehicles = vehicles.Where(v => v.Manufacturer == manufacturer).ToList();
            }

            if (year is not null)
            {
                vehicles = vehicles.Where(v => v.Year == year).ToList();
            }

            if (type is not null)
            {
                vehicles = vehicles.Where(v => v.Type == type).ToList();
            }

            return vehicles;
        }

        public async Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            _dbContext.Update(vehicle);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}