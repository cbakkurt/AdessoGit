using AdessoRideShare.DataAccess.IRepositories;
using AdessoRideShare.Domain;
using AdessoRideShare.Domain.IContext;

namespace AdessoRideShare.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IAdessoDbContext context)
           : base(context)
        { }
    }
}
