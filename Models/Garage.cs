using System;
using System.Collections.Generic;

namespace Rent_a_car.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Garage
    {
        public int Id { get; set; }

        
        public int CarId { get; set; }
        public Car Car { get; set; }

        [Display(Name = "Storage Time")]
        public int StorageTime { get; set; }

        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        public ICollection<CarUsage> CarUsages { get; set; } = new List<CarUsage>();
    }
}
