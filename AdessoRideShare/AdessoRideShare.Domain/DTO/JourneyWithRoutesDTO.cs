using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdessoRideShare.Domain.DTO
{
    public class JourneyWithRoutesDTO
    {
        public Guid UserId { get; set; }

        public List<int> CitiesList { get; set; }
        public DateTime JourneyDate { get; set; }
        public string Description { get; set; }

        [Range(1, 10, ErrorMessage = "Koltuk sayısı 1 ile 10 arasında olmalıdır.")]
        public int SeatCount { get; set; }

    }
}
