using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class Staff
    {
        public Staff()
        {
            DonationStaff = new HashSet<Donation>();
            SalaryMasterStaff = new HashSet<SalaryMaster>();
            WithdrawalStaff = new HashSet<WithdrawalSalary>();
        }
        [Key]
        public int Id { get; set; }   // Staff ID (PK)
        public virtual Mosque? Mosque { get; set; }

        public int? MosqueId { get; set; }   
        public virtual Organization? Organization { get; set; }
        public int OrganizationId { get; set; }    
        public string FullName { get; set; } = string.Empty;
        public DateOnly DateOfJoining { get; set; }
        public string Address { get; set; } = string.Empty;

        public string? AadharNo { get; set; }
        public string ContactNo { get; set; } = string.Empty;
        public string? AlternateNo { get; set; }

        public string? VillageTown { get; set; }
        public string? Pincode { get; set; }
        public decimal Salary { get; set; }
        public string StaffType { get; set; } = string.Empty;
        // Example: Cleaner, Muezzin, Imam, Watchman

        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual OfficeStaff? OfficeStaff { get; set; }
        public int OfficeStaffId { get; set; }

        public ICollection<Donation> DonationStaff { get; set; }
        public ICollection<SalaryMaster> SalaryMasterStaff { get; set; }
        public ICollection<WithdrawalSalary> WithdrawalStaff { get; set; }
    }
}
