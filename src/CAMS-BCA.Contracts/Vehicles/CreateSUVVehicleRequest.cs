namespace CAMS_BCA.Contracts.Vehicles
{
    public record CreateSUVVehicleRequest : CreateVehicleRequest
    {
        public required int NumberOfSeats { get; set; }
    }
}