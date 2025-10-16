using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MVCAssignmentTwo.Models;
using MVCAssignmentTwo.Repository;

public class ProfessorRepositoryImpl : IProfessorRepository
{
    private readonly string _connectionString;

    public ProfessorRepositoryImpl(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MVCConnectionString");
    }

    public List<Professor> GetAllProfessors()
    {
        var professors = new List<Professor>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand("sp_GetAllProfessors", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    professors.Add(
                        new Professor
                        {
                            ProfessorId = Convert.ToInt32(reader["ProfessorId"]),
                            ManagerId = reader["ManagerId"] as int?,
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Salary = reader["Salary"] as decimal?,
                            JoiningDate = reader["JoiningDate"] as DateTime?,
                            DateOfBirth = reader["DateOfBirth"] as DateTime?,
                            Gender = reader["Gender"].ToString(),
                            DeptNo = Convert.ToInt32(reader["DeptNo"]),
                            Department = new Department
                            {
                                DeptName = reader["DeptName"].ToString(),
                                Location = reader["Location"].ToString(),
                            },
                        }
                    );
                }
            }
        }

        return professors;
    }

    public void AddProfessor(Professor professor)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand("sp_AddProfessor", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", professor.FirstName);
            cmd.Parameters.AddWithValue("@LastName", professor.LastName);
            cmd.Parameters.AddWithValue("@Salary", professor.Salary ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue(
                "@JoiningDate",
                professor.JoiningDate ?? (object)DBNull.Value
            );
            cmd.Parameters.AddWithValue(
                "@DateOfBirth",
                professor.DateOfBirth ?? (object)DBNull.Value
            );
            cmd.Parameters.AddWithValue("@Gender", professor.Gender ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@DeptNo", professor.DeptNo);
            cmd.Parameters.AddWithValue("@ManagerId", professor.ManagerId ?? (object)DBNull.Value);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public IEnumerable<Department> GetDepartments()
    {
        var departments = new List<Department>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        using (
            SqlCommand cmd = new SqlCommand(
                "SELECT DeptNo, DeptName, Location FROM Department",
                con
            )
        )
        {
            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    departments.Add(
                        new Department
                        {
                            DeptNo = Convert.ToInt32(reader["DeptNo"]),
                            DeptName = reader["DeptName"].ToString(),
                            Location = reader["Location"].ToString(),
                        }
                    );
                }
            }
        }

        return departments;
    }

    public Professor? GetProfessorById(int id)
    {
        Professor? professor = null;

        using (SqlConnection con = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand("sp_GetProfessorById", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProfessorId", id);

            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    professor = new Professor
                    {
                        ProfessorId = Convert.ToInt32(reader["ProfessorId"]),
                        ManagerId = reader["ManagerId"] as int?,
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Salary = reader["Salary"] as decimal?,
                        JoiningDate = reader["JoiningDate"] as DateTime?,
                        DateOfBirth = reader["DateOfBirth"] as DateTime?,
                        Gender = reader["Gender"].ToString(),
                        DeptNo = Convert.ToInt32(reader["DeptNo"]),
                        Department = new Department
                        {
                            DeptName = reader["DeptName"].ToString(),
                            Location = reader["Location"].ToString(),
                        },
                    };
                }
            }
        }

        return professor;
    }

    public void UpdateProfessor(Professor professor)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand("sp_UpdateProfessor", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProfessorId", professor.ProfessorId);
            cmd.Parameters.AddWithValue("@FirstName", professor.FirstName);
            cmd.Parameters.AddWithValue("@LastName", professor.LastName);
            cmd.Parameters.AddWithValue("@Salary", professor.Salary ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue(
                "@JoiningDate",
                professor.JoiningDate ?? (object)DBNull.Value
            );
            cmd.Parameters.AddWithValue(
                "@DateOfBirth",
                professor.DateOfBirth ?? (object)DBNull.Value
            );
            cmd.Parameters.AddWithValue("@Gender", professor.Gender ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@DeptNo", professor.DeptNo);
            cmd.Parameters.AddWithValue("@ManagerId", professor.ManagerId ?? (object)DBNull.Value);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
