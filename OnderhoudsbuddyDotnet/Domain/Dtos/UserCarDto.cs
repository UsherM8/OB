using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class UserCarDto
    {
        public int UserCarId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
    }
}
