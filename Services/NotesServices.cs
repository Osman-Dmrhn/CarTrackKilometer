using AutoMapper;
using CarKilometerTrack.AppDbConnect;
using CarKilometerTrack.Dtos.NotesDtos;
using CarKilometerTrack.Dtos.UserDtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Interfaces;
using CarKilometerTrack.Model;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarKilometerTrack.Services
{
    public class NotesServices : INotesServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public NotesServices(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseHelper<bool>> AddNote(AddNoteDto data)
        {
            var newnote =_mapper.Map<Note>(data);
            await _context.Notes.AddAsync(newnote);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }

        public async Task<ResponseHelper<bool>> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if(note == null) { return ResponseHelper<bool>.Fail("Not Bulunamadı"); }
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }

        public async Task<ResponseHelper<IEnumerable<NoteDto>>> getAllNoteByCarId(int id)
        {
            var data = await _context.Notes.Where(x => x.carId == id).OrderByDescending(x=>x.createdAt).Select(x=>new NoteDto
            {
                id=x.Id,
                notes=x.notes,
                CreatedAt = x.createdAt,
                isRead=x.isRead,
                carId=x.carId,
                userId=x.userId,
                user=new UserDto
                {
                    Username=x.user.Username,
                    Name=x.user.Name,
                    Surname=x.user.Surname,
                }
            }).ToListAsync();
            if(data.Count() > 0) {
                return ResponseHelper<IEnumerable<NoteDto>>.Ok(data);
            }
            return ResponseHelper<IEnumerable<NoteDto>>.Fail("Araç için not bulunamadı");
        }

        public async Task<ResponseHelper<bool>> deleteAllNoteByCarId(int id)
        {
            var data =  _context.Notes.Where(x => x.carId == id);
            if (data == null) { return ResponseHelper<bool>.Fail("Araç Notu Bulunamadı"); }
            _context.Notes.RemoveRange(data);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);

        }

        public async Task<ResponseHelper<NoteDto>> getNoteById(int id)
        {
            var data = await _context.Notes.FindAsync(id);
            var result = _mapper.Map<NoteDto>(data);
            if (data is not null) { return ResponseHelper<NoteDto>.Ok(result); }
            return ResponseHelper<NoteDto>.Fail("Not bulunamadı");
        }

        public async Task<ResponseHelper<bool>> UpdateNote(int id,UpdateNoteDto data)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) { return ResponseHelper<bool>.Fail("Not Bulunamadı"); }
            _mapper.Map(data, note);
            _context.Update(note);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }

        public async Task<ResponseHelper<bool>> UpdateNoteStatus(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) { return ResponseHelper<bool>.Fail("Not Bulunamadı"); }
            note.isRead=true;
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }

    }
}
