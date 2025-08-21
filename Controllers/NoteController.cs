using CarKilometerTrack.Dtos.NotesDtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarKilometerTrack.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INotesServices _notesServices;
        public NoteController(INotesServices notesServices)
        {
            _notesServices = notesServices;
        }

        [Authorize (Roles ="Admin")]
        [HttpGet("getAllNoteByCarId/{id}")]
        public async Task<IActionResult> getAllNoteByCarId(int id)
        {
            var result = await _notesServices.getAllNoteByCarId(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getNoteById/{id}")]
        public async Task<IActionResult> getNoteById(int id)
        {
            var result = await _notesServices.getNoteById(id);
            return Ok(result);
        }


        [HttpPost("addNote")]
        public async Task<IActionResult> addNote([FromBody] AddNoteDto data)
        {
            var userId = User.GetUserId();
            data.userId= userId.Value;
            var result = await _notesServices.AddNote(data);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("updateNote/{id}")]
        public async Task<IActionResult> updateNote([FromBody] UpdateNoteDto data,int id)
        {
            var result = await _notesServices.UpdateNote(id,data);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateStatusNote/{id}")]
        public async Task<IActionResult> updateStatusNode(int id)
        {
            var result = await _notesServices.UpdateNoteStatus(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteNote/{id}")]
        public async Task<IActionResult> deleteNote(int id) {
            var result = await _notesServices.DeleteNote(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteAllNoteByCarId/{id}")]
        public async Task<IActionResult> deleteAllNoteByCarId(int id)
        {
            var result = await _notesServices.deleteAllNoteByCarId(id);
            return Ok(result);
        }
    }
}
