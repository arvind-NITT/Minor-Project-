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
    public class MatchServiceTest
    {
        MatrimonialContext matrimonialContext;
        private  IRepository<int, Match> _matchRepo;
        private  IRepository<int, Subscription> _SubscriptionRepo;
        private IRepository<int, User> _UserRepo;
        private IMatchService matchservice;
        [SetUp]
        public void Setup()
        {

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            matrimonialContext = new MatrimonialContext(optionsBuilder.Options);
            _matchRepo = new MatchRepository(matrimonialContext);
            _SubscriptionRepo = new SubscriptionRepository(matrimonialContext);
            _UserRepo = new UserRepository(matrimonialContext);
              var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing"},
                {"Jwt:Issuer", "*"},
                {"Jwt:Audience", "*"}
            };


            IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
                .Build();

            matchservice = new MatchService(matrimonialContext, _matchRepo, _SubscriptionRepo,_UserRepo);
        }
        [Test]
        public void AddTheMatchTest()
        {
            int userid1=1;
            var matchInsertDTO = new MatchInsertDTO
            {
                UserID2 = 2,
            };
            var result = matchservice.AddTheMatch(userid1, matchInsertDTO);
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetAllTheMatchTest()
        {
            //int userid1=1;
            //var matchUpdateDTO = new MatchUpdateDTO
            //{
            //    UserID2 = 2,
            //    MatchStatus = MatchStatus.Pending
            //};
            var result = matchservice.GetAllTheMatch();
            //Console.WriteLine(result.Result);
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetTheMatchbyuserIdTest()
        {
            int userid1=1;
            //var matchUpdateDTO = new MatchUpdateDTO
            //{
            int UserID2 = 2;
            //    MatchStatus = MatchStatus.Pending
            //};
            var result = matchservice.GetTheMatchbyuserId(userid1,UserID2);
            //Console.WriteLine(result.Result);
            Assert.IsNotNull(result);
        }
          [Test]
        public void UpdateMatchStatusTest()
        {
            int userid1=1;
            var matchUpdateDTO = new MatchUpdateDTO
            {
                UserID2 = 2,
                MatchStatus = MatchStatus.Approved
            };
            var result = matchservice.UpdateMatchStatus(userid1,matchUpdateDTO);
            //Console.WriteLine(result.Result);
            Assert.IsNotNull(result);
        }
          [Test]
        public void RemoveTheMatchTest()
        {
            int userid1=1;
            //var matchUpdateDTO = new MatchUpdateDTO
            //{
            int UserID2 = 2;
            //    MatchStatus = MatchStatus.Approved
            //};
            var result = matchservice.RemoveTheMatch(userid1,UserID2);
            //Console.WriteLine(result.Result);
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task AddTheMatch_ShouldThrowException_WhenUserIDsAreInvalid()
        {
            // Arrange
            int userId = -1;
            var matchInsertDTO = new MatchInsertDTO { UserID2 = -2 };

            // Act & Assert
             Assert.ThrowsAsync<ArgumentException>(() => matchservice.AddTheMatch(userId, matchInsertDTO));
        }
        [Test]
        public async Task AddTheMatch_ShouldThrowException_WhenMatchInsertDTOIsNull()
        {
            // Arrange
            int userId = 1;
            MatchInsertDTO matchInsertDTO = null;

            // Act & Assert
             Assert.ThrowsAsync<ArgumentNullException>(() => matchservice.AddTheMatch(userId, matchInsertDTO));
        }
       // [Test]
        //public async Task AddTheMatch_ShouldThrowException_WhenSubscriptionIsBasicAndMatchCountExceedsLimit()
        //{
        //    // Arrange
        //    UserDTO newuser = new UserDTO
        //    {
        //        FirstName = "sunita",
        //        LastName = "Mali",
        //        DateOfBirth = new DateTime(2001, 01, 05),
        //        Email = "s@gmail.com",
        //        PhoneNumber = "1234567890",
        //        Address = "emerald 65",
        //        ProfilePicture = "string",
        //        Password = "123456"
        //    };
        //    UserDTO newuser1 = new UserDTO
        //    {
        //        FirstName = "sunita",
        //        LastName = "Mali",
        //        DateOfBirth = new DateTime(2001, 01, 05),
        //        Email = "s@gmail.com",
        //        PhoneNumber = "1234567890",
        //        Address = "emerald 65",
        //        ProfilePicture = "string",
        //        Password = "123456"
        //    };
        //       UserDTO newuser2 = new UserDTO
        //    {
        //        FirstName = "sunita",
        //        LastName = "Mali",
        //        DateOfBirth = new DateTime(2001, 01, 05),
        //        Email = "s@gmail.com",
        //        PhoneNumber = "1234567890",
        //        Address = "emerald 65",
        //        ProfilePicture = "string",
        //        Password = "123456"
        //    };
        //       UserDTO newuser3 = new UserDTO
        //    {
        //        FirstName = "sunita",
        //        LastName = "Mali",
        //        DateOfBirth = new DateTime(2001, 01, 05),
        //        Email = "s@gmail.com",
        //        PhoneNumber = "1234567890",
        //        Address = "emerald 65",
        //        ProfilePicture = "string",
        //        Password = "123456"
        //    };
        //       UserDTO newuser4 = new UserDTO
        //    {
        //        FirstName = "sunita",
        //        LastName = "Mali",
        //        DateOfBirth = new DateTime(2001, 01, 05),
        //        Email = "s@gmail.com",
        //        PhoneNumber = "1234567890",
        //        Address = "emerald 65",
        //        ProfilePicture = "string",
        //        Password = "123456"
        //    };
        //          UserDTO newuser5 = new UserDTO
        //    {
        //        FirstName = "sunita",
        //        LastName = "Mali",
        //        DateOfBirth = new DateTime(2001, 01, 05),
        //        Email = "s@gmail.com",
        //        PhoneNumber = "1234567890",
        //        Address = "emerald 65",
        //        ProfilePicture = "string",
        //        Password = "123456"
        //    };

        //    var u1 = await _UserRepo.Add(newuser); 
        //    var u2 = await _UserRepo.Add(newuser1);
        //    var u3 = await _UserRepo.Add(newuser2);
        //    var u4 = await _UserRepo.Add(newuser3);
        //    var u5 = await _UserRepo.Add(newuser4);
        //    var u6 = await _UserRepo.Add(newuser5);
        //    int userId = 1;
        //    var matchInsertDTO1 = new MatchInsertDTO { UserID2 = u1.UserId };
        //     var matchInsertDTO2 = new MatchInsertDTO { UserID2 = u2.UserId };
        //     var matchInsertDTO3 = new MatchInsertDTO { UserID2 = u3.UserId };
        //     var matchInsertDTO4 = new MatchInsertDTO { UserID2 = u4.UserId };
        //     var matchInsertDTO5 = new MatchInsertDTO { UserID2 = u5.UserId };
        //    var matchInsertDTO = new MatchInsertDTO { UserID2 = u6.UserId };
        //    await matchservice.AddTheMatch(userId, matchInsertDTO1);
        //    await matchservice.AddTheMatch(userId, matchInsertDTO2);
        //    await matchservice.AddTheMatch(userId, matchInsertDTO3);
        //    await matchservice.AddTheMatch(userId, matchInsertDTO4);
        //    await matchservice.AddTheMatch(userId, matchInsertDTO5);

        //    // Act & Assert
        //    Assert.ThrowsAsync<Exception>(() => matchservice.AddTheMatch(userId, matchInsertDTO));
        //}
        
    }
}
