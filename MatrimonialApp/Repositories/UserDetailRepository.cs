﻿using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrimonialApp.Repositories
{
    public class UserDetailRepository : IRepository<int, UserDetail>
    {
        private MatrimonialContext _context;

        public UserDetailRepository(MatrimonialContext context)
        {
            _context = context;
        }
        public async Task<UserDetail> Add(UserDetail item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<UserDetail> Delete(int key)
        {
            var UserDetail = await Get(key);
            if (UserDetail != null)
            {
                _context.Remove(UserDetail);
                await _context.SaveChangesAsync();
                return UserDetail;
            }
            throw new Exception("No UserDetail with the given ID");
        }
        //public async Task<UserDetail> GetByEmailAsync(string email)
        //{
        //    return await _context.UserDetails.FirstOrDefaultAsync(u => u.email == email);
        //}
        public async Task<UserDetail> Get(int key)
        {
            return (await _context.UserDetails.SingleOrDefaultAsync(u => u.UserId == key)) ?? throw new Exception("No UserDetail with the given ID");
        }

        public async Task<IEnumerable<UserDetail>> Get()
        {
            return (await _context.UserDetails.ToListAsync());
        }

        public IQueryable<UserDetail> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDetail> Update(UserDetail item)
        {
            var UserDetail = await Get(item.UserId);
            if (UserDetail != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return UserDetail;
            }
            throw new Exception("No UserDetail with the given ID");
        }
    }
}
