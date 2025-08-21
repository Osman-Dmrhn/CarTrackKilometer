namespace CarKilometerTrack.Dtos
{
    public class FileResultDto
    {
        public required byte[] Content { get; init; }
        public required string FileName { get; init; }
        public required string ContentType { get; init; }
    }
}
