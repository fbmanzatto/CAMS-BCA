namespace CAMS_BCA.Contracts.Vehicles
{
    public record SearchVehicleRequest
    {
        public string? Model { get; init; }
        public string? Manufacturer { get; init; }
        public int? Year { get; init; }
        public VehicleType? Type { get; init; }
    }
}