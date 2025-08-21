using System.ComponentModel.DataAnnotations;

namespace CarKilometerTrack.Dtos.CarDtos
{
    public class CarAddDto
    {
        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Kilometer { get; set; }

        [Required]
        public int PeriodicKilometer { get; set; }

        [Required]
        public int LastMaintenance { get; set; }

        [Required]
        public DateTime Inspection { get; set; }

        [Required]
        public DateTime Insurance { get; set; }

        [Required]
        public int PeriodicInspection { get; set; }

    }
}
