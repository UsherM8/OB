namespace Models;

public class CarModel
{
    public int CarId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public int Mileage { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string TradeName { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string PrimaryColor { get; set; } = string.Empty;
    public string ApkExpiryDate { get; set; } = string.Empty;
    public int EmptyVehicleMass { get; set; }
    public string RegistrationDate { get; set; } = string.Empty;
    public string FirstAdmissionDate { get; set; } = string.Empty;
    public string MileageJudgment { get; set; } = string.Empty;
}