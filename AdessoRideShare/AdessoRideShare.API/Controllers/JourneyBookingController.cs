using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdessoRideShare.Domain.DTO;
using AdessoRideShare.Service.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdessoRideShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyBookingController : Controller
    {
        private readonly IJourneyBookingService _journeyBookingService;
        private readonly ILogger<JourneyBookingController> _logger;
        private readonly IMapper _mapper;

        public JourneyBookingController(ILogger<JourneyBookingController> logger, IJourneyBookingService journeyBookingService, IMapper mapper)
        {
            _logger = logger;
            _journeyBookingService = journeyBookingService;
            _mapper = mapper;
        }

        [HttpPost("AddJourneyBooking")]
        public async Task<JsonResult> AddJourneyBooking([FromBody] JourneyBooinkgDTO journeyBooinkgDTO)
        {
            _logger.LogInformation($"User: {journeyBooinkgDTO.UserId} , Ürün :{journeyBooinkgDTO.SeatCount} için sepet ekleme çağrıldı.");

            //var shoppingCart = _mapper.Map<ShoppingCartDTO, ShoppingCart>(shoppingCartDTO);

            var serviceResponse = await _journeyBookingService.AddJourneyBooking(journeyBooinkgDTO);

            return Json(serviceResponse);
        }
    }
}