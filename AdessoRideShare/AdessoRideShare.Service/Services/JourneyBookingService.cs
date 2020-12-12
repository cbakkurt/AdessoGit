using AdessoRideShare.DataAccess.UnitOfWork;
using AdessoRideShare.Domain;
using AdessoRideShare.Domain.DTO;
using AdessoRideShare.Service.IServices;
using AdessoRideShare.Service.ResponseApi;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AdessoRideShare.Service.Services
{
    public class JourneyBookingService : IJourneyBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<JourneyBookingService> _logger;

        public JourneyBookingService(IUnitOfWork unitOfWork, ILogger<JourneyBookingService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ResponseModel> AddJourneyBooking(JourneyBooinkgDTO journeyBooinkgDTO)
        {
            var journey = await _unitOfWork.JourneyRepository.GetByIdAsync(journeyBooinkgDTO.JourneyId);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(journeyBooinkgDTO.UserId);
            if (journey == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Bu seyahet {journeyBooinkgDTO.JourneyId} bulunamadı."
                };
            }
            if (user == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Bu kullanıcı {journeyBooinkgDTO.UserId} bulunamadı."
                };
            }
            if (journey.SeatCount < journeyBooinkgDTO.SeatCount)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Bu seyahet için {journeyBooinkgDTO.SeatCount} adet koltuk bulanmamaktadır. Sadece {journey.SeatCount } adet koltuk vardır."
                };
            }
            if (journey.UserId == journeyBooinkgDTO.UserId)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Kullanıcı kendi seyahetine ekleyemezsiniz. {journeyBooinkgDTO.UserId}."
                };
            }

            journey.SeatCount -= journeyBooinkgDTO.SeatCount;

            JourneyBooking journeyBooking = new JourneyBooking
            {
                Id = Guid.NewGuid(),
                JourneyId = journeyBooinkgDTO.JourneyId,
                UserId = journeyBooinkgDTO.UserId
            };

            await _unitOfWork.JourneyBookingRepository.AddAsync(journeyBooking);
            _unitOfWork.JourneyRepository.Update(journey);

            await _unitOfWork.CommitAsync();

            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Kayıt eklendi."
            };

        }
    }
}
