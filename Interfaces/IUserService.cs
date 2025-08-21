using CarKilometerTrack.Dtos.UserDtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Model;

namespace CarKilometerTrack.Interfaces
{
    public interface IUserService
    {
        Task<ResponseHelper<PaginationHelper<UserDto>>> GetAllUsersAsync(int page,int take, string searchString);

        Task<IEnumerable<UserDto>> GetAllUsersReport();


        Task<ResponseHelper<string>> VerificationUser(UserLoginDto data);

        Task<ResponseHelper<UserDto>> GetUserAsync(int id);

        Task<ResponseHelper<MeResponse>> GetUserRoleById(int id);

        Task<string> GetUsernameById(int id);

        Task<ResponseHelper<bool>> AddUserAsync(UserAddDto data,int userId);
        Task<ResponseHelper<bool>> UpdateUserAsync(int id, UserUpdateDto data, int userId);

        Task<ResponseHelper<bool>> UpdateUserPassAsync(UserPassDto data, int userId);

        Task<ResponseHelper<string>> ResetUserPass(int userId);

        Task<ResponseHelper<bool>> DeleteUserAsync(int id,int userId);

    }
}
