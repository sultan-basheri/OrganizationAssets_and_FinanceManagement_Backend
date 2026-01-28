using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Model
{
    public class ResponseResult
    {
        public ResponseResult(string Status,object Result)
        {
            this.Status = Status;
            this.Result = Result;
        }
        public string Status { get; set; }
        public object Result { get; set; }
    }
}
