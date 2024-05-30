using MainRequestTrackerAPI.Models.DTOs;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;

namespace MatrimonialApp.Interfaces
{
    public interface IUserService
    {
        public Task<LoginReturnDTO> Login(UserLoginDTO loginDTO);
        public Task<User> Register(UserDTO userDTO);
        public Task<IEnumerable<MatchDetailsDTO>> GetMyMatchs(int id);
        public Task<IEnumerable<MatchReturnDTO>> FindMyMatch(int UserId,MatchDTO matchDTO);
    }
}
