using System.Data.Common;

namespace Microliu.Utils.Database
{
    public interface IDbFactory
    {
        DbConnection Create();
    }
}
