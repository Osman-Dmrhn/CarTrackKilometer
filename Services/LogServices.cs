using AutoMapper;
using CarKilometerTrack.AppDbConnect;
using CarKilometerTrack.Dtos;
using CarKilometerTrack.Dtos.UserDtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Interfaces;
using CarKilometerTrack.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CarKilometerTrack.Services
{
    public class LogServices : ILogServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public LogServices(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async void addLog(string action, int userId)
        {
            var log = new Log
            {
                Action = action,
                userId = userId,
                CreatedAt = DateTime.Now,
            };

            await _context.Logs.AddAsync(log);
        }

        public async void addLog(string action, int userId, int carId)
        {
            var log = new Log
            {
                Action = action,
                userId = userId,
                carId = carId,
                CreatedAt = DateTime.Now,
            };

            await _context.Logs.AddAsync(log);
        }


        public async Task<ResponseHelper<IEnumerable<LogDto>>> GetAllLogs()
        {
            var logs = await _context.Logs.Select(x =>
                new LogDto
                {
                    Action = x.Action,
                    CreatedAt=x.CreatedAt,
                    user=new UserDto
                    {
                        Username=x.user.Username,
                        Name=x.user.Name,
                        Surname=x.user.Surname,
                    },
                    carId=x.carId,
                    car=new Dtos.CarDtos.CarDto
                    {
                        Brand=x.car.Brand,
                        Model = x.car.Model,
                        LicensePlate = x.car.LicensePlate,
                    }
                }
                ).ToListAsync();       
            if (logs.Count > 0) {
                var data = _mapper.Map<IEnumerable<LogDto>>(logs);
                return ResponseHelper<IEnumerable<LogDto>>.Ok(data);
            } 
            return ResponseHelper<IEnumerable<LogDto>>.Fail("Log Kaydı Bulunamadı");
        }

        public async Task<ResponseHelper<IEnumerable<LogDto>>> GetAllCarLogs()
        {
            var logs = await _context.Logs.Where(x=>x.carId!=null&&x.car.IsActive==true).Select(x =>
                new LogDto
                {
                    Action = x.Action,
                    CreatedAt = x.CreatedAt,
                    user = new UserDto
                    {
                        Username = x.user.Username,
                        Name = x.user.Name,
                        Surname = x.user.Surname,
                    },
                    carId = x.carId,
                    car = new Dtos.CarDtos.CarDto
                    {
                        Brand = x.car.Brand,
                        Model = x.car.Model,
                        LicensePlate = x.car.LicensePlate,
                    }
                }
                ).ToListAsync();
            if (logs.Count > 0)
            {
                var data = _mapper.Map<IEnumerable<LogDto>>(logs);
                return ResponseHelper<IEnumerable<LogDto>>.Ok(data);
            }
            return ResponseHelper<IEnumerable<LogDto>>.Fail("Log Kaydı Bulunamadı");
        }

        public async Task<ResponseHelper<IEnumerable<LogDto>>> GetAllLogsByCarReport(int id)
        {

            var logs = await _context.Logs.Where(x => x.carId == id).OrderByDescending(x => x.CreatedAt).Select(x =>
            new LogDto
            {
                Action = x.Action,
                CreatedAt = x.CreatedAt,
                user = new UserDto
                {
                    Name = x.user.Name,
                    Surname = x.user.Surname,
                    Username = x.user.Username,
                },
                carId = x.carId,
                car = new Dtos.CarDtos.CarDto
                {
                    Brand = x.car.Brand,
                    Model = x.car.Model,
                    LicensePlate = x.car.LicensePlate,
                    InUse = x.car.InUse,
                    InUseUserId = x.car.InUseUserId,
                    Kilometer = x.car.Kilometer,
                    LastMaintenance = x.car.LastMaintenance,
                    PeriodicKilometer = x.car.PeriodicKilometer,
                    UseNote = x.car.UseNote ?? "",
                }
            }
               ).ToListAsync();
            if (logs.Count > 0)
            {
                var data = _mapper.Map<IEnumerable<LogDto>>(logs);
                return ResponseHelper<IEnumerable<LogDto>>.Ok(data);
            }
            return ResponseHelper<IEnumerable<LogDto>>.Fail("Log Kaydı Bulunamadı");
        }

        public async Task<ResponseHelper<LogDto>> GetLogById(int id)
        {
            var logs = await _context.Logs.FindAsync(id);
            if (logs == null) return ResponseHelper<LogDto>.Fail("Log Kaydı Bulunamadı");
            var data = _mapper.Map<LogDto>(logs);
            return ResponseHelper<LogDto>.Ok(data);
        }

        public async Task<ResponseHelper<PaginationHelper<LogDto>>> GetAllLogsByCar(int Carid,int take,int page,string searchString)
        {
            var totalCount = await _context.Logs.Where(x => x.carId == Carid).CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / take);

            var query =  _context.Logs.Where(x => x.carId == Carid);

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.user.Name.Contains(searchString) || x.Action.Contains(searchString));
            }

            query = query.OrderByDescending(x => x.CreatedAt);

            var logs = await query.Skip((page-1)*take).Take(take).Select(x =>      
            new LogDto
               {
                   Action = x.Action,
                   CreatedAt = x.CreatedAt,
                   user = new UserDto
                   {
                       Name=x.user.Name,
                       Surname=x.user.Surname,
                       Username = x.user.Username,
                   },
                   carId = x.carId,
                   car = new Dtos.CarDtos.CarDto
                   {
                       Brand = x.car.Brand,
                       Model = x.car.Model,
                       LicensePlate = x.car.LicensePlate,
                       InUse = x.car.InUse,
                       InUseUserId = x.car.InUseUserId,
                       Kilometer = x.car.Kilometer,
                       LastMaintenance=x.car.LastMaintenance,
                       PeriodicKilometer = x.car.PeriodicKilometer,
                       UseNote= x.car.UseNote??"",
                   }
               }
               ).ToListAsync();
            if (logs.Count <= 0)
            {
                return ResponseHelper<PaginationHelper<LogDto>>.Fail("Log Kaydı Bulunamadı");    
            }
            var data = _mapper.Map<IEnumerable<LogDto>>(logs);

            var datas = new PaginationHelper<LogDto>
            {
                data = logs,
                total_items = totalCount,
                total_pages = totalPages,
                current_page = page,
                previous_page = page > 1 ? page - 1 : 0,
                next_page = page < totalPages ? page + 1 : 0,
                take = take
            };

            return ResponseHelper<PaginationHelper<LogDto>>.Ok(datas);

        }

        public async Task<ResponseHelper<bool>> DeleteAllLogsByCar(int Carid)
        {
            var logs =_context.Logs.Where(x=>x.carId == Carid);
            if (logs is null) { return ResponseHelper<bool>.Fail("Kayıt Bulunamadı"); }
            _context.Logs.RemoveRange(logs);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }

        public async Task<ResponseHelper<bool>> DeleteAllCarLogs()
        {
            var logs = _context.Logs.Where(x => x.carId !=null);
            if(logs is null) { return ResponseHelper<bool>.Fail("Kayıt Bulunamadı"); }
            _context.Logs.RemoveRange(logs);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }

        public async Task<ResponseHelper<PaginationHelper<LogDto>>> GetAllLogsByUser( int page, int take,string searchString)
        {
            var totalCount = await _context.Logs.Where(x=>x.carId==null).CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / take);

            var query = _context.Logs.Where(x => x.carId == null);

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Action.Contains(searchString));
            }

            query = query.OrderByDescending(x => x.CreatedAt);

            var logs = await query.Skip((page-1)*take).Take(take).Select(x =>
               new LogDto
               {
                   Action = x.Action,
                   CreatedAt = x.CreatedAt,
                   user = new UserDto
                   {
                       Username = x.user.Username,
                   },
                   carId = x.carId,
                   car = new Dtos.CarDtos.CarDto
                   {
                       Brand = x.car.Brand,
                       Model = x.car.Model,
                       LicensePlate = x.car.LicensePlate,
                   }
               }
               ).ToListAsync();
            var datas = new PaginationHelper<LogDto>
            {
                data = logs,
                total_items = totalCount,
                total_pages = totalPages,
                current_page = page,
                previous_page = page > 1 ? page - 1 : 0,
                next_page = page < totalPages ? page + 1 : 0,
                take=take
            };
            if (logs.Count > 0)
            {
                var data = _mapper.Map<IEnumerable<LogDto>>(logs);
                return ResponseHelper<PaginationHelper<LogDto>>.Ok(datas);
            }
            return ResponseHelper<PaginationHelper<LogDto>>.Fail("Log Kaydı Bulunamadı");
        }

        public  async Task<IEnumerable<LogDto>> GetAllLogsByUserReport()
        {
            var logs = await  _context.Logs.Where(x => x.carId == null).Select(x => new LogDto
            {
                Action = x.Action,
                CreatedAt = x.CreatedAt,
                userId=x.userId,
                user = new UserDto
                {
                    Username = x.user.Username,
                    Name=x.user.Name,
                    Surname= x.user.Surname,
                },
            }).ToListAsync();
            return logs;
        }

        public async Task<ResponseHelper<bool>> DeleteAllUserLogs()
        {
            var logs =  _context.Logs.Where(x => x.carId == null);
            if (logs is null) { ResponseHelper<bool>.Fail("Kayıt Bulunamadı"); }
            _context.Logs.RemoveRange(logs);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }


    }
}
