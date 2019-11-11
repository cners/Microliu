using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Utils.jwt
{
    public interface IJwt
    {
        string GetToken(Dictionary<string, string> Clims);
        bool ValidateToken(string Token, out Dictionary<string, string> Clims);
    }
}
