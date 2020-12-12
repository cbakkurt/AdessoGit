using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Domain.DTO
{
    public class JourneyResponseDTO
    {
        public Guid JourneyId { get; set; }
        public int SeatCount { get; set; }
    }
}
