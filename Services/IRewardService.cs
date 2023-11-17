using System.Collections.Generic;
using System.Threading.Tasks;
using HPW.Entities;

namespace HPW.Services
{
    public interface IRewardService
    {
        Task<IEnumerable<Reward>> GetAllRewards();
        Task<Entities.User> PurchaseReward(string rewardId, Entities.User user);
    }
}