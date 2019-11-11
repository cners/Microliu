﻿using System;

namespace Microliu.Auth.Application
{
    public class AuthException :Exception
    {
        public AuthException(string message)
            : base(message) { }

        public AuthException(string message,Exception innerException)
            : base(message, innerException) { }

    }
}
