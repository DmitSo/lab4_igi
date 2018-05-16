using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace lab1_ef
{
    public class HotelContext : DbContext, IDesignTimeDbContextFactory<HotelContext>
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

        public HotelContext() : base(new DbContextOptions<HotelContext>())
        {

        }

        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var options = optionsBuilder.UseSqlServer(connectionString).Options;

        }
        
        public HotelContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<HotelContext> optionsBuilder = 
                new DbContextOptionsBuilder<HotelContext>();

            OnConfiguring(optionsBuilder);

            return new HotelContext(optionsBuilder.Options);
            //throw new System.NotImplementedException();
        }
    }
}
