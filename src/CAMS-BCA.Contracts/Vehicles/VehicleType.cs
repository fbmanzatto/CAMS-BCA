using System.Text.Json.Serialization;

namespace CAMS_BCA.Contracts.Vehicles
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum VehicleType
    {
        Hatchback,
        Sedan,
        SUV,
        Truck,
    }
}