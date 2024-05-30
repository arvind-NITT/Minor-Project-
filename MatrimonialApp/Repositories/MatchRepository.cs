using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrimonialApp.Repositories
{
    public class MatchRepository : IRepository<int, Match>
    {
        private MatrimonialContext _context;

        public MatchRepository(MatrimonialContext context)
        {
            _context = context;
        }
        public async Task<Match> Add(Match item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Match> Delete(int key)
        {
            var match = await Get(key);
            if (match != null)
            {
                _context.Remove(match);
                await _context.SaveChangesAsync();
                return match;
            }
            throw new Exception("No user with the given ID");
        }

        public async Task<Match> Get(int key)
        {
            return (await _context.Matchs.SingleOrDefaultAsync(m => m.MatchID == key)) ?? throw new Exception("No Match with the given ID");
        }

        public async Task<IEnumerable<Match>> Get()
        {
            return (await _context.Matchs.ToListAsync());
        }

        public IQueryable<Match> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<Match> Update(Match match)
        {
            try
            {
                var data = await _context.Matchs.FindAsync(match.MatchID); // Use context directly
                if (data == null)
                {
                    throw new Exception("Match not found.");
                }

                // Update properties
                data.MatchStatus = match.MatchStatus;
                data.MatchDate = match.MatchDate;

                _context.Entry(data).State = EntityState.Modified; // Mark entity as modified
                await _context.SaveChangesAsync(); // Save changes to database

                return data;
            }
            catch
            {
            throw new Exception("No user with the given ID");
            }
         

        }
    }
}
