using System.ComponentModel.DataAnnotations;

namespace CarKilometerTrack.Dtos.UserDtos
{
    public class UserAddDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
