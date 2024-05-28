using MainRequestTrackerAPI.Models.DTOs;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;

namespace MatrimonialApp.Interfaces
{
    public interface IUserService
    {
        public Task<LoginReturnDTO> Login(UserLoginDTO loginDTO);
        public Task<User> Register(UserDTO userDTO);
        public Task<Profile> MakeMyProfile(ProfileDTO profileDTO);
        public Task<IEnumerable<MatchDetailsDTO>> GetMyMatchs(LoginReturnDTO loginReturnDTO);
        public Task<IEnumerable<MatchReturnDTO>> FindMyMatch(MatchDTO matchDTO);
    }
}
