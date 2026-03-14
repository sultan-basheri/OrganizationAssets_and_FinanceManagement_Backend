using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLayer.Model
{
    public class Admin
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;
    }
    public class Authentication
    {
        public int Id { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? oldPassword { get; set; }
        public string? newPassword { get; set; }
    }
}
