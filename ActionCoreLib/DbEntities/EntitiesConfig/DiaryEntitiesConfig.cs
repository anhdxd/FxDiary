using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActionCoreLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActionCoreLib.DbEntities.EntitiesConfig
{

    internal class DiaryEntitiesConfig : IEntityTypeConfiguration<DiaryModel>
    {
        // Config data type for Diary table
        public void Configure(EntityTypeBuilder<DiaryModel> builder)
        {
            builder.ToTable("FxDiary");// Table name
            builder.HasKey(x => x.Id); // Primary key
            builder.Property(x => x.Id).ValueGeneratedOnAdd(); // Auto increment
            builder.Property(x => x.TimeStamp).IsRequired(); // Date
            builder.Property(x => x.OrderType).IsRequired(); // Order type
            builder.Property(x => x.RiskReward).IsRequired(); // Risk reward
            builder.Property(x => x.Note); // Note
            builder.Property(x => x.ImageEntryTF); // Image entry timeframe
            builder.Property(x => x.Image2); // Image 2
            builder.Property(x => x.Image3); // Image 3

        }
    }
}
