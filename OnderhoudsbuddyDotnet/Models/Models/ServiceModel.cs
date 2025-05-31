using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ServiceModel
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
