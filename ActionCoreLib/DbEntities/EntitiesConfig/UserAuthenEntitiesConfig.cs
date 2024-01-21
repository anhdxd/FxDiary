using ActionCoreLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionCoreLib.DbEntities.EntitiesConfig
{
    internal class UserAuthenEntitiesConfig: IEntityTypeConfiguration<UserAuthenModel>
    {
        public void Configure(EntityTypeBuilder<UserAuthenModel> builder)
        {
            builder.ToTable("UserAuthen");// Table name
            builder.HasKey(x => x.UserId); // Primary key
            builder.Property(x=>x.UserId).ValueGeneratedOnAdd(); // Auto increment
            builder.HasIndex(x => x.Username).IsUnique(); // Email
            builder.Property(x => x.Username).IsRequired(); // User name
            builder.Property(x => x.Password).IsRequired(); // Password
            builder.HasIndex(x => x.Email).IsUnique(); // Email
            builder.Property(x => x.Email).IsRequired(); // Email


        }
    }
}
