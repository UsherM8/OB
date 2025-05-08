using System.Text.Json.Serialization;

namespace Domain.Dtos
{
    public class RdwCarDto
    {
        [JsonPropertyName("kenteken")]
        public string LicensePlate { get; set; } = string.Empty;

        [JsonPropertyName("merk")]
        public string Brand { get; set; } = string.Empty;

        [JsonPropertyName("handelsbenaming")]
        public string TradeName { get; set; } = string.Empty;

        [JsonPropertyName("voertuigsoort")]
        public string VehicleType { get; set; } = string.Empty;

        [JsonPropertyName("eerste_kleur")]
        public string PrimaryColor { get; set; } = string.Empty;

        [JsonPropertyName("vervaldatum_apk")]
        public string ApkExpiryDate { get; set; } = string.Empty;

        [JsonPropertyName("massa_ledig_voertuig")]
        public int EmptyVehicleMass { get; set; }

        [JsonPropertyName("datum_eerste_afgifte_nederland")]
        public string RegistrationDate { get; set; } = string.Empty;

        [JsonPropertyName("datum_eerste_toelating")]
        public string FirstAdmissionDate { get; set; } = string.Empty;

        [JsonPropertyName("tellerstandoordeel")]
        public string MileageJudgment { get; set; } = string.Empty;
    }
}