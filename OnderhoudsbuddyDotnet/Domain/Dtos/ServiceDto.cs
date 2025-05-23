using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }

        public int CarId { get; set; }
        public int GarageId { get; set; }
        public int ServiceType { get; set; }
        public int Status { get; set; }
        public DateTime ServiceDate { get; set; }
        public DateTime NextServiceDate { get; set; }
        public int Description { get; set; }
    }
}
