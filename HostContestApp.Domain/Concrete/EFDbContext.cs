using HostContestApp.Domain.Entities;
using System.Data.Entity;

namespace HostContestApp.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }        
    }
}
