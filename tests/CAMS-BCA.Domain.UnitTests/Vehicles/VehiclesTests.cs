using CAMS_BCA.Domain.UnitTests.Common;
using CAMS_BCA.Domain.Vehicles;

using FluentAssertions;

using Xunit;

namespace CAMS_BCA.Domain.UnitTests.Vehicles
{
    public class VehiclesTests : TestsBase
    {
        [Fact]
        public void CreateVehicle_WhenConstructedSuccessfully_ShouldHaveAvailableFalse()
        {
            // Arrange || Act
            var hatchbakcVehicle = Constructors.CreateHatchbackVehicle();

            // Assert
            hatchbakcVehicle.Available.Should().BeTrue();
        }

        [Fact]
        public void CreateVehicle_WhenConstructedSuccessfully_ShouldHaveCorrectType()
        {
            // Arrange || Act
            var hatchbakcVehicle = new HatchbackVehicle();
            var sedanVehicle = new SedanVehicle();
            var suvVehicle = new SUVVehicle();
            var truckVehicle = new TruckVehicle();

            // Assert
            hatchbakcVehicle.Type.Should().BeEquivalentTo(VehicleType.Hatchback);
            sedanVehicle.Type.Should().BeEquivalentTo(VehicleType.Sedan);
            suvVehicle.Type.Should().BeEquivalentTo(VehicleType.SUV);
            truckVehicle.Type.Should().BeEquivalentTo(VehicleType.Truck);
        }
    }
}