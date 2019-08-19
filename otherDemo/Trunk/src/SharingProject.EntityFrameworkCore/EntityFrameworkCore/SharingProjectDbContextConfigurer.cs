using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace SharingProject.EntityFrameworkCore
{
    public static class SharingProjectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SharingProjectDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<SharingProjectDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
