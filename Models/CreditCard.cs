using System;

namespace Rent_a_car.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreditCard
    {
        [Display(Name = "Credit Card Number")]
        public int CreditCardId { get; set; }

        [Display(Name = "Expiration Time")]
        [DataType(DataType.Date)]
        public DateTime ExpTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Range(0, 10000, ErrorMessage = "Out of Limit")]
        public decimal Limit { get; set; }
    }
}
