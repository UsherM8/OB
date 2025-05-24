using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public int Mileage { get; set; }
        public string LicensePlate { get; set; }
    }
}
