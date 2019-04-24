using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Models
{
    public class DummyContext : DbContext
    {
        public DummyContext (DbContextOptions<DummyContext> options)
            : base(options)
        {
        }
        
        // public DbSet<Web.Models.LexiconEntryViewModel> LexiconEntryViewModel { get; set; }
        // public DbSet<Web.Models.CategoryViewModel> CategoryViewModel { get; set; }
    }
}
