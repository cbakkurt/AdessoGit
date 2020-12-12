using System;
using System.ComponentModel.DataAnnotations;

namespace AdessoRideShare.Domain.DTO
{
    public class JourneyBooinkgDTO
    {
        public Guid JourneyId { get; set; }

        public Guid UserId { get; set; }

        [Range(1, 10, ErrorMessage = "Koltuk sayısı 1 ile 10 arasında olmalıdır.")]
        public int SeatCount { get; set; }
    }
}
