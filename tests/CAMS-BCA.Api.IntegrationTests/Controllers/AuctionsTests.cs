using System.Net.Http;
using System.Net.Http.Json;
using System.Numerics;

using CAMS_BCA.Api.IntegrationTests.Common.WebApplicationFactory;
using CAMS_BCA.Contracts.Auctions;
using CAMS_BCA.Contracts.Vehicles;
using CAMS_BCA.Domain.Vehicles;

using FluentAssertions;

namespace CAMS_BCA.Api.IntegrationTests.Controllers;

[Collection(WebAppFactoryCollection.CollectionName)]

public class AuctionsTests
{
    private readonly HttpClient _client;

    public AuctionsTests(WebAppFactory webAppFactory)
    {
        _client = webAppFactory.CreateClient();
        webAppFactory.ResetDatabase();
    }

    [Fact]
    public async Task CreateAuction_WhenVehicleExists_ShouldReturnSuccess()
    {
        // Arrange
        var createVehicleRequest = new CreateHatchbackVehicleRequest
        {
            UniqueIdentifier = "MM-46-JD",
            Model = "Punto Evo 1.3",
            Manufacturer = "Fiat",
            Year = 2011,
            StartingBid = 5000,
            NumberOfDoors = 4,
        };
        var responseVehicle = await _client.PostAsJsonAsync($"vehicles/hatchback", createVehicleRequest);
        var vehicle = await responseVehicle.Content.ReadFromJsonAsync<VehicleResponse>();

        var createAuctionRequest = new CreateAuctionRequest { Description = "Auction Description", VehicleId = vehicle?.Id ?? Guid.NewGuid() };

        // Act
        var response = await _client.PostAsJsonAsync($"auctions/", createAuctionRequest);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateAuction_WhenVehicleNotExists_ShouldReturnError()
    {
        // Arrange
        var createAuctionRequest = new CreateAuctionRequest { Description = "Auction Description", VehicleId = Guid.NewGuid() };

        // Act
        var response = await _client.PostAsJsonAsync($"auctions/", createAuctionRequest);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task StartAuction_WhenAuctionNotActive_ShouldReturnSuccess()
    {
        // Arrange
        var createVehicleRequest = new CreateHatchbackVehicleRequest
        {
            UniqueIdentifier = "MM-46-JD",
            Model = "Punto Evo 1.3",
            Manufacturer = "Fiat",
            Year = 2011,
            StartingBid = 5000,
            NumberOfDoors = 4,
        };
        var responseVehicle = await _client.PostAsJsonAsync($"vehicles/hatchback", createVehicleRequest);
        var vehicle = await responseVehicle.Content.ReadFromJsonAsync<VehicleResponse>();

        var createAuctionRequest = new CreateAuctionRequest { Description = "Auction Description", VehicleId = vehicle?.Id ?? Guid.NewGuid() };
        var responseAuction = await _client.PostAsJsonAsync($"auctions/", createAuctionRequest);
        var auction = await responseAuction.Content.ReadFromJsonAsync<AuctionResponse>();
        var startAuctionRequest = new StartAuctionRequest { AuctionId = auction?.Id ?? Guid.NewGuid() };

        // Act
        var response = await _client.PutAsJsonAsync($"auctions/start/", startAuctionRequest);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }
}
