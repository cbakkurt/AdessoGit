using System;

namespace AdessoRideShare.Domain
{
    public class JourneyBooking
    {
        public Guid Id { get; set; }
        public Guid JourneyId { get; set; }
        public Guid UserId { get; set; }
    }
}
