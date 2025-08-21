using AutoMapper;
using CarKilometerTrack.Dtos;
using CarKilometerTrack.Dtos.CarDtos;
using CarKilometerTrack.Dtos.NotesDtos;
using CarKilometerTrack.Dtos.UserDtos;
using CarKilometerTrack.Model;

namespace CarKilometerTrack.Helpers
{
    public class AutoMapperHelper:Profile
    {
        public AutoMapperHelper() {

            CreateMap<CarDto, Car>().ReverseMap();
            CreateMap<CarAddDto, Car>().ReverseMap();

            CreateMap<LogDto,Log>().ReverseMap();

            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserAddDto, User>().ReverseMap();
            CreateMap<UserUpdateDto, User>().ReverseMap();

            CreateMap<AddNoteDto,Note>().ReverseMap();
            CreateMap<UpdateNoteDto,Note>().ReverseMap();

        }
    }
}
