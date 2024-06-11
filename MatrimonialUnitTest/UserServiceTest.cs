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
    public class UserServiceTest
    {
        MatrimonialContext matrimonialContext;
        private IRepository<int, User> _userRepository;
        private IRepository<int, UserDetail> _userDetailRepo;
        private IRepository<int, Profile> _profileRepo;
        private IRepository<int, Subscription> _SubscriptionRepo;
        private IRepository<int, Transaction> _TransactionRepo;
        private ITokenService _tokenService;
        private IUserService _userService;
        public UserServiceTest() { }
        [SetUp]
        public void Setup()
        {

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            matrimonialContext = new MatrimonialContext(optionsBuilder.Options);
            _userRepository = new UserRepository(matrimonialContext);
            _userDetailRepo = new UserDetailRepository(matrimonialContext);
            _profileRepo = new ProfileRepository(matrimonialContext);
            _SubscriptionRepo = new SubscriptionRepository(matrimonialContext);
            _TransactionRepo = new TransactionRepository(matrimonialContext);
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing"},
                {"Jwt:Issuer", "*"},
                {"Jwt:Audience", "*"}
            };


            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
             _tokenService = new TokenService(configuration);
            _userService = new UserService(matrimonialContext, _userDetailRepo, _userRepository, _tokenService, _profileRepo, _SubscriptionRepo, _TransactionRepo);
        }

        [Test]
        public void AddUserTest()
        {
           
            UserDTO newuser = new UserDTO {
                    FirstName = "sunita",
                    LastName = "Mali",
                    DateOfBirth =new  DateTime(2001,01,05),
                    Email = "s@gmail.com",
                    PhoneNumber = "1234567890",
                    Address = "emerald 65",
                    ProfilePicture = "string",
                    Password= "123456"
                };
          
            var result = _userService.Register(newuser);
            Assert.IsNotNull(result);
        }
        [Test]
        public void LoginUserTest()
        {

            UserLoginDTO userLoginDTO = new UserLoginDTO
            {
                UserId = 106,
                Password = "123456"
            };
             var result = _userService.Login(userLoginDTO);
            Assert.IsNotNull(result);
        }
           [Test]
        public void GetMyMatchsTest()
        {
            int UserId = 106;
             var result = _userService.GetMyMatchs(UserId);
            Assert.IsNotNull(result);
        }
           [Test]
        public void FindMyMatchTest()
        {
            int UserId = 106;
            MatchDTO matchDTO = new MatchDTO
            {
                Looking_for = "Male",
                Age = 23,
                Religion = "Muslim",
                MotherTongue = "hindi",
                MaritalStatus = 0
            };
            var result = _userService.FindMyMatch(UserId,matchDTO);
            Assert.IsNotNull(result);
        }

    }
}
