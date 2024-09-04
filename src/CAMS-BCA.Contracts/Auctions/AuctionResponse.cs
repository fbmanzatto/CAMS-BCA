namespace CAMS_BCA.Contracts.Auctions
{
    public record AuctionResponse(Guid Id, string Description, DateTime StartDate, bool Active, DateTime EndDate, Guid VehicleId);
}
