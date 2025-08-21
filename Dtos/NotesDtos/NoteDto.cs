using CarKilometerTrack.Dtos.CarDtos;
using CarKilometerTrack.Dtos.UserDtos;
using CarKilometerTrack.Model;

namespace CarKilometerTrack.Dtos.NotesDtos
{
    public class NoteDto
    {
        public int id { get; set; }
        public int carId { get; set; }
        public CarDto car { get; set; }

        public int userId { get; set; }
        public UserDto user { get; set; }

        public string notes { get; set; }

        public bool isRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
