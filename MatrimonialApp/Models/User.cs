using System.Text.RegularExpressions;

namespace MatrimonialApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
      
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }

        //public virtual ICollection<Match> Matches1 { get; set; }
        //public virtual ICollection<Match> Matches2 { get; set; }
      
    }

}
