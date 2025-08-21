using CarKilometerTrack.Dtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Model;

namespace CarKilometerTrack.Interfaces
{
    public interface ILogServices
    {
        void addLog(string action,int userId);

        void addLog(string action,int userId,int carId);

        Task<ResponseHelper<PaginationHelper<LogDto>>> GetAllLogsByCar(int id, int take, int page, string searchString);

        Task<ResponseHelper<IEnumerable<LogDto>>> GetAllLogsByCarReport(int id);

        Task<ResponseHelper<PaginationHelper<LogDto>>> GetAllLogsByUser(int page,int take, string searchString);

        Task<IEnumerable<LogDto>> GetAllLogsByUserReport();


        Task<ResponseHelper<IEnumerable<LogDto>>> GetAllLogs();

        Task<ResponseHelper<bool>> DeleteAllLogsByCar(int Carid);

        Task<ResponseHelper<bool>> DeleteAllCarLogs();

        Task<ResponseHelper<bool>> DeleteAllUserLogs();

        Task<ResponseHelper<IEnumerable<LogDto>>> GetAllCarLogs();
        Task<ResponseHelper<LogDto>> GetLogById(int id);

    }
}
