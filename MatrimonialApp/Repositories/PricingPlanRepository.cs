using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrimonialApp.Repositories
{
    public class PricingPlanRepository : IRepository<int, PricingPlan>
    {
        private MatrimonialContext _context;

        public PricingPlanRepository(MatrimonialContext context)
        {
            _context = context;
        }

        public IQueryable<PricingPlan> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<PricingPlan> Delete(int key)
        {
            var pricingPlan = await _context.PricingPlans.FindAsync(key);
            if (pricingPlan != null)
            {
                _context.PricingPlans.Remove(pricingPlan);
                await _context.SaveChangesAsync();
            }
            return pricingPlan;
        }


        public async Task<PricingPlan> Get(int key)
        {
            return await _context.PricingPlans.FindAsync(key);
        }

        public async Task<IEnumerable<PricingPlan>> Get()
        {
            return await _context.PricingPlans.ToListAsync();
        }

        public async Task<PricingPlan> Add(PricingPlan pricingPlan)
        {
            _context.PricingPlans.Add(pricingPlan);
               await _context.SaveChangesAsync();
            return pricingPlan;
        }

        public async Task<PricingPlan> Update(PricingPlan item)
        {
            try
            {
                var data = await _context.PricingPlans.FindAsync(item.PricingPlanId);
                if (data == null)
                {
                    throw new Exception("Pricing plan not found.");
                }

                // Update properties
                data.Price = item.Price;
                //data.Name = item.Name; // Update other properties if needed
                //data.Description = item.Description;

                _context.PricingPlans.Update(data);
                await _context.SaveChangesAsync();

                return data;
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error updating pricing plan: {ex.Message}");
                throw; // Re-throw the exception after logging it
            }
        }
    }

}
