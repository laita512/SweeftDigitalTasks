using Microsoft.EntityFrameworkCore;

namespace Core.DB
{
    public partial class DBCoreDataContext : DbContext
    {
        public DBCoreDataContext()
        {
        }

        public DBCoreDataContext(DbContextOptions<DBCoreDataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}