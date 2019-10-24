using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ResimEklemeSiralama.Models
{
    public class Context:DbContext
    {
        public Context() : base("imageDB")
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());
        }
        public DbSet<Image> Images { get; set; }
    }
}