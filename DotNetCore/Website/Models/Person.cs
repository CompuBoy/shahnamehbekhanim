using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shahnameh.Models
{
    [Table("Persons", Schema = "dbo")]
    public class Person
    {
        #region Properties

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PostID { get; set; }

        public Post Post { get; set; }

        [StringLength(256), Key, Column(Order = 2)]
        public string Name { get; set; }

        [Key, Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VerseIndex { get; set; }

        public bool IsPresentInPart1 { get; set; }

        public bool IsPresentInPart2 { get; set; }

        #endregion
    }
}