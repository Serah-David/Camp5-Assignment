using MVCAssignmentThree.Models;

namespace MVCAssignmentThree.Repository
{
    public interface IUserRepository
    {
        void RegisterUser(User user);
        User ValidateUser(string email, string password);
        IEnumerable<Role> GetRoles();
    }
}
