namespace MatrimonialApp.Models
{
    public class Profile
    {
        public int ProfileID { get; set; }
        public int UserID { get; set; }
        public string MaritalStatus { get; set; } // Consider using an enum
        public decimal Height { get; set; }
        public string Education { get; set; }
        public decimal Income { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
        public string MotherTongue { get; set; }
        public string Interests { get; set; }
        public string PartnerExpectations { get; set; }

        public virtual User User { get; set; }
    }

}
