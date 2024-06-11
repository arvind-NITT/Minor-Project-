using MainRequestTrackerAPI.Models.DTOs;
using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using MatrimonialApp.Repositories;
using MatrimonialApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonialUnitTest
{
    public class ProfileServiceTest
    {
         MatrimonialContext matrimonialContext;
        private IRepository<int, Profile> _profileRepo;
        private IProfileService _ProfileService;
        [SetUp]
        public void Setup()
        {

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            matrimonialContext = new MatrimonialContext(optionsBuilder.Options);
            _profileRepo = new ProfileRepository(matrimonialContext);
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing"},
                {"Jwt:Issuer", "*"},
                {"Jwt:Audience", "*"}
            };


            IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
            _ProfileService = new ProfileService(matrimonialContext, _profileRepo);
        }
        [Test]
        public async Task CreateProfileTest()
        {
            var newProfile = new ProfileDTO
            {
               
                Gender = "Female",
                MaritalStatus =0,
                Height = 165,
                Education = "Bachelor's",
                Income = 123456789,
                Religion = "Hindu",
                Caste = "General",
                MotherTongue = "Hindi",
                Interests = "Reading, Traveling",
                PartnerExpectations = "Well-educated, Respectful"
            };
            var userid = 108;
            var result = await _ProfileService.AddMyProfile(userid, newProfile);
            Assert.IsNotNull(result);
            //Assert.AreEqual(userid, result.UserId);
        }
        [Test]
        public async Task GetMyProfileTest()
        {
            var userid = 108;
            var ans = await _ProfileService.GetMyProfile(userid);
            Assert.IsNotNull(ans);
            //Assert.AreEqual(profile.UserId, result.UserId);
        }

        [Test]
        public async Task DeleteMyProfileTest()
        {
            var newProfile = new ProfileDTO
            {

                Gender = "Female",
                MaritalStatus = 0,
                Height = 165,
                Education = "Bachelor's",
                Income = 123456789,
                Religion = "Hindu",
                Caste = "General",
                MotherTongue = "Hindi",
                Interests = "Reading, Traveling",
                PartnerExpectations = "Well-educated, Respectful"
            };
            var userid = 109;
            var result = await _ProfileService.AddMyProfile(userid, newProfile);

            var ans = await _ProfileService.DeleteMyProfile(userid);
            Assert.IsNotNull(ans);
            Assert.AreEqual(userid, ans.UserID);
        }
        [Test]
        public async Task UpdateMyProfileTest()
        {
          
            int userid = 108;

            var updatedProfileDTO = new ProfileDTO
            {
                Gender = "Female",
                MaritalStatus = MaritalStatus.Married,
                Height = 170,
                Education = "Master's",
                Income = 70000,
                Religion = "Hindu",
                Caste = "General",
                MotherTongue = "Hindi",
                Interests = "Reading, Traveling, Cooking",
                PartnerExpectations = "Well-educated, Respectful"
            };

            var ans = await _ProfileService.UpdateMyProfile(userid, updatedProfileDTO);
            //Assert.IsNotNull(result);
            Assert.AreEqual(updatedProfileDTO.MaritalStatus, ans.MaritalStatus);
            Assert.AreEqual(updatedProfileDTO.Education, ans.Education);
        }
    }
}
