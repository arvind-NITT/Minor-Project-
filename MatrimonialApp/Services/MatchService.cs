using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MatrimonialApp.Services
{
    public class MatchService : IMatchService
    {
        private readonly MatrimonialContext _context;
        private readonly IRepository<int, Match> _matchRepo;
        public MatchService(MatrimonialContext context, IRepository<int, Match> matchRepo)
        {
            _context = context;
            _matchRepo = matchRepo;
        }
        public async Task<Match> AddTheMatch(Match match)
        {

            if (match == null)
            {
                throw new ArgumentNullException("match not found");
            }
            var newmatch =  await _matchRepo.Add(match);
            return newmatch;
            
        }

        public async Task<IEnumerable<Match>> GetAllTheMatch()
        {
            var matches = await _matchRepo.Get();
            return matches;
        }

        public async Task<Match> GetTheMatchbyuserId(int user1, int user2)
        {
            if (user1 <= 0 || user2 <= 0)
            {
                throw new ArgumentException("User IDs must be positive integers.");
            }

            var match = await _context.Matchs
                .FirstOrDefaultAsync(m =>
                    (m.UserID1 == user1 && m.UserID2 == user2) ||
                    (m.UserID1 == user2 && m.UserID2 == user1));

            if (match == null)
            {
                throw new Exception("Match not found.");
            }

            return match;
        }

        public async Task<Match> RemoveTheMatch(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Match ID must be a positive integer.");
            }
            var match = await _matchRepo.Get(id);
            if (match == null)
            {
                throw new Exception("Match not found.");
            }
            await _matchRepo.Delete(id);
            return match;
        }

        public async Task<Match> UpdateMatchStatus(Match match)
        {
            var data = await _matchRepo.Get(match.MatchID);
            if (data == null)
            {
                throw new Exception("Match not found.");
            }
            await _matchRepo.Update(match);
            data = await _matchRepo.Get(match.MatchID);
            return data;
        }
    }
}
