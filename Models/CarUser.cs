namespace Rent_a_car.Models
{
    public class CarUser
    {
        public int  Id { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
