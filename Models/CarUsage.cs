namespace Rent_a_car.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CarUsage
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int Range { get; set; }

        public int GarageId { get; set; }

        public Garage Garage { get; set; }
    }
}
