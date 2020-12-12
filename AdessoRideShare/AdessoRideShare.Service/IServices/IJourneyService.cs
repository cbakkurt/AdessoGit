using AdessoRideShare.Domain;
using AdessoRideShare.Domain.DTO;
using AdessoRideShare.Service.ResponseApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdessoRideShare.Service.IServices
{
    public interface IJourneyService
    {
        Task<ResponseModel> AddJourneyWithRoute(JourneyWithRoutesDTO journeyDTO);
        Task<ResponseModel> AddJourney(JourneyDTO journeyDTO);
        Task<ResponseModel> ChangeJourneyState(JourneyChangeStateDTO journeyDTO);
        Task<List<JourneyResponseDTO>> GetAllAvaliableJourney(int cityCodeFrom, int cityCodeTo);
        Task<List<JourneyResponseDTO>> GetAllAvaliableJourneyWithRoute(int cityCodeFrom, int cityCodeTo);

    }
}
