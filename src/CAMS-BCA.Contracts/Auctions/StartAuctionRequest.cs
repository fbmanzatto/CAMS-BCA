namespace CAMS_BCA.Contracts.Auctions
{
    public record StartAuctionRequest
    {
        public required Guid AuctionId { get; init; }
    }
}