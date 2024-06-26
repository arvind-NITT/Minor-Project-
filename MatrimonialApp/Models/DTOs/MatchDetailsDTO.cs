﻿namespace MatrimonialApp.Models.DTOs
{
    //public enum MatchStatus
    //{
    //    Pending,
    //    Approved,
    //    Rejected
    //}
 
    public class MatchDetailsDTO
    {
        public int MatchID { get; set; }
        public int MatchedUserID { get; set; }
        public MatchStatus MatchStatus { get; set; }
        public string MatchedFirstName { get; set; }
        public string MatchedLastName { get; set; }
        public string MatchedGender { get; set; }
        public MaritalStatus MatchedMaritalStatus { get; set; }
        public decimal MatchedHeight { get; set; }
        public string MatchedEducation { get; set; }
        public decimal MatchedIncome { get; set; }
        public string MatchedReligion { get; set; }
        public string MatchedCaste { get; set; }
        public string MatchedMotherTongue { get; set; }
        public string MatchedProfilePicture { get; set; }
        public DateTime MatchDate { get; set; }

    }
}
