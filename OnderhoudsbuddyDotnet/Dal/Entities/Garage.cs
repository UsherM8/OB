using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class Garage
    {
        [Key]
        public int GarageId { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string ExtraAddressInfo { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}

