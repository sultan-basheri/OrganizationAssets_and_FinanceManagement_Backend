using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class RentMaster
    {
        [Key]
        public int Id { get; set; }   // PK

        public virtual PropertyRentAgreement? PRAgreement { get; set; }
        [Required]
        public int PropertyRentAgreementId { get; set; }   // FK → RentAgreement

        [Required]
        public decimal RentAmount { get; set; }

        [Required(ErrorMessage = "Payment type is required")]
        public string PaymentType { get; set; } = string.Empty;
        // Example: Cash, UPI, Bank Transfer

        public string? Reference { get; set; }  

        [Required]
        public DateOnly PaymentDate { get; set; } 

        public string? Remark { get; set; }

        public virtual OfficeStaff? OfficeStaff { get; set; }
        [Required]
        public int OfficeStaffId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime? UpdatedAt { get; set; }
    }
}
