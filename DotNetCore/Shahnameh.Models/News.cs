using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shahnameh.Models
{
    [Table("News", Schema = "dbo")]
    public class News
    {
        #region Properties

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NewsID { get; set; }

        [StringLength(256), Key, Column(Order = 2)]
        public string Title { get; set; }

        [StringLength(256), Key, Column(Order = 3)]
        public string SubTitle { get; set; }

        [Key, Column(Order = 4)]
        public string Content { get; set; }

        [Key, Column(Order = 5)]
        public string Date { get; set; }

        [Key, Column(Order = 6)]
        public string[] references { get; set; }

        #endregion
    }
}