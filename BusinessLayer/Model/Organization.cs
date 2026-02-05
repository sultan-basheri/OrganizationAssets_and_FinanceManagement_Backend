using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;

namespace BusinessLayer.Model
{
    public class Organization
    {
        public Organization()
        {
            EnrollOrgMember = new HashSet<Member>();
            EnrollStaffOrg = new HashSet<Staff>();
            EnrollPropertiesOrg = new HashSet<Properties>();
            EnrollDonationOrg = new HashSet<Donation>();
            EnrollSalaryMasterOrg = new HashSet<SalaryMaster>();
            EnrollPurchaseMasterOrg = new HashSet<PurchaseMaster>();
            EnrollPurchasePaymentOrg = new HashSet<PurchasePayment>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Trust number is required")]
        public string TrustNo { get; set; } = string.Empty;

        public string? City { get; set; }

        [Phone(ErrorMessage = "Invalid contact number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digits")]
        public string? ContactNo { get; set; }
        [Phone(ErrorMessage = "Invalid alternate contact number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Alternate number must be 10 digits")]
        public string? AlternateNo { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        public string? EstablishedYear { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public string base64Data { get; set; }
        public string? docType { get; set; }
        public string? docUrl { get; set; }


        public ICollection<Member> EnrollOrgMember { get; set; }
        public ICollection<Staff> EnrollStaffOrg { get; set; }
        public ICollection<Properties> EnrollPropertiesOrg { get; set; }
        public ICollection<Donation> EnrollDonationOrg { get; set; }
        public ICollection<SalaryMaster> EnrollSalaryMasterOrg { get;set; }
        public ICollection<PurchaseMaster> EnrollPurchaseMasterOrg { get; set; }
        public ICollection<PurchasePayment> EnrollPurchasePaymentOrg { get; set; }

        

    }
}
