using CarKilometerTrack.AppDbConnect;
using CarKilometerTrack.Dtos.CarDtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Model;
using Microsoft.EntityFrameworkCore;

namespace CarKilometerTrack.Interfaces
{
    public interface ICarServices
    {
        Task<ResponseHelper<PaginationHelper<CarDto>>> GetAllCarsAsync(int page,int take,string searchString);

        Task<ResponseHelper<CarDto>> GetCarsAsync(int id);

        Task<ResponseHelper<bool>> AddCarAsync(CarAddDto car, int userId);

        Task<ResponseHelper<bool>> UpdateCarAsync(int id, CarAddDto car, int userId);

        Task<ResponseHelper<bool>> DeleteCarAsync(int id, int userId);

        Task<ResponseHelper<bool>> CarInUseUpdate(int id,CarUseUpdateDto data, int userId);

        Task<ResponseHelper<bool>> CarKilometerUpdateAsync(int id,int km, int userId);

        Task<ResponseHelper<IEnumerable<CarDto>>> GetAllCars();

    }
}
