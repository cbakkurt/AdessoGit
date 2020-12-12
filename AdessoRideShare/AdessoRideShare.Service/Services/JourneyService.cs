using AdessoRideShare.DataAccess.UnitOfWork;
using AdessoRideShare.Domain;
using AdessoRideShare.Domain.DTO;
using AdessoRideShare.Service.IServices;
using AdessoRideShare.Service.ResponseApi;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Service.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<JourneyService> _logger;

        public JourneyService(IUnitOfWork unitOfWork, ILogger<JourneyService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<ResponseModel> AddJourney(JourneyDTO journeyDTO)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(journeyDTO.UserId);
            var cityFrom = await _unitOfWork.CityRepository.GetCityByCode(journeyDTO.CityCodeFrom);
            var cityTo = await _unitOfWork.CityRepository.GetCityByCode(journeyDTO.CityCodeTo);

            if (user == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Bu kullanıcı {journeyDTO.UserId} bulunamadı."
                };
            }
            if (cityFrom == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Başlangıç şehri {journeyDTO.CityCodeFrom} bulunamadı."
                };
            }
            if (cityTo == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Bitiş şehri {journeyDTO.CityCodeTo} bulunamadı."
                };
            }

            Journey journey = new Journey
            {
                Id = Guid.NewGuid(),
                Description = journeyDTO.Description,
                JourneyDate = journeyDTO.JourneyDate,
                PublishingState = true,
                SeatCount = journeyDTO.SeatCount,
                UserId = journeyDTO.UserId
            };

            JourneyRoute journeyRouteFrom = new JourneyRoute
            {
                Id = Guid.NewGuid(),
                JourneyId = journey.Id,
                CityId = journeyDTO.CityCodeFrom,
                IsStartCity = true,
                IsEndCity = false
            };

            JourneyRoute journeyRouteTo = new JourneyRoute
            {
                Id = Guid.NewGuid(),
                JourneyId = journey.Id,
                CityId = journeyDTO.CityCodeTo,
                IsStartCity = false,
                IsEndCity = true
            };

            await _unitOfWork.JourneyRepository.AddAsync(journey);
            await _unitOfWork.JourneyRouteRepository.AddAsync(journeyRouteFrom);
            await _unitOfWork.JourneyRouteRepository.AddAsync(journeyRouteTo);

            await _unitOfWork.CommitAsync();


            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Kayıt eklendi."
            };
        }

        public async Task<ResponseModel> AddJourneyWithRoute(JourneyWithRoutesDTO journeyWithRoutesDTO)
        {
            if (journeyWithRoutesDTO.CitiesList == null || journeyWithRoutesDTO.CitiesList.Count < 2)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Şehir listesi en az 2 olmalıdır."
                };
            }
            var user = await _unitOfWork.UserRepository.GetByIdAsync(journeyWithRoutesDTO.UserId);
            var cityFrom = await _unitOfWork.CityRepository.IsAllCitiesExist(journeyWithRoutesDTO.CitiesList);

            if (user == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = $"Bu kullanıcı {journeyWithRoutesDTO.UserId} bulunamadı."
                };
            }

            if (cityFrom == false)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Şehir listesi içerisinde sistem olmayan mevcut. Kontrol ediniz."
                };
            }
            Journey journey = new Journey
            {
                Id = Guid.NewGuid(),
                Description = journeyWithRoutesDTO.Description,
                JourneyDate = journeyWithRoutesDTO.JourneyDate,
                PublishingState = true,
                SeatCount = journeyWithRoutesDTO.SeatCount,
                UserId = journeyWithRoutesDTO.UserId
            };

            JourneyRoute journeyRouteFrom = new JourneyRoute
            {
                Id = Guid.NewGuid(),
                JourneyId = journey.Id,
                CityId = journeyWithRoutesDTO.CitiesList.First(),
                IsStartCity = true,
                IsEndCity = false
            };

            JourneyRoute journeyRouteTo = new JourneyRoute
            {
                Id = Guid.NewGuid(),
                JourneyId = journey.Id,
                CityId = journeyWithRoutesDTO.CitiesList.Last(),
                IsStartCity = false,
                IsEndCity = true
            };

            await _unitOfWork.JourneyRepository.AddAsync(journey);
            await _unitOfWork.JourneyRouteRepository.AddAsync(journeyRouteFrom);
            await _unitOfWork.JourneyRouteRepository.AddAsync(journeyRouteTo);

            for (int i = 1; i < journeyWithRoutesDTO.CitiesList.Count - 1; i++)
            {
                JourneyRoute journeyRoute = new JourneyRoute
                {
                    Id = Guid.NewGuid(),
                    JourneyId = journey.Id,
                    CityId = journeyWithRoutesDTO.CitiesList.ElementAt(i),
                    IsStartCity = false,
                    IsEndCity = false
                };
                await _unitOfWork.JourneyRouteRepository.AddAsync(journeyRoute);

            }

            await _unitOfWork.CommitAsync();


            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Kayıt eklendi."
            };
        }

        public async Task<ResponseModel> ChangeJourneyState(JourneyChangeStateDTO journeyDTO)
        {
            var journey = await _unitOfWork.JourneyRepository.GetByIdAsync(journeyDTO.JourneyId);
            journey.PublishingState = journeyDTO.State;

            _unitOfWork.JourneyRepository.Update(journey);

            await _unitOfWork.CommitAsync();

            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Kayıt güncellendi."
            };
        }

        public async Task<List<JourneyResponseDTO>> GetAllAvaliableJourney(int cityCodeFrom, int cityCodeTo)
        {
            var journey = await _unitOfWork.JourneyRepository.GetJourney(cityCodeFrom, cityCodeTo);

            return journey;
        }

        public async Task<List<JourneyResponseDTO>> GetAllAvaliableJourneyWithRoute(int cityCodeFrom, int cityCodeTo)
        {
            var journey = await _unitOfWork.JourneyRepository.GetJourneyWithRoute(cityCodeFrom, cityCodeTo);
            return journey;
        }
    }
}
