using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdessoRideShare.Domain.DTO
{
    public class JourneyDTO
    {
        public Guid UserId { get; set; }

        [Range(1, 100, ErrorMessage = "Başlangıç şehir kodu 1 ile 100 arasında olmalıdır.")]
        public int CityCodeFrom { get; set; }

        [Range(1, 100, ErrorMessage = "Gidilecek şehir kodu 1 ile 100 arasında olmalıdır.")]
        public int CityCodeTo { get; set; }
        public DateTime JourneyDate { get; set; }
        public string Description { get; set; }
        
        [Range(1, 10, ErrorMessage = "Koltuk sayısı 1 ile 10 arasında olmalıdır.")]
        public int SeatCount { get; set; }
    }
}
