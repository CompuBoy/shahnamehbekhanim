using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Shahnameh.Models
{
    public class DataModel : DbContext
    {
        #region Constructors

        public DataModel(DbContextOptions<DataModel> options) : base(options)
        {
        }

        #endregion

        #region Properties

        public DbSet<PodcastPost> PodcastPosts { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Verse> Verses { get; set; }

        public DbSet<Phrase> Phrases { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Voice> Voices { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Person>()
                .HasKey(p => new { p.PostID, p.Name, p.VerseIndex, });
            modelBuilder.Entity<Phrase>()
                .HasKey(ph => new { ph.PostID, ph.Value, ph.VerseIndex, });
            modelBuilder.Entity<Verse>()
                .HasKey(v => new { v.PostID, v.Index, });
            
        }        
    }
}