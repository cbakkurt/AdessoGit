using AdessoRideShare.Domain;
using AdessoRideShare.Domain.DTO;
using AdessoRideShare.Service.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdessoRideShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : Controller
    {
        private readonly IJourneyService _journeyService;
        private readonly ILogger<JourneyController> _logger;
        private readonly IMapper _mapper;

        public JourneyController(ILogger<JourneyController> logger, IJourneyService journeyService, IMapper mapper)
        {
            _logger = logger;
            _journeyService = journeyService;
            _mapper = mapper;
        }

        [HttpPost("AddJourney")]
        public async Task<JsonResult> AddJourney([FromBody] JourneyDTO journeyDTO)
        {
            _logger.LogInformation($"User: {journeyDTO.UserId} , Ürün :{journeyDTO.CityCodeFrom} için sepet ekleme çağrıldı.");

            //var shoppingCart = _mapper.Map<ShoppingCartDTO, ShoppingCart>(shoppingCartDTO);

            var serviceResponse = await _journeyService.AddJourney(journeyDTO);

            return Json(serviceResponse);
        }

        [HttpPost("AddJourneyWithRoutes")]
        public async Task<JsonResult> AddJourneyWithRoutes([FromBody] JourneyWithRoutesDTO journeyWithRoutesDTO)
        {

            var serviceResponse = await _journeyService.AddJourneyWithRoute(journeyWithRoutesDTO);

            return Json(serviceResponse);
        }

        [HttpPost("ChangeJourneyState")]
        public async Task<JsonResult> ChangeJourneyState([FromBody] JourneyChangeStateDTO journeyChangeStateDTO)
        {

            var serviceResponse = await _journeyService.ChangeJourneyState(journeyChangeStateDTO);

            return Json(serviceResponse);
        }

        [HttpGet("GetAllAvaliableJourney")]
        public async Task<ActionResult<List<JourneyResponseDTO>>> GetAllAvaliableJourney(int cityCodeFrom, int cityCodeTo)
        {
            var serviceResponse = await _journeyService.GetAllAvaliableJourney(cityCodeFrom, cityCodeTo);

            return Json(serviceResponse);
        }
        [HttpGet("GetAllAvaliableJourneyWithRoute")]
        public async Task<ActionResult<List<JourneyResponseDTO>>> GetAllAvaliableJourneyWithRoute(int cityCodeFrom, int cityCodeTo)
        {
            var serviceResponse = await _journeyService.GetAllAvaliableJourneyWithRoute(cityCodeFrom, cityCodeTo);

            return Json(serviceResponse);
        }
    }
}