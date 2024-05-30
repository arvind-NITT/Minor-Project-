using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;

namespace MatrimonialApp.Interfaces
{
    public interface IProfileService
    {
        Task<Profile> AddMyProfile(int UserId,ProfileDTO profile);
        Task<Profile> GetMyProfile(int userId);
        Task<Profile> UpdateMyProfile(int userId,ProfileDTO profile);
        Task<Profile> DeleteMyProfile(int userId);
    }
}
