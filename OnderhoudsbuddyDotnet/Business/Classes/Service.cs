
namespace Business.Classes
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int CarId { get; set; }
        public int GarageId { get; set; }
        public string ServiceType { get; set; }
        public string Status { get; set; }
        public DateTime ServiceDate { get; set; }
        public DateTime NextServiceDate { get; set; }
        public string Description { get; set; }
    }
}
