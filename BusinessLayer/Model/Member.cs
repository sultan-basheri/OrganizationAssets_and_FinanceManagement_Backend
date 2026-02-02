using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer.Model
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        public virtual Organization? Organization { get; set; }
        [Required(ErrorMessage = "Organization is required")]

        public int OrganizationId { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact Number is required")]
        [Phone(ErrorMessage = "Invalid contact number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digits")]
        public string ContactNo { get; set; } = string.Empty;
        [Phone(ErrorMessage = "Invalid alternate contact number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digits")]
        public string? AlternateNo { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } =string.Empty;

        /* ================= SECURITY ================= */
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    
}
