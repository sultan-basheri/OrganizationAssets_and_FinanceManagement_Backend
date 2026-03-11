using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IVendor
    {
        public Task<ResponseResult> getVendorList();
        public Task<ResponseResult> getVendorById(int Id);
        public Task<ResponseResult> addVendor(Vendor vendor);
        public Task<ResponseResult> updateVendor(int Id, Vendor vendor);
    }
}
