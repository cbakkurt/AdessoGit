using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Domain.DTO
{
    public class JourneyChangeStateDTO
    {
        public Guid JourneyId { get; set; }
        public bool State { get; set; }
    }
}
