using System;
using System.Collections.Generic;

namespace AdessoRideShare.Domain
{
    public class Journey
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime JourneyDate { get; set; }
        public string Description { get; set; }
        public int SeatCount { get; set; }
        public bool PublishingState { get; set; }

        public List<JourneyRoute> JourneyRoutes { get; set; }
    }
}
