namespace Domain.Dtos;

public class CarDto
{
    public int CarId { get; set; }
    public string LicencePlate { get; set; } = string.Empty;
    public int Mileage { get; set; }
}