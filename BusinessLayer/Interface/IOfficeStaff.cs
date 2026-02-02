using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IOfficeStaff
    {
        public Task<ResponseResult> officeStaffAuthentication(Authentication authentication);
        public Task<ResponseResult> changeProfile(int Id, OfficeStaff officeStaff);
        public Task<ResponseResult> getOfficeStaffList();
        public Task<ResponseResult> officeStaffProfile(int Id);
        public Task<ResponseResult> signUpofficeStaff(OfficeStaff officeStaff);
        public Task<ResponseResult> deactivateofficeStaff(int Id);
        public Task<ResponseResult> updatePassword(Authentication authentication);
    }
}
