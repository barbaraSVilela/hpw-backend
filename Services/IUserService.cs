using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HPW.Entities;

namespace HPW.Services
{
    public interface IUserService
    {
        Task<User> CompleteUserInformation(User incompleteUser);

        Task<User> UpdateUser(User updatedUser);

        Task SendInvite(String invitedUserId, User currentUser);

        Task AcceptInvite(String inviteId, User currentUser);

        Task<IEnumerable<User>> GetFriends(User currentUser);

        Task<User> GetUserById(String id);

    }
}