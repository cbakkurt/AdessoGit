using AdessoRideShare.DataAccess.IRepositories;
using AdessoRideShare.Domain;
using AdessoRideShare.Domain.DTO;
using AdessoRideShare.Domain.IContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.DataAccess.Repositories
{
    public class JourneyRepository : Repository<Journey>, IJourneyRepository
    {
        public JourneyRepository(IAdessoDbContext context)
           : base(context)
        { }

        public async Task<List<JourneyResponseDTO>> GetJourney(int cityCodeFrom, int cityCodeTo)
        {
            var list = await _context.Journeys
                .Include(x => x.JourneyRoutes)
                .Where(x => x.PublishingState == true
                && x.SeatCount > 0
                && x.JourneyRoutes.Any(y => y.CityId == cityCodeFrom && y.IsStartCity == true)
                && x.JourneyRoutes.Any(y => y.CityId == cityCodeTo && y.IsEndCity == true))
                .Select(x => new JourneyResponseDTO
                {
                    JourneyId = x.Id,
                    SeatCount = x.SeatCount
                }).ToListAsync();

            return list;
        }

        public async Task<List<JourneyResponseDTO>> GetJourneyWithRoute(int cityCodeFrom, int cityCodeTo)
        {
            var list = await _context.Journeys
               .Include(x => x.JourneyRoutes)
               .Where(x => x.PublishingState == true
               && x.SeatCount > 0
               && x.JourneyRoutes.Any(y => y.CityId == cityCodeFrom)
               && x.JourneyRoutes.Any(y => y.CityId == cityCodeTo))
               .Select(x => new JourneyResponseDTO
               {
                   JourneyId = x.Id,
                   SeatCount = x.SeatCount
               }).ToListAsync();

            return list;
        }
    }
}
