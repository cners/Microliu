using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.Consul.REST
{
    public class RestResponseWithBody<T> : RestResponse
    {
        public T Body { get; set; }
    }
}
