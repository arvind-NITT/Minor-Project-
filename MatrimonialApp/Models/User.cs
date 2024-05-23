using System.Text.RegularExpressions;

namespace MatrimonialApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; } // Consider using an enum
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }

        public virtual ICollection<Match> Matches1 { get; set; }
        public virtual ICollection<Match> Matches2 { get; set; }
      
    }

}
