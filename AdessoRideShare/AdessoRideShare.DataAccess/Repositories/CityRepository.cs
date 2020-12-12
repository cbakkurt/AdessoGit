using AdessoRideShare.DataAccess.IRepositories;
using AdessoRideShare.Domain;
using AdessoRideShare.Domain.IContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.DataAccess.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(IAdessoDbContext context)
           : base(context)
        { }

        public async Task<City> GetCityByCode(int cityCode)
        {
            return await _context.Cities.FirstOrDefaultAsync(x => x.Code == cityCode);
        }

        public async Task<bool> IsAllCitiesExist(List<int> cityCode)
        {
            var result = _context.Cities.Where(p => cityCode.Any(p2 => p2 == p.Code));

            return result.Count() == cityCode.Count;
        }
    }
}
