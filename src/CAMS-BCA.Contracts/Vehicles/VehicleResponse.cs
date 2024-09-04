namespace CAMS_BCA.Contracts.Vehicles
{
    public record VehicleResponse(
        Guid Id,
        string UniqueIdentifier,
        string Model,
        string Manufacturer,
        int Year,
        decimal StartingBid,
        VehicleType Type);
}
