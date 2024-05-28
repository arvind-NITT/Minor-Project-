namespace MatrimonialApp.Models.DTOs
{
    public class MatchReturnDTO
    {

        // Basic User Information
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }

        // Profile Information
        public string MaritalStatus { get; set; } // Consider using an enum
        public decimal Height { get; set; }
        public string Education { get; set; }
        public decimal Income { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
        public string MotherTongue { get; set; }
        public string Interests { get; set; }
        public string PartnerExpectations { get; set; }


    }
}
