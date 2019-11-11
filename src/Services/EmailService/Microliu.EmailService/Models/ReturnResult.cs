using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microliu.EmailService.API.Models
{
    public class ReturnResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public ReturnResult()
        {
            Success = true;
            Message = "";
            Data = "";
        }
        public ReturnResult(bool success, string message)
        {
            Success = success;
            Message = message;
            Data = "";
        }

        public ReturnResult SetSuccess(bool success = true)
        {
            Success = success;
            return this;
        }

        public ReturnResult SetMessage(string message)
        {
            Message = message;
            return this;
        }

        public ReturnResult SetData(object data)
        {
            Data = data;
            return this;
        }

        public ReturnResult SetData(bool success, object data)
        {
            Success = success;
            Data = data;
            return this;
        }

        public static ReturnResult SetFail(string message = "", object data = null)
        {
            var r = new ReturnResult();
            r.Success = false;
            r.Message = message;
            r.Data = data;
            return r;
        }

        public static ReturnResult SetSuccess(string message = "", object data = null)
        {
            var r = new ReturnResult();
            r.Success = true;
            r.Message = message;
            r.Data = data;
            return r;
        }
    }
}
