using System.Data;
using System.Data.SqlClient;
using MVCAssignmentThree.Models;

namespace MVCAssignmentThree.Repository
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepositoryImpl(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MVCConnectionString");
        }

        public void RegisterUser(User user)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_RegisterUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FullName", user.FullName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@RoleId", user.RoleId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public User ValidateUser(string email, string password)
        {
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new(
                @"SELECT u.*, r.RoleName FROM [User] u
                                   JOIN Role r ON u.RoleId = r.RoleId
                                   WHERE Email=@Email AND Password=@Password",
                con
            );
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    UserId = (int)reader["UserId"],
                    FullName = reader["FullName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    RoleId = (int)reader["RoleId"],
                    Role = new Role { RoleName = reader["RoleName"].ToString() },
                };
            }
            return null;
        }

        public IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>();
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("SELECT * FROM Role", con);
            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roles.Add(
                    new Role
                    {
                        RoleId = (int)reader["RoleId"],
                        RoleName = reader["RoleName"].ToString(),
                    }
                );
            }
            return roles;
        }
    }
}
