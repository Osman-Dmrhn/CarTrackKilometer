using System.ComponentModel.DataAnnotations;

namespace CarKilometerTrack.Dtos.CarDtos
{
    public class CarKilometerUpdate
    {
        [Required]
        public int kilometer { get; set; }
    }
}
