using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using SharingProject.Authorization.Roles;
using SharingProject.Authorization.Users;
using SharingProject.MultiTenancy;

namespace SharingProject.EntityFrameworkCore
{
    public class SharingProjectDbContext : AbpZeroDbContext<Tenant, Role, User, SharingProjectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public SharingProjectDbContext(DbContextOptions<SharingProjectDbContext> options)
            : base(options)
        {
        }
    }
}
