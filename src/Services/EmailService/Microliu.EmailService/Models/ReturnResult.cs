using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microliu.EmailService.API.Models
{
    public class ReturnResult
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public string Data { get; set; }

        public ReturnResult(int code, string message)
        {
            Code = code;
            Message = message;
            Data = null;
        }
    }
}
