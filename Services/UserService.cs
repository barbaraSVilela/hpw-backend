using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPW.Database;
using HPW.Entities;
using HPW.Extensions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using User = HPW.Entities.User;

namespace HPW.Services
{
    public class UserService : IUserService
    {
        private readonly Container _container;
        private const string UserPartitionKey = "id";

        public UserService(Microsoft.Azure.Cosmos.Database database, DatabaseSettings cosmosStoreSettings)
        {
            _container = database.GetContainer("User");
        }

        public Task<User> CompleteUserInformation(User incompleteUser)
        {
            return GetOrCreateUser(incompleteUser);
        }

        private async Task<User> GetOrCreateUser(User incompleteUser)
        {
            var user = await GetUserByEmail(incompleteUser.Email);
            if (user == null)
            {
                user = await CreateUser(incompleteUser);
            }
            return user;
        }

        private async Task<User> GetUserByEmail(String email)
        {
            //Define query
            var query = _container.GetItemLinqQueryable<User>().ToFeedIterator();

            return (await ExecuteUserQuery(query)).FirstOrDefault();
        }

        private async Task<User> CreateUser(User user)
        {

            if (user.Email != null &&
                        user.Name != null)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Coins = 0,
                    Email = user.Email,
                    // Friends = new List<User>(),
                    Invites = new List<Invite>(),
                    Level = 1,
                    Name = user.Name,
                    Rewards = new List<Reward>(),
                    Streak = 0,
                };
                var result = await _container.UpsertItemAsync(newUser, new PartitionKey(newUser.Id));
                return result.Resource;
            }
            else
            {
                throw new ArgumentException("Missing user information.");
            }
        }

        private async Task<IEnumerable<User>> ExecuteUserQuery(FeedIterator<User> query)
        {
            var response = await query.ReadAllAsync();
            return response;

        }
    }
}