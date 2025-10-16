using MVCAssignmentThree.Models;

namespace MVCAssignmentThree.Service
{
    public interface IUserService
    {
        void RegisterUser(User user);
        User ValidateUser(string email, string password);
        IEnumerable<Role> GetRoles();
    }
}
