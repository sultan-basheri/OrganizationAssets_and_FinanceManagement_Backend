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

        public int OrganizationId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public string? AlternateNo { get; set; }
        public string Email { get; set; } =string.Empty;

        /* ================= SECURITY ================= */
        public string? Password { get; set; }
        public string Role { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    
}
