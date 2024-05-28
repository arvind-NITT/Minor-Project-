namespace MatrimonialApp.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        public int UserID1 { get; set; }
        public int UserID2 { get; set; }
        public string MatchStatus { get; set; } // Consider using an enum
        public DateTime MatchDate { get; set; }

        //public virtual User User1 { get; set; }
        //public virtual User User2 { get; set; }
    }

}
