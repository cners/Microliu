using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SharingProject.Configuration;
using SharingProject.Web;

namespace SharingProject.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class SharingProjectDbContextFactory : IDesignTimeDbContextFactory<SharingProjectDbContext>
    {
        public SharingProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SharingProjectDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            SharingProjectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SharingProjectConsts.ConnectionStringName));

            return new SharingProjectDbContext(builder.Options);
        }
    }
}
