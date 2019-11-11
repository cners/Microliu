using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.FileService.Application
{
    public class FileServiceException : Exception
    {
        public FileServiceException(string message)
              : base(message) { }

        public FileServiceException(string message, Exception innerException)
            : base(message, innerException) { }

    }
}
