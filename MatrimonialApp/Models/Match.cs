using System.ComponentModel.DataAnnotations;

namespace MatrimonialApp.Models
{
    public enum MatchStatus
    {
        Pending,
        Approved,
        Rejected
    }
    public class Match
    {
        public int MatchID { get; set; }
        [Required(ErrorMessage = "User ID 1 is required.")]
        public int UserID1 { get; set; }

        [Required(ErrorMessage = "User ID 2 is required.")]
        public int UserID2 { get; set; }

        [Required(ErrorMessage = "Match date is required.")]
        public DateTime MatchDate { get; set; }

        [EnumDataType(typeof(MatchStatus), ErrorMessage = "Invalid MatchStatus value.")]
        public MatchStatus MatchStatus { get; set; }
        public bool IsValidMatchStatus()
        {
            return Enum.IsDefined(typeof(MatchStatus), this.MatchStatus);
        }
        public Match()
        {
            // Ensure MatchDate defaults to current date/time
            MatchDate = DateTime.Now;
        }
        //public virtual User User1 { get; set; }
        //public virtual User User2 { get; set; }
    }
   

}
