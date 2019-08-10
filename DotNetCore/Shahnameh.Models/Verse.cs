using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shahnameh.Models
{
    [Table("Verses", Schema = "dbo")]
    public class Verse
    {
        #region Properties

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PostID { get; set; }

        [Key, Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Index { get; set; }

        public decimal Part1StartTime { get; set; }

        public decimal Part1EndTime { get; set; }

        public decimal Part2StartTime { get; set; }

        public decimal Part2EndTime { get; set; }

        #endregion
    }
}