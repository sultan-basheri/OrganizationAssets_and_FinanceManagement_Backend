using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrganization
    {
        public Task<ResponseResult> GetList();
        public Task<ResponseResult> GetOrganizationList();
        public Task<ResponseResult> GetOrganizationById(int Id);
        public Task<ResponseResult> AddOrganization(Organization organization);
        public Task<ResponseResult> UpdateOrganization(Organization organization);
        public Task<ResponseResult> DeleteOrganization(int Id);
    }
}
