using MatrimonialApp.Models;

namespace MatrimonialApp.Interfaces
{
    public interface IMatchService
    {
        public Task<Match>  AddTheMatch(Match match);
        public Task<Match> RemoveTheMatch(int id);
        public Task<Match> UpdateMatchStatus(Match match);
        public Task<Match> GetTheMatchbyuserId(int user1,int user2);
        public Task<IEnumerable<Match>> GetAllTheMatch();
    }
}
