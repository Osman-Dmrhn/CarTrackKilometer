using AutoMapper;
using CarKilometerTrack.AppDbConnect;
using CarKilometerTrack.Dtos;
using CarKilometerTrack.Dtos.CarDtos;
using CarKilometerTrack.Dtos.UserDtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Interfaces;
using CarKilometerTrack.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace CarKilometerTrack.Services
{
    public class CarServices : ICarServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogServices _logservices;
        private readonly IUserService _userservices;

        public CarServices(ApplicationDbContext context, IMapper mapper, ILogServices logservices,IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _logservices = logservices;
            _userservices = userService;
        }

        public async Task<ResponseHelper<bool>> AddCarAsync(CarAddDto data,int userId)
        {
            var car = _mapper.Map<Car>(data);
            await _context.cars.AddAsync(car);
            _logservices.addLog(car.LicensePlate + " plakalı araç eklendi.",userId);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }

        public async Task<ResponseHelper<bool>> UpdateCarAsync(int id , CarAddDto data, int userId)
        {
            var car= await _context.cars.FindAsync(id);
            if(car is not null)
            {
                _mapper.Map(data, car);
                _context.cars.Update(car);
                _logservices.addLog("Araç güncellendi.",userId,id);
                await _context.SaveChangesAsync();
                return ResponseHelper<bool>.Ok(true);
            }
            return ResponseHelper<bool>.Fail("Araç Bulunamadı");
        }

        public async Task<ResponseHelper<bool>> CarKilometerUpdateAsync(int id, int km, int userId)
        {
            var car = await _context.cars.FindAsync(id);
            if(car is not null)
            {
                if (km <= 0) return ResponseHelper<bool>.Fail("Girilen Kilometre Değeri 0'dan Büyük Olmalıdır.");
                var oldKilometer=car.Kilometer;
                car.Kilometer = km;
                car.InUse = false;
                car.InUseUserId = null;
                car.UseNote = string.Empty;
                _context.cars.Update(car);
                _logservices.addLog("Kilometresi güncellendi.Sürüş Mesafesi: "+(km-oldKilometer)+" km",userId,id);
                await _context.SaveChangesAsync();
                return ResponseHelper<bool>.Ok(true);
            }        
            return ResponseHelper<bool>.Fail("Araç Bulunamadı");
        }

        public async Task<ResponseHelper<bool>> DeleteCarAsync(int id, int userId)
        {
            var car = await _context.cars.FindAsync(id);
            if (car is not null)
            {
                car.IsActive = false;
                _context.cars.Update(car);
                _logservices.addLog("Araç silindi.", userId, id);
                await _context.SaveChangesAsync();
                return ResponseHelper<bool>.Ok(true);
            }
            return ResponseHelper<bool>.Fail("Araç Bulunamadı");
        }

        public async Task<ResponseHelper<PaginationHelper<CarDto>>> GetAllCarsAsync(int page, int take, string searchString)
        {
            var totalCount = await _context.cars.Where(x => x.IsActive==true).CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / take);

            var query = _context.cars.Where(x => x.IsActive == true);

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(x =>x.LicensePlate.Contains(searchString));
            }

            query = query.OrderByDescending(x => x.Brand).Skip((page-1)*take).Take(take);


            var cars= await query.Select(x=>new CarDto
               {
                    Brand=x.Brand,
                    Model=x.Model,
                    LicensePlate=x.LicensePlate,
                    InUse=x.InUse,
                    id=x.Id,
                    Kilometer=x.Kilometer,
                    PeriodicKilometer=x.PeriodicKilometer,
                    LastMaintenance=x.LastMaintenance,
                    UseNote=x.UseNote ?? null,

                    Inspection=x.Inspection,
                    PeriodicInspection=x.PeriodicInspection,
                    Insurance=x.Insurance,
                    Notes=_context.Notes.Where(z=>z.carId==x.Id).Where(y=>y.isRead==false).Count()>0?true:false,

                    InUseUserId = x.InUseUserId ?? null,
                    InUseUser = x.InUseUser != null
                    ? 
                    new UserDto
                    {
                    Id = x.InUseUser.Id,        
                    Username = x.InUseUser.Username,
                    Name = x.InUseUser.Name,
                    Surname = x.InUseUser.Surname,   
                    Role = x.InUseUser.Role
                    }     
                    :null
               }
               ).ToListAsync();

            if (cars.Count <= 0)
            {
                return ResponseHelper<PaginationHelper<CarDto>>.Fail("Kayıtlı Araç Bulunamadı");
            }

            var data = _mapper.Map<IEnumerable<CarDto>>(cars);

            var datas = new PaginationHelper<CarDto>
            {
                data = cars,
                total_items = totalCount,
                total_pages = totalPages,
                current_page = page,
                previous_page = page > 1 ? page - 1 : 0,
                next_page = page < totalPages ? page + 1 : 0,
                take = take
            };
 
                return ResponseHelper<PaginationHelper<CarDto>>.Ok(datas);         
        }

        public async Task<ResponseHelper<CarDto>> GetCarsAsync(int id)
        {
            var car = await  _context.cars.Where(x => x.Id == id).Select(x => new CarDto
            {
                Brand = x.Brand,
                Model = x.Model,
                LicensePlate = x.LicensePlate,
                InUse = x.InUse,
                id = x.Id,
                Kilometer = x.Kilometer,
                PeriodicKilometer = x.PeriodicKilometer,
                LastMaintenance = x.LastMaintenance,
                UseNote = x.UseNote ?? null,
                Inspection=x.Inspection,
                Insurance=x.Insurance,
                PeriodicInspection=x.PeriodicInspection,

                InUseUserId = x.InUseUserId ?? null,
                InUseUser = x.InUseUser != null
                    ?
                    new UserDto
                    {
                        Id = x.InUseUser.Id,
                        Username = x.InUseUser.Username,
                        Name = x.InUseUser.Name,
                        Surname = x.InUseUser.Surname,
                        Role = x.InUseUser.Role
                    }
                    : null
            }
            ).SingleOrDefaultAsync();

            var data =_mapper.Map<CarDto>(car);
            if(car is not null) return ResponseHelper<CarDto>.Ok(data);
            return ResponseHelper<CarDto>.Fail("Araç Bulunamadı");
        }

        public async Task<ResponseHelper<IEnumerable<CarDto>>> GetAllCars()
        {
          

            var cars = await _context.cars.Where(x => x.IsActive == true).OrderByDescending(x => x.Brand).Select(x => new CarDto
            {
                Brand = x.Brand,
                Model = x.Model,
                LicensePlate = x.LicensePlate,
                InUse = x.InUse,
                id = x.Id,
                Kilometer = x.Kilometer,
                PeriodicKilometer = x.PeriodicKilometer,
                LastMaintenance = x.LastMaintenance,
                UseNote = x.UseNote ?? null,

                Inspection = x.Inspection,
                PeriodicInspection = x.PeriodicInspection,
                Insurance = x.Insurance,

                InUseUserId = x.InUseUserId ?? null,
                InUseUser = x.InUseUser != null
                    ?
                    new UserDto
                    {
                        Id = x.InUseUser.Id,
                        Username = x.InUseUser.Username,
                        Name = x.InUseUser.Name,
                        Surname = x.InUseUser.Surname,
                        Role = x.InUseUser.Role
                    }
                    : null
            }
               ).ToListAsync();
            if (cars.Count > 0)
            {
                var data = _mapper.Map<IEnumerable<CarDto>>(cars);
                return ResponseHelper<IEnumerable<CarDto>>.Ok(data);
            }
            return ResponseHelper<IEnumerable<CarDto>>.Fail("Kayıtlı Araç Bulunamadı");
        }


        public async Task<ResponseHelper<bool>> CarLastMaintenceUpdateAsync(int id, int km, int userId)
        {
            var car = await _context.cars.FindAsync(id);
            if (car is not null)
            {
                if (car.Kilometer < km)
                {
                    return ResponseHelper<bool>.Fail("Bakım Kilometresi Araç Kilometresinden Fazla Olamaz.");
                }
                car.LastMaintenance = km;
                _context.cars.Update(car);
                _logservices.addLog("Bakım kilometresi güncellendi.",userId,id);
                await _context.SaveChangesAsync();
                return ResponseHelper<bool>.Ok(true);
            }
            return ResponseHelper<bool>.Fail("Araç Bulunamadı");
        }



        public async Task<ResponseHelper<bool>> CarInUseUpdate(int id, CarUseUpdateDto data , int userId)
        {
            var car = await _context.cars.FindAsync(id);
            if (car is null) { return ResponseHelper<bool>.Fail("Araç Bulunamadı"); }
            car.InUseUserId=userId;
            car.InUse = true;
            car.UseNote = data.UseNote;
            _logservices.addLog("Aracı teslim Aldı.Araç KM: "+car.Kilometer, userId,car.Id);
            _context.cars.Update(car);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }



    }
}
