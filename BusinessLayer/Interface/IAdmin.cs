using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdmin
    {
        public Task<ResponseResult> adminAuthentication(Authentication authentication);
        public Task<ResponseResult> changeProfile(int Id,Admin admin);
        public Task<ResponseResult> getAdminList();
        public Task<ResponseResult> adminProfile(int Id);
        public Task<ResponseResult> signUpAdmin(Admin admin);
        public Task<ResponseResult> deactivateAdmin(int Id);
        public Task<ResponseResult> updatePassword(Authentication authentication);
    }
}
