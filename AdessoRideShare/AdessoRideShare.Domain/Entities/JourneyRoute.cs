using System;

namespace AdessoRideShare.Domain
{
    public class JourneyRoute
    {
        public Guid Id { get; set; }
        public Guid JourneyId { get; set; }
        public int CityId { get; set; }
        public bool IsStartCity { get; set; }
        public bool IsEndCity { get; set; }

    }
}
