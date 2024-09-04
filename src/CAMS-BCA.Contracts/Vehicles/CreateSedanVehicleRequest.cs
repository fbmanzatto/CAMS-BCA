namespace CAMS_BCA.Contracts.Vehicles
{
    public record CreateSedanVehicleRequest : CreateVehicleRequest
    {
        public required int NumberOfDoors { get; set; }
    }
}