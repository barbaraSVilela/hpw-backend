using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUserService _userService;

        public RewardService(Microsoft.Azure.Cosmos.Database database, IUserService userService)
        {
            _container = database.GetContainer("Reward");
            _userService = userService;
        }

        public async Task<IEnumerable<Reward>> GetAllRewards()
        {
            var query = _container.GetItemLinqQueryable<Reward>().ToFeedIterator();

            return await query.ReadAllAsync();
        }

        public async Task<Entities.User> PurchaseReward(string rewardId, Entities.User user)
        {
            var query = _container.GetItemLinqQueryable<Reward>().ToFeedIterator();

            var reward = (await query.ReadAllAsync()).FirstOrDefault(r => r.Id.Equals(rewardId));

            if (reward == null)
            {
                throw new Exception("Reward not found");
            }
            if (reward.Price > user.Coins)
            {
                throw new Exception("You don't have enough punchcards to purchase this.");
            }


            if (user.Rewards == null)
            {
                user.Rewards = new List<Reward>();
            }

            if (user.Rewards.Any(r => r.Id == reward.Id))
            {
                throw new Exception("You already have this reward");
            }

            user.Coins = user.Coins - reward.Price;
            user.Rewards.Add(reward);

            await _userService.UpdateUser(user);

            return user;

        }
    }
}