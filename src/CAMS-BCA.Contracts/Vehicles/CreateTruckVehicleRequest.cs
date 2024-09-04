namespace CAMS_BCA.Contracts.Vehicles
{
    public record CreateTruckVehicleRequest : CreateVehicleRequest
    {
        public required decimal LoadCapacity { get; set; }
    }
}