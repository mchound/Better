using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better.Data
{
    public class BetterContext : DbContext, IDisposable
    {
        public DbSet<RawMatch> RawMatches { get; set; }

        public BetterContext() : base("Better")
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
