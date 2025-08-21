using CarKilometerTrack.Model;

namespace CarKilometerTrack.Dtos.NotesDtos
{
    public class AddNoteDto
    {
        public int carId { get; set; }
        public int? userId { get; set; }
        public string notes { get; set; }

    }
}
