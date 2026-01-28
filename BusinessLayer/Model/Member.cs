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
        [Required(ErrorMessage = "Organization is required")]
        public virtual Organization? Organization { get; set; }
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
        public string? Email { get; set; }

        /* ================= SECURITY ================= */
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = string.Empty;
        [Required(ErrorMessage = "Joining Date is required")]
        public DateOnly JoiningDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
