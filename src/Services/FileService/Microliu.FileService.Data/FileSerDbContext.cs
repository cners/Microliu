using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Microliu.FileService.Data
{
    public class FileSerDbContext : DbContext, IDbContext
    {
        public static long InstanceCount;

        public FileSerDbContext(DbContextOptions options)
            : base(options)
        {

            Interlocked.Increment(ref InstanceCount);
        }
    }
}
