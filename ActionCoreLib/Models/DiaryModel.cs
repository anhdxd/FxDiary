using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionCoreLib.Models
{
    public class DiaryModel
    {
        //[Key]
        public int Id { get; set; }

        //[Required]
        public int TimeStamp { get; set; }

        //[Required]
        public int OrderType { get; set; } // 0: Buy, 1: Sell

        //[Required]
        public int RiskReward { get; set; } // ... -3 -2 -1 0 1 2 3 ...

        public string? Note { get; set; } // Note

        //[Required]
        public byte[]? ImageEntryTF { get; set; } // Image, entry timeframe

        public byte[]? Image2 { get; set; } // Image 2 - optional

        public byte[]? Image3 { get; set; } // Image 3 - optional



    }
}
