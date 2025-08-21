using CarKilometerTrack.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CarKilometerTrack.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> GetAllCarLogs();
        Task<byte[]> GetCarLogByID(int id);

        Task<byte[]> GetAllUserLogs();


    }
}
