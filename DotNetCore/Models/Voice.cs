using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shahnameh.Models
{
    [Table("Voices", Schema = "dbo")]
    public class Voice
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PostID { get; set; }

        public byte[] Content { get; set; }

        #endregion
    }
}