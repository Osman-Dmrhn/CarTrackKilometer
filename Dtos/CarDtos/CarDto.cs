using CarKilometerTrack.Dtos.UserDtos;
using CarKilometerTrack.Model;
using System.ComponentModel.DataAnnotations;

namespace CarKilometerTrack.Dtos.CarDtos
{
    public class CarDto
    {
        public int id { get; set; }
        public string LicensePlate { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Kilometer { get; set; }

        public int PeriodicKilometer { get; set; }

        public int LastMaintenance { get; set; }

        public bool InUse { get; set; }

        public int PeriodicInspection { get; set; }

        public int? InUseUserId { get; set; }
        public UserDto? InUseUser { get; set; }

        public string? UseNote { get; set; }

        public bool Notes { get; set; } 

        public DateTime Inspection { get; set; }

        public DateTime Insurance { get; set; }
    }
}
