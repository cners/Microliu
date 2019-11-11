using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microliu.Core.Redis
{
    public class LogWriter : TextWriter
    {
        public override Encoding Encoding => throw new NotImplementedException();
    }
}
