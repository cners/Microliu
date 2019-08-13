using Microliu.Auth.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microliu.Auth.DataMSSQL.Configurations
{
    public class RoleConfiguration
    {
        public RoleConfiguration(EntityTypeBuilder<Role> entity)
        {
            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(4);
        }
    }
}
