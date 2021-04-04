namespace Rent_a_car.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    
    public class DriverInfo
    {
        public int Id { get; set; }

        [Required]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expiration Driver License")]
        public DateTime ExpDriverLicense { get; set; }

        public ICollection<CreditCard> CreditCards = new List<CreditCard>();
    }
}
