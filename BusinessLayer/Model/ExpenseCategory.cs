using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model
{
    public class ExpenseCategory
    {
        public ExpenseCategory()
        {
            ExpMasterCategory = new HashSet<ExpensesMaster>();
        }
        [Key]
        public int Id { get; set; }  

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;  //(Electricity,Repair,etc)

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual OfficeStaff? OfficeStaff { get; set; }
        public int OfficeStaffId { get; set; }
        public ICollection<ExpensesMaster> ExpMasterCategory { get; set; }
    }
}
