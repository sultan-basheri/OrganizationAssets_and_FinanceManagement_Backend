using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }  

        public virtual Organization? Organization { get; set; } 
        [Required]
        public int OrganizationId { get; set; }   

        public virtual Staff? Staff { get; set; }
        [Required]
        public int StaffId { get; set; }   

        public string? DonorThrough { get; set; }   

        [Required(ErrorMessage = "Donation amount is required")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Donation type is required")]
        public string DonationType { get; set; } = string.Empty;
        // Example: Zakat, Sadaqah, Donation

        [Required(ErrorMessage = "Payment type is required")]
        public string PaymentType { get; set; } = string.Empty;
        // Example: Cash, UPI, Bank Transfer

        [Required]
        public DateOnly PaymentDate { get; set; }

        public string? Remark { get; set; }

        public virtual FinancialYear? FinancialYear { get; set; }
        [Required]
        public int FinancialYearId { get; set; }   

        public virtual OfficeStaff? OfficeStaff { get; set; }
        [Required]
        public int OfficeStaffId { get; set; }   

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
