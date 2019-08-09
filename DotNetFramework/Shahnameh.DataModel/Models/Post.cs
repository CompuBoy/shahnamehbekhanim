using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shahnameh.Models
{
    [Table("dbo.posts")]
    public class Post
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID { get; set; }

        [StringLength(256)]
        public string Category { get; set; }

        [StringLength(256)]
        public string Title { get; set; }

        public int StartingVerse { get; set; }

        public int EndingVerse { get; set; }

        [StringLength(128)]
        public string Narrator { get; set; }

        public string Story { get; set; }

        #endregion

    }
}