namespace CAMS_BCA.Contracts.Bids
{
    public record BidResponse(Guid Id,
        Guid AuctionId,
        Guid VehicleId,
        DateTime Date,
        decimal Value);
}
