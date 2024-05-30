using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

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
        public async Task<Match> AddTheMatch(int userid, MatchUpdateDTO matchUpdateDTO)
        {

            if (userid <= 0 || matchUpdateDTO.UserID2 <= 0)
            {
                throw new ArgumentException("User IDs must be positive integers.");
            }

            var match = await _context.Matchs
            .FirstOrDefaultAsync(m =>
                    (m.UserID1 == userid && m.UserID2 == matchUpdateDTO.UserID2));

            if (match != null)
            {
                throw new Exception("Match Already Exist.");
            }
            if (matchUpdateDTO == null)
            {
                throw new ArgumentNullException("Invalid Details");
            }
            Match match1 = new Match
            {
                UserID1 = userid,
                UserID2 = matchUpdateDTO.UserID2,
                MatchStatus = matchUpdateDTO.MatchStatus,
                MatchDate = DateTime.Now
            };
          
            var newmatch =  await _matchRepo.Add(match1);
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
                    (m.UserID1 == user1 && m.UserID2 == user2));

            if (match == null)
            {
                throw new Exception("Match not found.");
            }

            return match;
        }

        public async Task<Match> RemoveTheMatch(int userId1,int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Match ID must be a positive integer.");
            }

            var matchToRemove = _context.Matchs
               .FirstOrDefault(m => m.UserID2 == id && m.UserID1 == userId1);

            if (matchToRemove == null)
            {
                throw new ArgumentException("Match not found or unauthorized to delete.");
            }
            await _matchRepo.Delete(matchToRemove.MatchID);
            return matchToRemove;
        }


        public async Task<Match> UpdateMatchStatus(int userid, MatchUpdateDTO matchUpdateDTO)
        {
            //var data = await _matchRepo.Get(match.MatchID);
            //if (data == null)
            //{
            //    throw new Exception("Match not found.");
            //}
            var matchToUpdate = _context.Matchs
             .FirstOrDefault(m => m.UserID2 == matchUpdateDTO.UserID2 && m.UserID1 == userid);
            if (matchToUpdate == null)
            {
                throw new Exception("Match not found.");
            }
            matchToUpdate.MatchStatus = matchUpdateDTO.MatchStatus;
            await _matchRepo.Update(matchToUpdate);
            var data = await _matchRepo.Get(matchToUpdate.MatchID);
            return data;
        }
    }
}
