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
        public long NewsId { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Content { get; set; }

        public string Date { get; set; }

        #endregion
    }
}