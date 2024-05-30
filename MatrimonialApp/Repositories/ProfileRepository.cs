using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrimonialApp.Repositories
{
    public class ProfileRepository : IRepository<int, Profile>
    {
        private MatrimonialContext _context;

        public ProfileRepository(MatrimonialContext context)
        {
            _context = context;
        }
        public async Task<Profile> Add(Profile item)
        {
            _context.Add(item);
            Console.WriteLine(item.ProfileID + "dfjbndm");
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Profile> Delete(int key)
        {
            var user = await Get(key);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            throw new Exception("No Profile with the given ID");
        }

        //public async Task<Profile> GetProfilebyUserId(int key)
        //{
        //    return (await _context.Profiles.SingleOrDefaultAsync(u => u.UserID == key)) ?? throw new Exception("No Profile with the given ID");
        //}
        public async Task<Profile> Get(int key)
        {
            return (await _context.Profiles.SingleOrDefaultAsync(u => u.UserID == key)) ?? throw new Exception("No Profile with the given ID");
        }

        public async Task<IEnumerable<Profile>> Get()
        {
            return (await _context.Profiles.ToListAsync());
        }

        public IQueryable<Profile> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<Profile> Update(Profile item)
        {
            var user = await Get(item.ProfileID);
            if (user != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return user;
            }
            throw new Exception("No user with the given ID");
        }
    }
}
