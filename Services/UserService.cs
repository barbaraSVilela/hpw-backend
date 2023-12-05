using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public UserService(Microsoft.Azure.Cosmos.Database database)
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
            var query = _container.GetItemLinqQueryable<User>().Where(u => u.Email == email).ToFeedIterator();

            return (await ExecuteUserQuery(query)).FirstOrDefault();
        }

        private async Task<User> GetUserById(String id)
        {
            var query = _container.GetItemLinqQueryable<User>().Where(u => u.Id == id).ToFeedIterator();
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
                    Friends = new List<String>(),
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

        public async Task<User> UpdateUser(User updatedUser)
        {
            var result = await _container.ReplaceItemAsync(updatedUser, updatedUser.Id, new PartitionKey(updatedUser.Id));

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception();
            }

            return result.Resource;
        }

        public async Task SendInvite(String invitedUserId, User currentUser)
        {
            var invitedUser = await GetUserById(invitedUserId);

            var invite = new Invite()
            {
                FromUserId = currentUser.Id,
                ToUserId = invitedUser.Id,
                Id = Guid.NewGuid().ToString()
            };

            if (invitedUser.Invites == null)
            {
                invitedUser.Invites = new List<Invite>();
            }

            invitedUser.Invites.Add(invite);

            await UpdateUser(invitedUser);
        }

        public async Task AcceptInvite(String inviteId, User currentUser)
        {
            var invite = currentUser.Invites.First(i => i.Id == inviteId);

            var newFriend = await GetUserById(invite.FromUserId);

            if (currentUser.Friends == null)
            {
                currentUser.Friends = new List<String>();
            }

            if (newFriend.Friends == null)
            {
                newFriend.Friends = new List<String>();
            }

            currentUser.Friends.Add(newFriend.Id);
            newFriend.Friends.Add(currentUser.Id);

            currentUser.Invites.Remove(invite);

            await UpdateUser(currentUser);
            await UpdateUser(newFriend);
        }

        public async Task<IEnumerable<User>> GetFriends(User currentUser)
        {
            var result = new List<User>();

            foreach (var id in currentUser.Friends)
            {
                var friend = await GetUserById(id);
                result.Add(friend);
            }

            return result;

        }
    }
}