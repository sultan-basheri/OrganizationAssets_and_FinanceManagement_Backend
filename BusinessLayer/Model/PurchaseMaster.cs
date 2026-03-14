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
            PurDetail = new HashSet<PurchaseDetail>();
        }
        [Key]
        public int Id { get; set; } 
        public virtual Vendor? Vendor { get; set; }
        public int VendorId {  get; set; }

        public int? BillNo { get; set; }

        public string? ChallanNo { get; set; }
        public decimal GrossAmount { get; set; }

        public string GSTType { get; set; } = string.Empty;
        // Example: CGST+SGST, IGST

        public decimal GSTPercentage { get; set; }

        public decimal GSTAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public DateOnly BillDate { get; set; }

        [NotMapped]
        public string? base64Data { get; set; }
        public string? DocType { get; set; } // PDF, PNG, JPG etc.
        public string? BillUrl { get; set; }   // File URL / Path

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Organization? Organization { get; set; }
        public int OrganizationId { get; set; }

        public virtual OfficeStaff? OfficeStaff { get; set; }
     
        public int OfficeStaffId { get; set; }   

        public virtual FinancialYear? FinancialYear { get; set; }
       
        public int FinancialYearId { get; set; }  


        public ICollection<PurchasePayment> PurPaymentMaster { get; set; }
        public ICollection<PurchaseDetail> PurDetail { get; set; }
    }
}
