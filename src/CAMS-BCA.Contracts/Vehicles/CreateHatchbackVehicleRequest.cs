namespace CAMS_BCA.Contracts.Vehicles
{
    public record CreateHatchbackVehicleRequest : CreateVehicleRequest
    {
        public required int NumberOfDoors { get; set; }
    }
}