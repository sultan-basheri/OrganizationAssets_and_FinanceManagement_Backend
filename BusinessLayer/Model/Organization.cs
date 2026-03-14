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
            EnrollExpMasterOrg = new HashSet<ExpensesMaster>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TrustNo { get; set; } = string.Empty;

        public string? City { get; set; }
        public string? ContactNo { get; set; }
        public string? AlternateNo { get; set; }
        public string? Email { get; set; }

        public string? EstablishedYear { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public string? base64Data { get; set; }
        public string? docType { get; set; }
        public string? docUrl { get; set; }


        public ICollection<Member> EnrollOrgMember { get; set; }
        public ICollection<Staff> EnrollStaffOrg { get; set; }
        public ICollection<Properties> EnrollPropertiesOrg { get; set; }
        public ICollection<Donation> EnrollDonationOrg { get; set; }
        public ICollection<SalaryMaster> EnrollSalaryMasterOrg { get;set; }
        public ICollection<PurchaseMaster> EnrollPurchaseMasterOrg { get; set; }
        public ICollection<PurchasePayment> EnrollPurchasePaymentOrg { get; set; }
        public ICollection<ExpensesMaster> EnrollExpMasterOrg { get; set; }

    }
}
