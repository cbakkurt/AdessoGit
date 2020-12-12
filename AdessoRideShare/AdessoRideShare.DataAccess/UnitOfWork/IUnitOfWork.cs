using AdessoRideShare.DataAccess.IRepositories;
using System;
using System.Threading.Tasks;

namespace AdessoRideShare.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository CityRepository { get; }
        IJourneyRepository JourneyRepository { get; }
        IJourneyBookingRepository JourneyBookingRepository{ get; }
        IJourneyRouteRepository JourneyRouteRepository{ get; }
        IUserRepository UserRepository { get; }
        Task<int> CommitAsync();
    }
}
