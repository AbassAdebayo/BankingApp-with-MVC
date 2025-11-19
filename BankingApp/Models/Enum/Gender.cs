using System.Text.Json.Serialization;

namespace BankingApp.Models.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender : byte
    {
        Male = 1,
        Female
    }
}
