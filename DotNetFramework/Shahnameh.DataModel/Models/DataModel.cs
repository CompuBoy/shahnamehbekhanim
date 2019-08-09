using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Shahnameh.Models
{
    public class DataModel : DbContext
    {
        #region Constructors

        public DataModel(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
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

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}