namespace Models;

public class CarModel
{
    public int CarId { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string CarType { get; set; } = string.Empty;
    public int Mileage { get; set; }
    public DateTime ExpiryDateApk { get; set; }
}