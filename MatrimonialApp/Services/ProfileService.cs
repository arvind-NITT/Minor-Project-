using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MatrimonialApp.Services
{
    public class ProfileService: IProfileService
    {
        private readonly MatrimonialContext _context;
        private readonly IRepository<int, Profile> _profileRepo;

        public ProfileService(MatrimonialContext context, IRepository<int, Profile> profileRepo)
        {
            _context = context;
            _profileRepo = profileRepo;
        }

        public async Task<Profile> AddMyProfile(int userid, ProfileDTO profiledto)
        {
            if (profiledto == null)
            {
                throw new ArgumentNullException(nameof(profiledto), "Profile object cannot be null.");
            }
            var user = await _context.Users.FindAsync(userid);
            if(user == null) { throw new ArgumentNullException(nameof(user)); }
            var puser = await _context.Profiles.FirstOrDefaultAsync(p=> p.UserID == userid);
           
           
            if (puser == null) {
                Profile profile = new Profile
                {

                    UserID = userid,
                    Gender = profiledto.Gender,
                    MaritalStatus = profiledto.MaritalStatus,
                    Height = profiledto.Height,
                    Education = profiledto.Education,
                    Income = profiledto.Income,
                    Religion = profiledto.Religion,
                    Caste = profiledto.Caste,
                    MotherTongue = profiledto.MotherTongue,
                    Interests = profiledto.Interests,
                    PartnerExpectations = profiledto.PartnerExpectations
                };
                var newProfile = await _profileRepo.Add(profile);
            
            return newProfile;
            }
            else
            {
                var newProfile = await UpdateMyProfile(userid, profiledto);

                return newProfile;
            }
        }

        public async Task<Profile> GetMyProfile(int userId)
        {
            var profile = await _profileRepo.Get(userId);
            if (profile == null)
            {
                throw new Exception($"Profile not found for UserID: {userId}");
            }
            return profile;
        }

        public async Task<Profile> UpdateMyProfile(int userId, ProfileDTO profile)
        {
            var existingProfile = await _profileRepo.Get(userId);
            if (existingProfile == null)
            {
                throw new Exception($"Profile not found for UserID: {userId}");
            }

            existingProfile.MaritalStatus = profile.MaritalStatus;
            existingProfile.Height = profile.Height;
            existingProfile.Education = profile.Education;
            existingProfile.Income = profile.Income;
            existingProfile.Religion = profile.Religion;
            existingProfile.Caste = profile.Caste;
            existingProfile.MotherTongue = profile.MotherTongue;
            existingProfile.Interests = profile.Interests;
            existingProfile.PartnerExpectations = profile.PartnerExpectations;

            await _profileRepo.Update(existingProfile);
            return existingProfile;
        }

        public async Task<Profile> DeleteMyProfile(int userId)
        {
            var profile = await _profileRepo.Get(userId);
            if (profile == null)
            {
                throw new Exception($"Profile not found for UserID: {userId}");
            }

            await _profileRepo.Delete(userId);
            return profile;
        }
    }
}
