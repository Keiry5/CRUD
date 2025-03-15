using CRUD.D;
using System.Security.Cryptography.X509Certificates;

namespace CRUD.Db
{
    public class AppDbContext
    {
        public AppDbContext(DbContext<AppDbContext> options ) : base (options) 
        {
            Public DbSet<Producto> productos { get; set; }
        }
    }
}
