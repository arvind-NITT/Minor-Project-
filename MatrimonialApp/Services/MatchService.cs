using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.X86;

namespace MatrimonialApp.Services
{
    public class MatchService : IMatchService
    {
        private readonly MatrimonialContext _context;
        private readonly IRepository<int, Match> _matchRepo;
        private readonly IRepository<int, User> _UserRepo;
        private readonly IRepository<int, Subscription> _SubscriptionRepo;
        public MatchService(MatrimonialContext context, IRepository<int, Match> matchRepo, IRepository<int, Subscription> subscriptionRepo, IRepository<int, User> userRepo)
        {
            _context = context;
            _matchRepo = matchRepo;
            _SubscriptionRepo = subscriptionRepo;
            _UserRepo = userRepo;
        }
        public async Task<Match> AddTheMatch(int userid, MatchInsertDTO matchInsertDTO)
        {
            if (matchInsertDTO == null)
            {
                throw new ArgumentNullException(nameof(matchInsertDTO), "MatchDto Should not be null.");
            }

            if (userid <= 0 || matchInsertDTO.UserID2 <= 0)
            {
                throw new ArgumentException("User IDs must be positive integers.");
            }
            var user2 = await _UserRepo.Get(matchInsertDTO.UserID2);
            if (user2 == null)
            {
                throw new Exception("Invalid UserId.");
            }
            var match = await _context.Matchs
            .FirstOrDefaultAsync(m =>
                    (m.UserID1 == userid && m.UserID2 == matchInsertDTO.UserID2));
            if (match != null)
            {
                throw new Exception("Match Already Exist.");
            }
            //var matchcount = await _context.Matchs
            //.query(m =>
            //        (m.UserID1 == userid ));
            var MatchCount =await GetCountOfMatchesAddedByUser(userid);
            var subscription = await _context.Subscriptions.SingleOrDefaultAsync(u => u.UserId == userid);
            if (subscription == null)
            {
                throw new Exception("subscription Invalid.");
            }
            if (subscription.Type == SubscriptionType.Premium  && subscription.EndDate < DateTime.Now)
            {
                subscription.Type = SubscriptionType.Basic;
                await _SubscriptionRepo.Update(subscription);
            }
            if (subscription.Type==SubscriptionType.Basic && MatchCount >= 5)
            {
                throw new Exception("To add more Match , upgrade your subscription");
            }

            Match match1 = new Match
            {
                UserID1 = userid,
                UserID2 = matchInsertDTO.UserID2,
                MatchStatus = MatchStatus.Pending,
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
            var MatchCount = await GetCountOfMatchesAddedByUser(userid);
            var subscription = await _context.Subscriptions.SingleOrDefaultAsync(u => u.UserId == userid);
            if (subscription.Type == SubscriptionType.Premium && subscription.EndDate < DateTime.Now)
            {
                subscription.Type = SubscriptionType.Basic;
                await _SubscriptionRepo.Update(subscription);
            }
            if (subscription.Type == SubscriptionType.Basic && MatchCount >= 5)
            {
                throw new Exception("To Update Match , upgrade your subscription");
            }
            var matchToUpdate = await _context.Matchs.SingleOrDefaultAsync(m => (m.UserID1 == userid && m.UserID2 == matchUpdateDTO.UserID2) || (m.UserID1 == matchUpdateDTO.UserID2 && m.UserID2 == userid));
            Console.WriteLine(matchUpdateDTO.UserID2 + userid);
            if (matchToUpdate == null)
            {
                Console.WriteLine("Reaching");
                throw new Exception("Match not found.");
            }

            matchToUpdate.MatchStatus = matchUpdateDTO.MatchStatus;
            await _matchRepo.Update(matchToUpdate);
            var data = await _matchRepo.Get(matchToUpdate.MatchID);
            return data;
        }
        public async Task<int> GetCountOfMatchesAddedByUser(int userId)
        {
            return await _context.Matchs.CountAsync(m => m.UserID1 == userId);
        }
    }
}
