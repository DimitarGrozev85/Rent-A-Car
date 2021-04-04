namespace Rent_a_car.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Utils;

    public class User
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }

        [Display(Name = "User Role")]
        public UserRole UserRole { get; set; }

        public DriverInfo DriverInfo { get; set; } = new DriverInfo();//what's this

        public ICollection<CarUser> CarUser { get; set; } = new List<CarUser>();
    }
}
