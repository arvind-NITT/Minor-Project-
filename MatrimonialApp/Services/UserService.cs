using MatrimonialApp.Interfaces;
using MatrimonialApp.Models.DTOs;
using MatrimonialApp.Models;
using System.Security.Cryptography;
using System.Text;
using MatrimonialApp.Exceptions;
using Microsoft.EntityFrameworkCore;
using MainRequestTrackerAPI.Models.DTOs;
using MatrimonialApp.Contexts;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace MatrimonialApp.Services
{
    public class UserService : IUserService
    {
        private readonly MatrimonialContext _context;
        private readonly IRepository<int, UserDetail> _UserDetailRepo;
        private readonly IRepository<int, User> _UserRepo;
        private readonly IRepository<int, Profile> _ProfileRepo;
        private readonly ITokenService _tokenService;

        public UserService(MatrimonialContext context, IRepository<int, UserDetail> UserDetailRepo, IRepository<int, User> UserRepo, ITokenService tokenService)
        {
            _context = context;
            _UserDetailRepo = UserDetailRepo;
            _UserRepo = UserRepo;
            _tokenService = tokenService;
        }
        public async Task<LoginReturnDTO> Login(UserLoginDTO loginDTO)
        {
            var UserDetailDB = await _UserDetailRepo.Get(loginDTO.UserId);
            if (UserDetailDB == null)
            {
                throw new UnauthorizedUserException("Invalid UserDetailname or password");
            }
            HMACSHA512 hMACSHA = new HMACSHA512(UserDetailDB.PasswordHashKey);
            var encrypterPass = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            bool isPasswordSame = ComparePassword(encrypterPass, UserDetailDB.Password);
            if (isPasswordSame)
            {
                var User = await _UserRepo.Get(loginDTO.UserId);
                // if(UserDetailDB.Status =="Active")
                //{
                LoginReturnDTO loginReturnDTO = MapUserToLoginReturn(User);
                return loginReturnDTO;
                // }

                throw new UserNotActiveException("Your account is not activated");
            }
            throw new UnauthorizedUserException("Invalid UserDetailname or password");
        }

        private bool ComparePassword(byte[] encrypterPass, byte[] password)
        {
            for (int i = 0; i < encrypterPass.Length; i++)
            {
                if (encrypterPass[i] != password[i])
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<User> Register(UserDTO UserDTO)
        {
            User user = null;
            UserDetail UserDetail = null;
            try
            {
                user = UserDTO;
                UserDetail = MapUserUserDetailDTOToUserDetail(UserDTO);
                user = await _UserRepo.Add(user);
                UserDetail.UserId = user.UserId;
                UserDetail = await _UserDetailRepo.Add(UserDetail);
                ((UserDTO)user).Password = string.Empty;

                return user;
            }
            catch (Exception) { }
            if (user != null)
                await RevertUserInsert(user);
            if (UserDetail != null && user == null)
                await RevertUserDetailInsert(UserDetail);
            throw new UnableToRegisterException("Not able to register at this moment");
        }

        private LoginReturnDTO MapUserToLoginReturn(User User)
        {
            LoginReturnDTO returnDTO = new LoginReturnDTO();
            returnDTO.UserID = User.UserId;
            //returnDTO.Role = User.Role ?? "UserDetail";
            returnDTO.Token = _tokenService.GenerateToken(User);
            return returnDTO;
        }

        private async Task RevertUserDetailInsert(UserDetail UserDetail)
        {
            await _UserDetailRepo.Delete(UserDetail.UserId);
        }

        private async Task RevertUserInsert(User User)
        {

            await _UserRepo.Delete(User.UserId);
        }

        private UserDetail MapUserUserDetailDTOToUserDetail(UserDTO UserDTO)
        {
            UserDetail UserDetail = new UserDetail();
            UserDetail.UserId = UserDTO.UserId;
            UserDetail.Status = "Disabled";
            HMACSHA512 hMACSHA = new HMACSHA512();
            UserDetail.PasswordHashKey = hMACSHA.Key;
            UserDetail.Password = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(UserDTO.Password));
            return UserDetail;
        }

        public async Task<IEnumerable<MatchDetailsDTO>> GetMyMatchs(LoginReturnDTO loginReturnDTO)
        {
            var user = await _context.Users.FindAsync(loginReturnDTO.UserID);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var matchDetails = await _context.Matchs
                .Where(m => (m.UserID1 == loginReturnDTO.UserID || m.UserID2 == loginReturnDTO.UserID) && m.MatchStatus == "Active")
                .Select(m => new
                {
                    MatchID = m.MatchID,
                    MatchedUserID = m.UserID1 == loginReturnDTO.UserID ? m.UserID2 : m.UserID1
                })
                .Join(_context.Users,
                      m => m.MatchedUserID,
                      u => u.UserId,
                      (m, u) => new { MatchID = m.MatchID, MatchedUser = u })
                .Join(_context.Profiles,
                      u => u.MatchedUser.UserId,
                      p => p.UserID,
                      (u, p) => new MatchDetailsDTO
                      {
                          MatchID = u.MatchID,
                          MatchedUserID = u.MatchedUser.UserId,
                          MatchedFirstName = u.MatchedUser.FirstName,
                          MatchedLastName = u.MatchedUser.LastName,
                          MatchedGender = p.Gender,
                          MatchedMaritalStatus = p.MaritalStatus,
                          MatchedHeight = p.Height,
                          MatchedEducation = p.Education,
                          MatchedIncome = p.Income,
                          MatchedReligion = p.Religion,
                          MatchedCaste = p.Caste,
                          MatchedMotherTongue = p.MotherTongue,
                          MatchedProfilePicture = u.MatchedUser.ProfilePicture
                      })
                .ToListAsync();

            return matchDetails;
        }
        public async Task<IEnumerable<MatchReturnDTO>> FindMyMatch(MatchDTO matchDTO)
        {
            var user = await _context.Users.FindAsync(matchDTO.UserId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var matches = await _context.Users
                .Join(_context.Profiles, u => u.UserId, p => p.UserID, (u, p) => new { User = u, Profile = p })
                .Where(up => up.User.UserId != matchDTO.UserId &&
                             up.Profile.Gender != matchDTO.Looking_for &&
                             (string.IsNullOrEmpty(matchDTO.Religion) || up.Profile.Religion == matchDTO.Religion) &&
                             (string.IsNullOrEmpty(matchDTO.MotherTongue) || up.Profile.MotherTongue == matchDTO.MotherTongue) &&
                             (string.IsNullOrEmpty(matchDTO.MaritalStatus) || up.Profile.MaritalStatus == matchDTO.MaritalStatus))
                .Select(up => new MatchReturnDTO
                {
                    UserID = up.User.UserId,
                    FirstName = up.User.FirstName,
                    LastName = up.User.LastName,
                    Gender = up.Profile.Gender,
                    DateOfBirth = up.User.DateOfBirth,
                    ProfilePicture = up.User.ProfilePicture,
                    MaritalStatus = up.Profile.MaritalStatus,
                    Height = up.Profile.Height,
                    Education = up.Profile.Education,
                    Income = up.Profile.Income,
                    Religion = up.Profile.Religion,
                    Caste = up.Profile.Caste,
                    MotherTongue = up.Profile.MotherTongue,
                    Interests = up.Profile.Interests,
                    PartnerExpectations = up.Profile.PartnerExpectations
                })
                .ToListAsync();

            return matches;
        }

        public async Task<Profile> MakeMyProfile(ProfileDTO profileDto)
        {
            if (profileDto == null)
            {
                throw new ArgumentNullException(nameof(profileDto));
            }
            try
            {
                var profile = await _ProfileRepo.Get(profileDto.UserID);
                if (profile == null)
                {
                    profile = new Profile
                    {
                        UserID = profileDto.UserID,
                        MaritalStatus = profileDto.MaritalStatus,
                        Gender = profileDto.Gender,
                        Height = profileDto.Height,
                        Education = profileDto.Education,
                        Income = profileDto.Income,
                        Religion = profileDto.Religion,
                        Caste = profileDto.Caste,
                        MotherTongue = profileDto.MotherTongue,
                        Interests = profileDto.Interests,
                        PartnerExpectations = profileDto.PartnerExpectations
                    };
                    await _ProfileRepo.Add(profile);
                }

                return profile;
            }
            catch
            {
                throw new Exception("Profile not added");
            }

            
        }


      
    }
}
