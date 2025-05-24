using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class UserCar
    {
        [Key]
        public int UserCarId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
    }
}
