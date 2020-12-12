using AdessoRideShare.Domain.DTO;
using AdessoRideShare.Service.ResponseApi;
using System.Threading.Tasks;

namespace AdessoRideShare.Service.IServices
{
    public interface IJourneyBookingService
    {
        Task<ResponseModel> AddJourneyBooking(JourneyBooinkgDTO journeyBooinkgDTO);
    }
}
