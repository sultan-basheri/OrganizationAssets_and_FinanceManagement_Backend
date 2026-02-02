using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrganization
    {
        public Task<ResponseResult> getList();
        public Task<ResponseResult> getOrganizationList();
        public Task<ResponseResult> getOrganizationById(int Id);
        public Task<ResponseResult> addOrganization(Organization organization);
        public Task<ResponseResult> updateOrganization(int Id,Organization organization);
    }
}
