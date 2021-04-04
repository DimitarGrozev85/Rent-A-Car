namespace Rent_a_car.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Flags]//we have to discuss that
    public enum UserRole
    {
        Admin = 0,
        Employee = 1,
        Driver = 2
    }
}
