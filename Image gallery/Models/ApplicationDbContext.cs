using Microsoft.EntityFrameworkCore;

namespace Image_gallery.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageCatalog> ImageCatalog { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
