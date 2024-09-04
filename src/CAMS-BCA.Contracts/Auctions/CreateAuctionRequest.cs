namespace CAMS_BCA.Contracts.Auctions
{
    public record CreateAuctionRequest
    {
        public required string Description { get; init; }

        public required Guid VehicleId { get; init; }
    }
}