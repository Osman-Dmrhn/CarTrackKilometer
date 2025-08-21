using System.ComponentModel.DataAnnotations;

namespace CarKilometerTrack.Dtos.UserDtos
{
    public class UserLoginDto
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
