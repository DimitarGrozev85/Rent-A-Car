using Microsoft.EntityFrameworkCore;
using Rent_a_car.Models;

namespace Rent_a_car.Data
{
    public class Rent_a_carContext : DbContext
    {
        public Rent_a_carContext(DbContextOptions<Rent_a_carContext> options)
            : base(options)// what is this
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarUser> CarUsers { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<DriverInfo> DriverInfos { get; set; }

        public DbSet<Garage> Garages { get; set; }
    }
}
