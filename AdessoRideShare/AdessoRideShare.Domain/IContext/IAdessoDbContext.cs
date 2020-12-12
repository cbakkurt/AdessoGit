using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AdessoRideShare.Domain.IContext
{
    public interface IAdessoDbContext : IDisposable
    {
        DbSet<City> Cities { get; set; }
        DbSet<Journey> Journeys { get; set; }
        DbSet<JourneyBooking> JourneyBookings { get; set; }
        DbSet<JourneyRoute> JourneyRoutes { get; set; }
        DbSet<User> Users { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
