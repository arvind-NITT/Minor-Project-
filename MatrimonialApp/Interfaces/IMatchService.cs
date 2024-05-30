using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;

namespace MatrimonialApp.Interfaces
{
    public interface IMatchService
    {
        public Task<Match>  AddTheMatch(int userid, MatchUpdateDTO matchUpdateDTO);
        public Task<Match> RemoveTheMatch(int userId1, int id);
        public Task<Match> UpdateMatchStatus(int userid,MatchUpdateDTO matchUpdateDTO);
        public Task<Match> GetTheMatchbyuserId(int user1,int user2);
        public Task<IEnumerable<Match>> GetAllTheMatch();
    }
}
