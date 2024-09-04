namespace CAMS_BCA.Contracts.Bids
{
    public record CreateBidRequest
    {
        public required Guid AuctionId { get; set; }
        public required Guid VehicleId { get; set; }
        public required decimal Value { get; set; }
    }
}