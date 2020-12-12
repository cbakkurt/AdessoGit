using AdessoRideShare.DataAccess.IRepositories;
using AdessoRideShare.DataAccess.Repositories;
using AdessoRideShare.Domain.IContext;
using System;
using System.Threading.Tasks;

namespace AdessoRideShare.DataAccess.UnitOfWork
{
    public class AdessoUnitOfWork : IUnitOfWork
    {
        private readonly IAdessoDbContext _context;
        private CityRepository _cityRepository;
        private JourneyBookingRepository _journeyBookingRepository;
        private JourneyRepository _journeyRepository;
        private JourneyRouteRepository _journeyRouteRepository;
        private UserRepository _userRepository;

        public AdessoUnitOfWork(IAdessoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        
        public IJourneyBookingRepository JourneyBookingRepository => _journeyBookingRepository = _journeyBookingRepository ?? new JourneyBookingRepository(_context);
        public IJourneyRepository JourneyRepository => _journeyRepository = _journeyRepository ?? new JourneyRepository(_context);
        public IJourneyRouteRepository JourneyRouteRepository=> _journeyRouteRepository = _journeyRouteRepository ?? new JourneyRouteRepository(_context);
        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_context);
        public ICityRepository CityRepository => _cityRepository = _cityRepository ?? new CityRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
