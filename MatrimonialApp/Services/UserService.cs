﻿using MatrimonialApp.Interfaces;
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

        public UserService(MatrimonialContext context, IRepository<int, UserDetail> UserDetailRepo, IRepository<int, User> UserRepo, ITokenService tokenService, IRepository<int, Profile> ProfileRepo)
        {
            _context = context;
            _UserDetailRepo = UserDetailRepo;
            _UserRepo = UserRepo;
            _tokenService = tokenService;
            _ProfileRepo = ProfileRepo;
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

        public async Task<IEnumerable<MatchDetailsDTO>> GetMyMatchs(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            //Console.WriteLine(id);
            var matchDetails = await _context.Matchs
                 .Where(m => m.UserID1 == userId || m.UserID2 == userId)
                 .Select(m => new
                 {
                     MatchID = m.MatchID,
                     MatchedUserID = m.UserID1 == userId ? m.UserID2 : m.UserID1,
                     MatchStatus = m.MatchStatus
                 })
                 .Join(_context.Users,
                     m => m.MatchedUserID,
                     u => u.UserId,
                     (m, u) => new { Match = m, MatchedUser = u })
                 .Join(_context.Profiles,
                     u => u.MatchedUser.UserId,
                     p => p.UserID,
                     (u, p) => new MatchDetailsDTO
                     {
                         MatchID = u.Match.MatchID,
                         MatchedUserID = u.MatchedUser.UserId,
                         MatchStatus = u.Match.MatchStatus,
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
        public async Task<IEnumerable<MatchReturnDTO>> FindMyMatch(int UserId, MatchDTO matchDTO)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var matches = await _context.Users
              .Join(_context.Profiles, u => u.UserId, p => p.UserID, (u, p) => new { User = u, Profile = p })
              .Where(up => up.User.UserId != UserId &&
                           up.Profile.Gender == matchDTO.Looking_for ||
                           (string.IsNullOrEmpty(matchDTO.Religion) || up.Profile.Religion == matchDTO.Religion) ||
                           (string.IsNullOrEmpty(matchDTO.MotherTongue) || up.Profile.MotherTongue == matchDTO.MotherTongue) ||
                           (matchDTO.MaritalStatus == null || up.Profile.MaritalStatus == matchDTO.MaritalStatus))
              .ToListAsync();

            var result = matches
                .Select(up => new
                {
                    up.User.UserId,
                    up.User.FirstName,
                    up.User.LastName,
                    up.Profile.Gender,
                    up.User.DateOfBirth,
                    up.User.ProfilePicture,
                    up.Profile.MaritalStatus,
                    up.Profile.Height,
                    up.Profile.Education,
                    up.Profile.Income,
                    up.Profile.Religion,
                    up.Profile.Caste,
                    up.Profile.MotherTongue,
                    up.Profile.Interests,
                    up.Profile.PartnerExpectations,
                    MatchScore = CalculateMatchScore(up.Profile, matchDTO)
                }).Where(up => up.MatchScore >= 50)
                .OrderByDescending(up => up.MatchScore) // Order by MatchScore descending
                .ToList();

            return result.Select(up => new MatchReturnDTO
            {
                UserID = up.UserId,
                FirstName = up.FirstName,
                LastName = up.LastName,
                Gender = up.Gender,
                DateOfBirth = up.DateOfBirth,
                ProfilePicture = up.ProfilePicture,
                MaritalStatus = up.MaritalStatus,
                Height = up.Height,
                Education = up.Education,
                Income = up.Income,
                Religion = up.Religion,
                Caste = up.Caste,
                MotherTongue = up.MotherTongue,
                Interests = up.Interests,
                PartnerExpectations = up.PartnerExpectations,
                MatchScore = up.MatchScore
            }).ToList();
        }
        private int CalculateMatchScore(Profile profile, MatchDTO matchDTO)
        {
            int score = 0;

            if (!string.IsNullOrEmpty(matchDTO.Religion) && profile.Religion == matchDTO.Religion)
            {
                score= score + 25;
            }

            if (!string.IsNullOrEmpty(matchDTO.MotherTongue) && profile.MotherTongue == matchDTO.MotherTongue)
            {
                score = score + 25;
            }

            if (matchDTO.MaritalStatus != null && profile.MaritalStatus == matchDTO.MaritalStatus)
            {
                score = score + 25;
            }

            // Add more criteria as needed...

            return score;
        }



    }
}