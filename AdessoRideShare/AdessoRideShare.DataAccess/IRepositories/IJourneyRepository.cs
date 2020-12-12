using AdessoRideShare.Domain;
using AdessoRideShare.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdessoRideShare.DataAccess.IRepositories
{
    public interface IJourneyRepository : IRepository<Journey>
    {
        Task<List<JourneyResponseDTO>> GetJourney(int cityCodeFrom, int cityCodeTo);
        Task<List<JourneyResponseDTO>> GetJourneyWithRoute(int cityCodeFrom, int cityCodeTo);

    }
}
