using CarKilometerTrack.Dtos;
using CarKilometerTrack.Interfaces;
using CarKilometerTrack.Model;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CarKilometerTrack.Services
{
    public class ReportService : IReportService
    {
        private readonly ILogServices _logservices;
        private readonly ICarServices _carServices;
        private readonly IUserService _userServices;

        public ReportService(ILogServices logservices,ICarServices carServices, IUserService userServices)
        {
            _logservices = logservices;
            _carServices = carServices;
            _userServices = userServices;
        }

        public async Task<byte[]> GetAllCarLogs()
        {
            byte[] data;

            var cars = await _carServices.GetAllCars();

            using (var workbook = new XLWorkbook())
            {
                foreach (var car in cars.Data)
                {
                    var logs =await _logservices.GetAllLogsByCarReport(car.id);
                    if(logs.Data is not null)
                    {
                        var worksheet = workbook.AddWorksheet(car.LicensePlate);

                        worksheet.Cell(1, 1).Value = "Araç";
                        worksheet.Cell(1, 2).Value = "Kullanıcı";
                        worksheet.Cell(1, 3).Value = "Tarih";
                        worksheet.Cell(1, 4).Value = "Eylem";

                        int i = 2;

                        foreach (LogDto log in logs.Data)
                        {


                            worksheet.Cell(i, 1).Value = log.car.LicensePlate;
                            worksheet.Cell(i, 2).Value = log.user.Name + " " + log.user.Surname;
                            worksheet.Cell(i, 3).Value = log.CreatedAt;
                            worksheet.Cell(i, 4).Value = log.Action;
                            i++;
                        }
                    }

                }
                

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    data = memoryStream.ToArray();
                }
            }

            return data;
        }

        public async Task<byte[]> GetCarLogByID(int id)
        {
            byte[] data;

            var logs = await _logservices.GetAllLogsByCarReport(id);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("Loglar");

                worksheet.Cell(1, 1).Value = "Araç";
                worksheet.Cell(1, 2).Value = "Kullanıcı";
                worksheet.Cell(1, 3).Value = "Tarih";
                worksheet.Cell(1, 4).Value = "Eylem";

                int i = 2;

                foreach (LogDto log in logs.Data)
                {


                    worksheet.Cell(i, 1).Value = log.car.LicensePlate;
                    worksheet.Cell(i, 2).Value = log.user.Name + " " + log.user.Surname;
                    worksheet.Cell(i, 3).Value = log.CreatedAt;
                    worksheet.Cell(i, 4).Value = log.Action;
                    i++;
                }


            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                data = memoryStream.ToArray();
            }
        }

        return data;
        }
        public async Task<byte[]> GetAllUserLogs()
        {

            byte[] data;

            var logs = await _logservices.GetAllLogsByUserReport();

            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.AddWorksheet("Kullanıcı Logları");

                worksheet.Cell(1, 1).Value = "Kullanıcı";
                worksheet.Cell(1, 2).Value = "Ad-Soyad";
                worksheet.Cell(1, 3).Value = "Tarih";
                worksheet.Cell(1, 4).Value = "Eylem";

                foreach (var log in logs)
                {
                        

                        int i = 2;

                    worksheet.Cell(i, 1).Value = log.user.Username;
                            worksheet.Cell(i, 2).Value = log.user.Name + " " + log.user.Surname;
                            worksheet.Cell(i, 3).Value = log.CreatedAt;
                            worksheet.Cell(i, 4).Value = log.Action;
                            i++;
                }


                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    data = memoryStream.ToArray();
                }
            }

            return data;
        }

    }

}


