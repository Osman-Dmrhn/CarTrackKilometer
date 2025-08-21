using System.ComponentModel.DataAnnotations;

namespace CarKilometerTrack.Dtos.UserDtos
{
    public class UserUpdateDto
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Role { get; set; }

        public string? Username { get; set; }
    }
}
