using ClaimsProcessing.Data;
using ClaimsProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimsProcessing.Repositories
{
    public interface IClaimRepository
    {
        Task<IEnumerable<Claim>> GetAllClaims();
        Task<IEnumerable<Claim>> GetClaimsByAgentId(int agentId);
        Task<Claim?> GetClaimById(int id);
        Task AddClaim(Claim claim);
    }

    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext _context;

        public ClaimRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Claim>> GetAllClaims()
        {
            return await _context.Claims.ToListAsync();
        }

        public async Task<Claim?> GetClaimById(int id)
        {
            return await _context.Claims.FindAsync(id);
        }

        public async Task<IEnumerable<Claim>> GetClaimsByAgentId(int agentId)
        {
            return await _context.Claims.Where(c => c.AgentId == agentId).ToListAsync();
        }

        public async Task AddClaim(Claim claim)
        {
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
        }
    }
}
