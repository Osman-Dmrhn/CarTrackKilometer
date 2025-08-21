using CarKilometerTrack.Core;

namespace CarKilometerTrack.Model
{
    public class User:MainModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }

        public bool isActive { get; set; }=true;

        public ICollection<Note> Notes { get; } = new List<Note>();

        public ICollection<Log> Logs { get; } = new List<Log>();

    }
}
