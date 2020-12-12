using AdessoRideShare.DataAccess.IRepositories;
using AdessoRideShare.Domain;
using AdessoRideShare.Domain.IContext;

namespace AdessoRideShare.DataAccess.Repositories
{
    public class JourneyBookingRepository : Repository<JourneyBooking>, IJourneyBookingRepository
    {
        public JourneyBookingRepository(IAdessoDbContext context)
           : base(context)
        { }
    }
}
