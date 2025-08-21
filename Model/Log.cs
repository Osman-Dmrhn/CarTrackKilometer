using CarKilometerTrack.Core;

namespace CarKilometerTrack.Model
{
    public class Log :MainModel
    {
        public int userId { get; set; }
        public User user { get; set; }

        public int? carId { get; set; }
        public Car? car { get; set; }

        public string Action {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
