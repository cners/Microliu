using Microliu.Auth.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.DataOracle
{
    public partial class BaseRepository
    {
        public DbType GetDbType()
        {
            return DbType.Oracle;
        }
    }
}
