using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HPW.Entities;
using HPW.Extensions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Newtonsoft.Json.Linq;

namespace HPW.Services
{
    public class DailyChallengeService : IDailyChallengeService
    {
        private readonly IChallengeService _challengeService;
        private readonly IUserService _userService;
        private readonly Container _container;
        public DailyChallengeService(
            IChallengeService challengeService,
            Microsoft.Azure.Cosmos.Database database,
            IUserService userService)
        {
            _challengeService = challengeService;
            _userService = userService;
            _container = database.GetContainer("DailyChallenge");
        }

        public async Task<Challenge> GetTodaysChallenge(int level)
        {

            var allChallenges = await ExecuteDailyChallengeQuery(_container.GetItemLinqQueryable<DailyChallenge>().ToFeedIterator());

            var result = allChallenges.Where(c => c.Date.ToUniversalTime().Date == DateTime.Now.ToUniversalTime().Date);

            var challenge = result.FirstOrDefault(c => c.Level == level);
            if (challenge != null)
            {
                return await _challengeService.GetChallenge(challenge.ChallengeId);
            }
            else
            {
                return null;
            }
        }

        public async Task SetTodaysChallenges()
        {
            var challengesByLevel = await _challengeService.GetAllChallengesSplitByLevel();
            var selectedChallenges = new Dictionary<int, Challenge>();

            foreach (var level in challengesByLevel)
            {
                var challenges = challengesByLevel.Values.SelectMany((v) => v).ToList();
                var random = new Random();
                var randomIndex = random.Next(0, challenges.Count - 1);

                selectedChallenges[level.Key] = challenges[randomIndex];
            }

            foreach (var challenge in selectedChallenges)
            {
                var dailyChallenge = new DailyChallenge()
                {
                    Id = Guid.NewGuid().ToString(),
                    ChallengeId = challenge.Value.Id,
                    Date = DateTime.Today,
                    Level = challenge.Value.Level,
                };

                await _container.UpsertItemAsync(dailyChallenge, new PartitionKey(dailyChallenge.ChallengeId));


            }
        }

        public async Task<Entities.User> SolveTodaysChallenge(Entities.User user, bool wasSuccessful)
        {

            if (wasSuccessful)
            {
                if (user.SolvedChallengesIds == null)
                {
                    user.SolvedChallengesIds = new List<string>();
                }

                var todaysChallenge = await GetTodaysChallenge(user.Level);
                user.SolvedChallengesIds.Add(todaysChallenge.Id);
                user.Level++;
                user.Streak++;
            }
            else
            {
                user.Streak = 0;
            }

            return await _userService.UpdateUser(user);

        }

        private async Task<IEnumerable<DailyChallenge>> ExecuteDailyChallengeQuery(FeedIterator<DailyChallenge> query)
        {
            return await query.ReadAllAsync();
        }

    }
}