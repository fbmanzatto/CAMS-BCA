namespace CAMS_BCA.Contracts.Vehicles
{
    public record CreateVehicleRequest
    {
        public required string UniqueIdentifier { get; init; }
        public required string Model { get; init; }
        public required string Manufacturer { get; init; }
        public required int Year { get; init; }
        public required decimal StartingBid { get; init; }
    }
}