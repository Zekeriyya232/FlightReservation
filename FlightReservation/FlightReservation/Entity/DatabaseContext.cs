using Microsoft.EntityFrameworkCore;

namespace WebProje2023.Entity
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<KullaniciDB> Kullanici { get; set; }
		public DbSet<PlaneDB> Plane { get; set; }
		public DbSet<RoutesDB> Routes { get; set; }

		public DbSet<TicketDB> Ticket { get; set; }
	}
}
