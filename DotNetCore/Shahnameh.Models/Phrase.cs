using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shahnameh.Models
{
    [Table("Phrases", Schema = "dbo")]
    public class Phrase
    {
        #region Properties

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PostID { get; set; }

        [StringLength(256), Key, Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Value { get; set; }

        [Key, Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VerseIndex { get; set; }

        public bool IsPresentInPart1 { get; set; }

        public bool IsPresentInPart2 { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        #endregion
    }
}