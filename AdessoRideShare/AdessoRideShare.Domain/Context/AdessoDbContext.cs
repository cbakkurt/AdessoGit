using AdessoRideShare.Domain.IContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AdessoRideShare.Domain.Context
{
    public class AdessoDbContext : DbContext, IAdessoDbContext
    {
        public AdessoDbContext(DbContextOptions<AdessoDbContext> options)
           : base(options)
        {

        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Journey> Journeys { get; set; }
        public virtual DbSet<JourneyBooking> JourneyBookings { get; set; }
        public virtual DbSet<JourneyRoute> JourneyRoutes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public void Dispose()
        {
            base.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        DbSet<TEntity> IAdessoDbContext.Set<TEntity>() where TEntity : class
        {
            try
            {
                return Set<TEntity>();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
