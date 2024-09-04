namespace CAMS_BCA.Contracts.Auctions
{
    public record EndAuctionRequest
    {
        public required Guid AuctionId { get; init; }
    }
}