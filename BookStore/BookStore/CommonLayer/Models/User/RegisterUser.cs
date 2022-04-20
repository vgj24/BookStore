using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models.User
{
    public class RegisterUser
    {
        [RegularExpression(@"^[A-Za-z]{3,}$",
        ErrorMessage = "Please enter a valid Full name")]
        public string FullName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",
       ErrorMessage = "Please enter correct Email Address")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$",
        ErrorMessage = "Please enter correct format of Password")]
        public string Password { get; set; }

        [RegularExpression(@"(0|91)?[6-9][0-9]{9}",
        ErrorMessage = "Please enter correct Phone Number")]
        public string PhoneNumber { get; set; }

        public string ServiceType { get; } = "User";
        // Password format : password between 8 to 15 characters which contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character.
        // Email format : A-Z small capital both and @ and gmail.com
        //phone number : starts with 0 or 91 then first number should start with 6 7 8 or 9 and rest nine numbers from 0-9
    }
}
