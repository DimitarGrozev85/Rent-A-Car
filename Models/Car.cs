using System.Collections.Generic;

namespace Rent_a_car.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "The Title length should be between 2 and 20.", MinimumLength = 2)]
        public string Make { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "The Title length should be between 2 and 20.", MinimumLength = 2)]
        public string Model { get; set; }

        [NotMapped]
        [Display(Name = "Car Name")]
        public string CarName => $"{Make} {Model}";

        [DataType(DataType.Currency)]
        [Range(1, 100)]
        public decimal Price { get; set; }

        [Display(Name = "Insurance Cost")]
        public decimal InsuranceCost { get; set; }

        [Display(Name = "Maintenance Cost")]
        public decimal MaintenanceCost { get; set; }

        public ICollection<CarUser> CarUser { get; set; } = new List<CarUser>(); //many to many?
    }
}
