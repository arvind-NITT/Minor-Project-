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
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
              .HasIndex(u => u.Email)
              .IsUnique();
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(e => e.ProfileID);
                entity.Property(e => e.Gender).IsRequired();
                entity.Property(e => e.Height).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Income).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<UserDetail>(e => e.UserId);
            });
            modelBuilder.Entity<Subscription>()
              .Property(s => s.Type)
              .HasDefaultValue(SubscriptionType.Basic); // Set your default value here
            //modelBuilder.Entity<User>().HasData(
            //    new User() { UserId = 101, FirstName = "Arvind", LastName = "Mali", Email = "Amali@gmail.com",  DateOfBirth = new DateTime(2000, 2, 12), PhoneNumber = "9876543321", Address="Chandwasa", ProfilePicture = "",Role=Role.Admin },
            //    new User() { UserId = 102, FirstName = "Arvind1", LastName = "Mali1", Email = "Amali1@gmail.com",  DateOfBirth = new DateTime(2000, 2, 12), PhoneNumber = "9876543321", Address="Mandsaur", ProfilePicture = "",Role = Role.Admin }
            //    );
            //modelBuilder.Entity<Match>().HasData(
            //    new Match() { MatchID = 101, UserID1 = 101, UserID2 = 105, MatchStatus = MatchStatus.Pending, MatchDate = new DateTime(2023, 2, 12) }
            //    ); 
            //modelBuilder.Entity<Profile>().HasData(
            //    new Profile() { ProfileID = 101, UserID = 101, MaritalStatus = MaritalStatus.Married, Gender="Male", Height = 102, Education = "MCA", Income=950000, Religion="Hindu", Caste="Mali", MotherTongue="Hindi", Interests="Sports", PartnerExpectations="Nothing" }
            //    ); 
            //modelBuilder.Entity<Subscription>().HasData(
            //    new Subscription() { SubscriptionId = 101, UserId = 101, TransactionId=101, Type = SubscriptionType.Basic, StartDate = new DateTime(2023, 2, 12), EndDate = new DateTime(2023, 5, 12)  }
            //    );
            //modelBuilder.Entity<Transaction>().HasData(
            //    new Transaction() { TransactionId=101,UserId=101,Amount=1000,TransactionDate=DateTime.Now,TransactionType="Initial"});
        }

    }
}
