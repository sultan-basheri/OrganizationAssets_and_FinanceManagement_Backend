using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IPropertyRentAgreement
    {
        public Task<ResponseResult> GetPRAgreementList();
        public Task<ResponseResult> GetPRAgreementById(int Id);
        public Task<PropertyRentAgreement?> getPRAgreementEntityById(int id);
        public Task<ResponseResult> AddPRAgreement(PropertyRentAgreement PRAgreement);
        public Task<ResponseResult> UpdatePRAgreement(int Id, PropertyRentAgreement PRAgreement);
    }
}
