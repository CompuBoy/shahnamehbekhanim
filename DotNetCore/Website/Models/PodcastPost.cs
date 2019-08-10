using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shahnameh.Models
{
    [Table("PodcastPost", Schema = "dbo")]
    public class PodcastPost
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ID { get; set; }

        [StringLength(256)]
        public string Category { get; set; }

        public DateTime PublishDate { get; set; }

        [StringLength(512)]
        public string Title { get; set; }

        [StringLength(2048)]
        public string PodcastUrl { get; set; }

        public string Description { get; set; }

        public string Story { get; set; }

        #endregion

    }
}
