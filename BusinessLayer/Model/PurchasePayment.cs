using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class PurchasePayment
    {
        [Key]
        public int Id { get; set; }  

        public virtual PurchaseMaster? PurchaseMaster { get; set; }
        [Required]
        public int PurchaseMasterId { get; set; }  

        [Required(ErrorMessage = "Payment amount is required")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Payment type is required")]
        public string PaymentType { get; set; } = string.Empty;
        // Example: Cash, Bank, UPI

        public string? Remark { get; set; }

        public virtual Organization? Organization { get; set; }
        [Required]
        public int OrganizationId { get; set; }   

        public virtual OfficeStaff? OfficeStaff { get; set; }
        [Required]
        public int OfficeStaffId { get; set; }   

        public virtual FinancialYear? FinancialYear { get; set; }
        [Required]
        public int FinancialYearId { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
