using ClaimsProcessing.Models;
using ClaimsProcessing.Repositories;

namespace ClaimsProcessing.Services
{
    public interface IClaimService
    {
        Task<IEnumerable<Claim>> GetClaimsByAgentId(int agentId);
    }

    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimService(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<IEnumerable<Claim>> GetClaimsByAgentId(int agentId)
        {
            return await _claimRepository.GetClaimsByAgentId(agentId);
        }
    }
}
