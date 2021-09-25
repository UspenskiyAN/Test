using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.DAL
{
    public class DBC : DbContext
    {
        public DBC(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new Client { Key = "1QaZ@wSx", Name = "TestClient" });
            modelBuilder.Entity<RequestResponse>().HasData(
                new RequestResponse { Request = "request", Response = "response" },
                new RequestResponse { Request = "request 2", Response = "response 2" });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RequestResponse> Responses { get; set; }

    }
}
