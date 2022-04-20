using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanLayer.Models.User
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
        //public string ServiceType { get; } = "Customer";
    }
    }
