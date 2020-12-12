using AdessoRideShare.DataAccess.IRepositories;
using AdessoRideShare.Domain;
using AdessoRideShare.Domain.IContext;

namespace AdessoRideShare.DataAccess.Repositories
{

    public class JourneyRouteRepository : Repository<JourneyRoute>, IJourneyRouteRepository
    {
        public JourneyRouteRepository(IAdessoDbContext context)
           : base(context)
        { }
    }
}
