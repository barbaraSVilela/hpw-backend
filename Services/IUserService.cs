using System.Threading.Tasks;
using HPW.Entities;

namespace HPW.Services
{
    public interface IUserService
    {
        Task<User> CompleteUserInformation(User incompleteUser);
    }
}