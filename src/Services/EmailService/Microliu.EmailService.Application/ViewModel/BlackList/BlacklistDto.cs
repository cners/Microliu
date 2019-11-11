using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.EmailService.Application.ViewModel
{
  public  class BlacklistDto
    {
        public long Total { get; set; }
        public List<string> Emails { get; set; }
    }
}
