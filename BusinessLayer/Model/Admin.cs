using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer.Model
{
    public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Invalid contact number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digits")]
        public string ContactNo { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Status { get; set; } = string.Empty;
    }
    public class Authentication
    {
        public int Id { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
        public string? oldPassword { get; set; }
        public string? newPassword { get; set; }
    }
}
