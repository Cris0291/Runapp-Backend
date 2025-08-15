using System.Text.Json.Serialization;

namespace Contracts.Reviews.Requests
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReviewDescriptions
    {
        Excellent,
        Good,
        Average,
        Incomplete,
        Terrible,
    }
}
