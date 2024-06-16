using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
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
    public class AdminServiceTests
    {
        private  MatrimonialContext matrimonialContext;
        private  IAdminService _adminService;
        public AdminServiceTests()
        {}
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            matrimonialContext = new MatrimonialContext(optionsBuilder.Options);

            _adminService = new AdminService(matrimonialContext);
        }
        [Test]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
               new User() { UserId = 101, FirstName = "Arvind", LastName = "Mali", Email = "Amali@gmail.com",  DateOfBirth = new DateTime(2000, 2, 12), PhoneNumber = "9876543321", Address="Chandwasa", ProfilePicture = "",Role=Role.Admin },
                new User() { UserId = 102, FirstName = "Arvind1", LastName = "Mali1", Email = "Amali1@gmail.com",  DateOfBirth = new DateTime(2000, 2, 12), PhoneNumber = "9876543321", Address="Mandsaur", ProfilePicture = "",Role = Role.Admin }
                };

            await matrimonialContext.Users.AddRangeAsync(users);
            await matrimonialContext.SaveChangesAsync();

            // Act
            var result = await _adminService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(3, result.Count());
        }
        [Test]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            //var user = new User { UserId = 1, FirstName = "John", LastName = "Doe" };
            //await matrimonialContext.Users.AddAsync(user);
            //await matrimonialContext.SaveChangesAsync();

            // Act
            var result = await _adminService.GetUserByIdAsync(101);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Arvind", result.FirstName);
            Assert.AreEqual("Mali", result.LastName);
        }
        [Test]
        public async Task DeleteUserAsync_ShouldRemoveUser_WhenUserExists()
        {
            // Arrange
            var user = new User() { UserId = 103, FirstName = "Arvind2", LastName = "Mali1", Email = "Amali1@gmail.com", DateOfBirth = new DateTime(2000, 2, 12), PhoneNumber = "9876543321", Address = "Mandsaur", ProfilePicture = "", Role = Role.Admin };
            await matrimonialContext.Users.AddAsync(user);
            await matrimonialContext.SaveChangesAsync();

            // Act
            await _adminService.DeleteUserAsync(103);
            var result = await matrimonialContext.Users.FindAsync(103);

            // Assert
            Assert.Null(result);
        }
        [Test]
        public async Task GetAllProfilesAsync_ShouldReturnAllProfiles()
        {
            // Arrange
            var profiles = new List<Profile>
            {
                new Profile() { ProfileID = 101, UserID = 101, MaritalStatus = MaritalStatus.Married, Gender="Male", Height = 102, Education = "MCA", Income=950000, Religion="Hindu", Caste="Mali", MotherTongue="Hindi", Interests="Sports", PartnerExpectations="Nothing" },
                new Profile() { ProfileID = 102, UserID = 102, MaritalStatus = MaritalStatus.Married, Gender="Male", Height = 102, Education = "MCA", Income=950000, Religion="Hindu", Caste="Mali", MotherTongue="Hindi", Interests="Sports", PartnerExpectations="Nothing" }
            };

            await matrimonialContext.Profiles.AddRangeAsync(profiles);
            await matrimonialContext.SaveChangesAsync();

            // Act
            var result = await _adminService.GetAllProfilesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetProfileByIdAsync_ShouldReturnProfile_WhenProfileExists()
        {
            // Arrange
            //var profile = new Profile { ProfileID = 1, UserID = 1, Gender = "Male" };
            //await matrimonialContext.Profiles.AddAsync(profile);
            //await matrimonialContext.SaveChangesAsync();

            // Act
            var result = await _adminService.GetProfileByIdAsync(101);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Male", result.Gender);
        }
        [Test]
        public async Task GetProfileByIdAsync_ShouldReturnNull_WhenProfileDoesNotExist()
        {
            // Act
            var result = await _adminService.GetProfileByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task UpdateProfileAsync_ShouldUpdateProfile()
        {
            // Arrange
            var profile = await _adminService.GetProfileByIdAsync(101);
            // Act
            profile.Gender = "Female";
            await _adminService.UpdateProfileAsync(profile);
            var result = await matrimonialContext.Profiles.FindAsync(101);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Female", result.Gender);
        }
        [Test]
        public async Task DeleteProfileAsync_ShouldRemoveProfile_WhenProfileExists()
        {
            // Arrange
            var user = new User() { UserId = 110, FirstName = "Arvind2", LastName = "Mali1", Email = "Amali1@gmail.com", DateOfBirth = new DateTime(2000, 2, 12), PhoneNumber = "9876543321", Address = "Mandsaur", ProfilePicture = "", Role = Role.Admin };
            await matrimonialContext.Users.AddAsync(user);
            await matrimonialContext.SaveChangesAsync();
            var profile = new Profile() { ProfileID = 105, UserID = 110, MaritalStatus = MaritalStatus.Married, Gender = "Male", Height = 102, Education = "MCA", Income = 950000, Religion = "Hindu", Caste = "Mali", MotherTongue = "Hindi", Interests = "Sports", PartnerExpectations = "Nothing" };

            await matrimonialContext.Profiles.AddAsync(profile);
            await matrimonialContext.SaveChangesAsync();

            // Act
            await _adminService.DeleteProfileAsync(105);
            //await _adminService.DeleteUserAsync(103);
            var result = await matrimonialContext.Profiles.FindAsync(105);

            // Assert
            Assert.Null(result);
        }
        [Test]
        public async Task GetAllSubscriptionsAsync_ShouldReturnAllSubscriptions()
        {
            // Arrange
            var subscriptions = new List<Subscription>
            {
                new Subscription() { SubscriptionId = 101, UserId = 101, TransactionId=101, Type = SubscriptionType.Basic, StartDate = new DateTime(2023, 2, 12), EndDate = new DateTime(2023, 5, 12)  },
                new Subscription() { SubscriptionId = 102, UserId = 102, TransactionId=102, Type = SubscriptionType.Basic, StartDate = new DateTime(2023, 2, 12), EndDate = new DateTime(2023, 5, 12)  }
             };

            await matrimonialContext.Subscriptions.AddRangeAsync(subscriptions);
            await matrimonialContext.SaveChangesAsync();

            // Act
            var result = await _adminService.GetAllSubscriptionsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetSubscriptionByIdAsync_ShouldReturnSubscription_WhenSubscriptionExists()
        {
            // Arrange
            //var subscription = new Subscription { SubscriptionID = 1, UserId = 1, Type = SubscriptionType.Basic };
            //await matrimonialContext.Subscriptions.AddAsync(subscription);
            //await matrimonialContext.SaveChangesAsync();

            // Act
            var result = await _adminService.GetSubscriptionByIdAsync(101);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(SubscriptionType.Basic, result.Type);
        }
        [Test]
        public async Task GetSubscriptionByIdAsync_ShouldReturnNull_WhenSubscriptionDoesNotExist()
        {
            // Act
            var result = await _adminService.GetSubscriptionByIdAsync(1);

            // Assert
            Assert.Null(result);
        }
        [Test]
        public async Task UpdateSubscriptionAsync_ShouldUpdateSubscription()
        {
            // Arrange
            var subscription = await _adminService.GetSubscriptionByIdAsync(102);
            // Act
            subscription.Type = SubscriptionType.Premium;
            await _adminService.UpdateSubscriptionAsync(subscription);
            var result = await matrimonialContext.Subscriptions.FindAsync(102);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(SubscriptionType.Premium, result.Type);
        }
        [Test]
        public async Task DeleteSubscriptionAsync_ShouldRemoveSubscription_WhenSubscriptionExists()
        {
            // Arrange
            var subscription = new Subscription() { SubscriptionId = 104, UserId = 103, TransactionId = 101, Type = SubscriptionType.Basic, StartDate = new DateTime(2023, 2, 12), EndDate = new DateTime(2023, 5, 12) };


            await matrimonialContext.Subscriptions.AddAsync(subscription);
            await matrimonialContext.SaveChangesAsync();

            // Act
            await _adminService.DeleteSubscriptionAsync(104);
            var result = await matrimonialContext.Subscriptions.FindAsync(104);

            // Assert
            Assert.Null(result);
        }
    }
}
