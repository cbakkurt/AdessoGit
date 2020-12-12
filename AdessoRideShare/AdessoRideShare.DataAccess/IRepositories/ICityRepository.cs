using AdessoRideShare.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdessoRideShare.DataAccess.IRepositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<City> GetCityByCode(int cityCode);
        Task<bool> IsAllCitiesExist(List<int> cityCode);

    }
}
