using System.Net.Http;
using System.Net.Http.Json;

using CAMS_BCA.Api.IntegrationTests.Common.WebApplicationFactory;
using CAMS_BCA.Contracts.Vehicles;
using CAMS_BCA.Domain.Vehicles;

using FluentAssertions;

namespace CAMS_BCA.Api.IntegrationTests.Controllers;

[Collection(WebAppFactoryCollection.CollectionName)]

public class VehiclesTests
{
    private readonly HttpClient _client;

    public VehiclesTests(WebAppFactory webAppFactory)
    {
        _client = webAppFactory.CreateClient();
        webAppFactory.ResetDatabase();
    }

    [Fact]
    public async Task CreateVehicle_WhenNotExists_ShouldReturnSuccess()
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

        // Act
        var response = await _client.PostAsJsonAsync($"vehicles/hatchback", createVehicleRequest);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateVehicle_WhenAlreadyExists_ShouldReturnError()
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

        // Act
        var response = await _client.PostAsJsonAsync($"vehicles/hatchback", createVehicleRequest);
        var response2 = await _client.PostAsJsonAsync($"vehicles/hatchback", createVehicleRequest);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        response2.StatusCode.Should().Be(System.Net.HttpStatusCode.Conflict);
    }
}
