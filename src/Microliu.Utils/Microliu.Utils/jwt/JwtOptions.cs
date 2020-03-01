using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Utils.jwt
{
    public class JwtOptions
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        /// <summary>
        /// 加密key
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 生命周期（分钟）
        /// </summary>
        public int Lifetime { get; set; }
        /// <summary>
        /// 是否验证生命周期
        /// </summary>
        public bool ValidateLifetime { get; set; }
        
        /// <summary>
        /// 忽略验证的url
        /// </summary>
        public List<string> IgnoreUrls { get; set; }

    }
}
