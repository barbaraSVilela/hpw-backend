using System.Collections.Generic;
using System.Threading.Tasks;
using HPW.Entities;
using HPW.Extensions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace HPW.Services
{
    public class RewardService : IRewardService
    {
        private readonly Container _container;

        public RewardService(Microsoft.Azure.Cosmos.Database database)
        {
            _container = database.GetContainer("Reward");
        }

        public async Task<IEnumerable<Reward>> GetAllRewards()
        {
            var query = _container.GetItemLinqQueryable<Reward>().ToFeedIterator();

            return await query.ReadAllAsync();
        }
    }
}