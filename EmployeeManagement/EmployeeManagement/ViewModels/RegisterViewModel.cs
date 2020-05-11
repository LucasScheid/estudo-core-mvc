using EmployeeManagement.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller:"Account", ErrorMessage ="Email domain must be yahoo.com.br")]
        [ValidEmailDomain(allowedDomain:"yahoo.com.br")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }

    }
}
