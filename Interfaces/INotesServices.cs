using CarKilometerTrack.Dtos.NotesDtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Model;

namespace CarKilometerTrack.Interfaces
{
    public interface INotesServices
    {
        Task<ResponseHelper<IEnumerable<NoteDto>>> getAllNoteByCarId(int id);

        Task<ResponseHelper<NoteDto>> getNoteById(int id);
        Task<ResponseHelper<bool>> AddNote(AddNoteDto data);
        Task<ResponseHelper<bool>> UpdateNote(int id, UpdateNoteDto data);

        Task<ResponseHelper<bool>> deleteAllNoteByCarId(int id);

        Task<ResponseHelper<bool>> UpdateNoteStatus(int id);
        Task<ResponseHelper<bool>> DeleteNote(int id);
    }
}
