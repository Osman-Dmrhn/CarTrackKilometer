using CarKilometerTrack.Core;

namespace CarKilometerTrack.Model
{
    public class Note:MainModel
    {
        public int carId { get; set; }
        public Car car { get; set; }

        public int userId { get; set; }
        public User user { get; set; }

        public string notes { get; set; }

        public bool isRead { get; set; } = false;

        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
