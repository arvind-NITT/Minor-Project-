using MatrimonialApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

namespace MatrimonialApp.Contexts
{
    public class MatrimonialContext: DbContext
    {
        public MatrimonialContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { UserId = 104, FirstName = "Arvind", LastName = "Mali", Email = "Amali@gmail.com",  DateOfBirth = new DateTime(2000, 2, 12), PhoneNumber = "9876543321", Address="", ProfilePicture = "" },
                new User() { UserId = 105, FirstName = "Arvind1", LastName = "Mali1", Email = "Amali1@gmail.com",  DateOfBirth = new DateTime(2000, 2, 12), PhoneNumber = "9876543321", Address="", ProfilePicture = "" }
                );
            modelBuilder.Entity<Match>().HasData(
                new Match() { MatchID = 102, UserID1 = 102, UserID2 = 103, MatchStatus = "Amali@gmail.com", MatchDate = new DateTime(2023, 2, 12) }
                ); 
            modelBuilder.Entity<Profile>().HasData(
                new Profile() { ProfileID = 102, UserID = 102, MaritalStatus = "Married",Gender="Male", Height = 102, Education = "MCA", Income=950000, Religion="Hindu", Caste="Mali", MotherTongue="Hindi", Interests="Sports", PartnerExpectations="Nothing" }
                ); 
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription() { SubscriptionID = 102, UserID = 102, SubscriptionType = "Normal", StartDate = new DateTime(2023, 2, 12), EndDate = new DateTime(2023, 5, 12)  }
                );
        }

    }
}
