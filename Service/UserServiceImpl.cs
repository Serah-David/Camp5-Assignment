using MVCAssignmentThree.Models;
using MVCAssignmentThree.Repository;

namespace MVCAssignmentThree.Service
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _repository;

        public UserServiceImpl(IUserRepository repository)
        {
            _repository = repository;
        }

        public void RegisterUser(User user) => _repository.RegisterUser(user);

        public User ValidateUser(string email, string password) =>
            _repository.ValidateUser(email, password);

        public IEnumerable<Role> GetRoles() => _repository.GetRoles();
    }
}
