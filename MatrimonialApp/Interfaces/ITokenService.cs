using MatrimonialApp.Models;

namespace MatrimonialApp.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
