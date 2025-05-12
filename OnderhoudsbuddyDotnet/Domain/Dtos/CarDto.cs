using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Dtos;

public class CarDto
{
    public int CarId { get; set; }
    public int Mileage { get; set; }
        
    [JsonPropertyName("kenteken")]
    public string LicensePlate { get; set; } = string.Empty;

    [NotMapped]

    [JsonPropertyName("merk")]
    public string Brand { get; set; } = string.Empty;

    [NotMapped]
    [JsonPropertyName("handelsbenaming")]
    public string TradeName { get; set; } = string.Empty;

    [NotMapped]
    [JsonPropertyName("voertuigsoort")]
    public string VehicleType { get; set; } = string.Empty;

    [NotMapped]
    [JsonPropertyName("eerste_kleur")]
    public string PrimaryColor { get; set; } = string.Empty;
    
    [NotMapped]
    [JsonPropertyName("vervaldatum_apk")]
    public string ApkExpiryDate { get; set; } = string.Empty;

    [NotMapped]
    [JsonPropertyName("massa_ledig_voertuig")]
    public int EmptyVehicleMass { get; set; }

    [NotMapped]
    [JsonPropertyName("datum_eerste_afgifte_nederland")]
    public string RegistrationDate { get; set; } = string.Empty;

    [NotMapped]
    [JsonPropertyName("datum_eerste_toelating")]
    public string FirstAdmissionDate { get; set; } = string.Empty;

    [NotMapped]
    [JsonPropertyName("tellerstandoordeel")]
    public string MileageJudgment { get; set; } = string.Empty;
}