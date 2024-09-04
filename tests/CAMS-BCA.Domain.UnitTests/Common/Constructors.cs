using CAMS_BCA.Domain.Auctions;
using CAMS_BCA.Domain.Bids;
using CAMS_BCA.Domain.Vehicles;

namespace CAMS_BCA.Domain.UnitTests.Common
{
    public static class Constructors
    {
        public static Auction CreateAuction(bool active)
        {
            if (active)
            {
                return new Auction { Description = "Description", Vehicle = new HatchbackVehicle(), Active = true, StartDate = DateTime.Now };
            }
            else
            {
                return new Auction { Description = "Description", Vehicle = new HatchbackVehicle(), Active = false, StartDate = DateTime.Now, EndDate = DateTime.Now };
            }
        }

        public static Auction CreateAuction(Vehicle vehicle, bool active)
        {
            if (active)
            {
                return new Auction { Description = "Description", Vehicle = vehicle, Active = true, StartDate = DateTime.Now };
            }
            else
            {
                return new Auction { Description = "Description", Vehicle = vehicle, Active = false, StartDate = DateTime.Now, EndDate = DateTime.Now };
            }
        }

        public static HatchbackVehicle CreateHatchbackVehicle()
        {
            return new HatchbackVehicle
            {
                Id = Guid.NewGuid(),
                UniqueIdentifier = "MM-46-JD",
                Model = "Punto Evo 1.3",
                Manufacturer = "Fiat",
                Year = 2011,
                StartingBid = 5000,
                NumberOfDoors = 4,
            };
        }

        public static HatchbackVehicle CreateHatchbackVehicle(decimal startingBid)
        {
            return new HatchbackVehicle
            {
                Id = Guid.NewGuid(),
                UniqueIdentifier = "MM-46-JD",
                Model = "Punto Evo 1.3",
                Manufacturer = "Fiat",
                Year = 2011,
                StartingBid = startingBid,
                NumberOfDoors = 4,
            };
        }

        public static Bid CreateBid(Auction auction, decimal value)
        {
            var bid = new Bid
            {
                Auction = auction,
                Date = DateTime.Now,
                Vehicle = CreateHatchbackVehicle(),
            };
            bid.SetValue(value);
            return bid;
        }

        public static Bid CreateBid(Auction auction, Vehicle vehicle)
        {
            var bid = new Bid
            {
                Auction = auction,
                Date = DateTime.Now,
                Vehicle = vehicle,
            };
            return bid;
        }
    }
}
