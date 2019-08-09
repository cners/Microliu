using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Microliu.Core.Consul.REST
{
   public class RestResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpResponseHeaders Headers { get; set; }
    }
}
