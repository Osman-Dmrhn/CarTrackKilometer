using CarKilometerTrack.Dtos.CarDtos;
using CarKilometerTrack.Dtos.UserDtos;
using CarKilometerTrack.Model;

namespace CarKilometerTrack.Dtos
{
    public class LogDto
    {

        public int userId { get; set; }
        public UserDto user { get; set; }

        public int? carId { get; set; }
        public CarDto? car { get; set; }

        int id { get; set; }
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
