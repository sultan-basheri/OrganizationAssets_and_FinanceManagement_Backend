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

        public int MosqueId { get; set; }   

       
        public virtual Organization? Organization { get; set; }
        public int OrganizationId { get; set; }    

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of joining is required")]
        public DateOnly DateOfJoining { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        public string? AadharNo { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Invalid contact number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Contact number must be 10 digits")]
        public string ContactNo { get; set; } = string.Empty;

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Alternate number must be 10 digits")]
        public string? AlternateNo { get; set; }

        public string? VillageTown { get; set; }

        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Pincode must be 6 digits")]
        public string? Pincode { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Staff type is required")]
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
