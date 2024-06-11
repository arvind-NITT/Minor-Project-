namespace MatrimonialApp.Models.DTOs
{
    public class LoginReturnDTO
    {
        public int UserID { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }
    }
}
