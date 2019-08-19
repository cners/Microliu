using Microliu.Auth.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.DataMSSQL
{
    public partial class BaseRepository
    {
        public DbType GetDbType()
        {
            return DbType.SQLServer;
        }
    }
}
