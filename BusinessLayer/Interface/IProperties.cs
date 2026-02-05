using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IProperties
    {
        public Task<ResponseResult> GetPropertyList();
        public Task<ResponseResult> GetPropertyById(int Id);
        public Task<Properties?> getPropertyEntityById(int id);
        public Task<ResponseResult> AddProperty(Properties properties);
        public Task<ResponseResult> UpdateProperty(int Id, Properties properties);
    }
}
