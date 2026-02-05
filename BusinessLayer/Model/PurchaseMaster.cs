using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class PurchaseMaster
    {
        public PurchaseMaster()
        {
            PurPaymentMaster = new HashSet<PurchasePayment>();
        }
        [Key]
        public int Id { get; set; }  

        public int? BillNo { get; set; }

        public string? ChallanNo { get; set; }

        public string BusinessName { get; set; } = string.Empty;

        public string? ContactPerson { get; set; }

        public string? GSTIN { get; set; }

        public string? Address { get; set; }

        public decimal GrossAmount { get; set; }

        public string GSTType { get; set; } = string.Empty;
        // Example: CGST+SGST, IGST

        [Required]
        public decimal GSTPercentage { get; set; }

        [Required]
        public decimal GSTAmount { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateOnly BillDate { get; set; }

        [NotMapped]
        public string base64Data { get; set; }
        public string? DocType { get; set; } // PDF, PNG, JPG etc.
        public string? BillUpload { get; set; }   // File URL / Path

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Organization? Organization { get; set; }
        [Required]
        public int OrganizationId { get; set; }

        public virtual OfficeStaff? OfficeStaff { get; set; }
     
        public int OfficeStaffId { get; set; }   

        public virtual FinancialYear? FinancialYear { get; set; }
       
        public int FinancialYearId { get; set; }  


        public ICollection<PurchasePayment> PurPaymentMaster { get; set; }
    }
}
