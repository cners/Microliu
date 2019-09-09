using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Core.RedisCache
{
    public interface ICacheService
    {
        void SetDbIndex(int index);

        string Get(string key);

        void Set(string key, string value, long timeout);
    }
}
