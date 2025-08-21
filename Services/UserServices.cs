using AutoMapper;
using BCrypt.Net;
using CarKilometerTrack.AppDbConnect;
using CarKilometerTrack.Dtos.UserDtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Interfaces;
using CarKilometerTrack.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace CarKilometerTrack.Services
{
    public class UserServices : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogServices _logservices;
        private readonly IMapper _mapper;

        public UserServices(ApplicationDbContext context,IMapper mapper,ILogServices logServices)
        {
            _context = context;
            _mapper = mapper;
            _logservices = logServices;
        }

        public async Task<ResponseHelper<bool>> AddUserAsync(UserAddDto data, int userId)
        {
            data.Password=BCrypt.Net.BCrypt.HashPassword(data.Password);
            var user = _mapper.Map<User>(data);
            await _context.users.AddAsync(user);
            _logservices.addLog("Kullanıcı Ekledi.Eklenen Kullanıcı: "+ data.Username,userId);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }

        public async Task<ResponseHelper<bool>> UpdateUserAsync(int id, UserUpdateDto data, int userId)
        {
            var user = await _context.users.FindAsync(id);
            if(user == null) {return ResponseHelper<bool>.Fail("Kullanıcı Bulunamadı");}
            user = _mapper.Map(data,user);
            _context.users.Update(user);
            _logservices.addLog("Kullanıcı Güncelledi.Güncellenen Kullanıcı: " + data.Username, userId);
            await _context.SaveChangesAsync();
            return ResponseHelper<bool>.Ok(true);
        }

        public async Task<ResponseHelper<bool>> DeleteUserAsync(int id, int userId)
        {
            var user = await _context.users.FindAsync(id);
            if(user is not null)
            {
                user.isActive= false;
                _context.users.Update(user);
                _logservices.addLog("Kullanıcı Sildi.Silinen Kullanıcı: " + user.Username, userId);
                await _context.SaveChangesAsync();
                return ResponseHelper<bool>.Ok(true);
            }
            return ResponseHelper<bool>.Fail("Kullanıcı Bulunamadı");
        }

        public async Task<ResponseHelper<PaginationHelper<UserDto>>> GetAllUsersAsync(int page,int take, string searchString)
        {
            var totalCount = await _context.users.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / take);

            var query = _context.users.Where(x => x.isActive == true);

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString) || x.Surname.Contains(searchString));
            }

            query = query.OrderByDescending(x => x.Name);


            var users = await query.Skip((page-1)*take).Take(take).ToListAsync();

            if (users.Count > 0)
            {
                var data = _mapper.Map<IEnumerable<UserDto>>(users);
                PaginationHelper<UserDto> datas =new PaginationHelper<UserDto>
                {
                    data = data,
                    total_items = totalCount,
                    total_pages = totalPages,
                    current_page = page,
                    next_page = page < totalPages ? page + 1 : 0,
                    previous_page = 1 < page ? page - 1 : 0,
                    take = take
                };
                return ResponseHelper<PaginationHelper<UserDto>>.Ok(datas);
            }
            return ResponseHelper<PaginationHelper<UserDto>>.Fail("Kullanıcı Bulunamadı");
        }

        public async Task<ResponseHelper<UserDto>> GetUserAsync(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null) { return ResponseHelper<UserDto>.Fail("Kullanıcı Bulunamadı"); }
            var data =_mapper.Map<UserDto>(user);
            return ResponseHelper<UserDto>.Ok(data);
        }

        public async Task<ResponseHelper<string>> VerificationUser(UserLoginDto data)
        {
            var user= await _context.users.FirstOrDefaultAsync(u=>u.Username==data.username);
            if (user == null) { return ResponseHelper<string>.Fail("Kullanıcı Bulunamadı"); }
            var passhash = BCrypt.Net.BCrypt.HashPassword(data.password);
            if (BCrypt.Net.BCrypt.Verify(data.password,user.Password)) {
                return ResponseHelper<string>.Ok(JwtHelper.GenerateJwtKey(user.Username, user.Id,user.Role));
            }
            return ResponseHelper<string>.Fail("Yanlış Şifre");

        }

        public async Task<ResponseHelper<MeResponse>> GetUserRoleById(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null) { return ResponseHelper<MeResponse>.Fail("Kullanıcı Bulunamadı"); }
            return ResponseHelper<MeResponse>.Ok(new MeResponse
            {
                username = user.Username,
                Role=user.Role
            });
        }

        public async Task<ResponseHelper<bool>> UpdateUserPassAsync(UserPassDto data,int userId)
        {
            var user = await  _context.users.FindAsync(userId);
            if(user == null) { return ResponseHelper<bool>.Fail("Kullanıcı Bulunamadı"); }
            if (BCrypt.Net.BCrypt.Verify(data.oldPassword, user.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(data.newPassword);
                await _context.SaveChangesAsync();
            }
            return ResponseHelper<bool>.Ok(true);
        }

        public async Task<ResponseHelper<string>> ResetUserPass(int userId)
        {
            var user = await _context.users.FindAsync(userId);
            if (user == null) { return ResponseHelper<string>.Fail("Kullanıcı Bulunamadı"); }
            var newpass=GenerateFromCharsetHelpers.GenerateAlphaNumericPassword(8);
            user.Password=BCrypt.Net.BCrypt.HashPassword(newpass);
            await _context.SaveChangesAsync();
            return ResponseHelper<string>.Ok(newpass);
        }

        public  async Task<string> GetUsernameById(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null) { return "Kullanıcı Bulunamadı"; }
            return user.Username;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersReport()
        {
            var data = await _context.users.Where(x=>x.isActive).ToListAsync();
            var datas = _mapper.Map<IEnumerable<UserDto>>(data);

            return datas;
        }
    }
}
