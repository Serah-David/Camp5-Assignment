using System.Data;
using System.Data.SqlClient;
using MVCAssignmentsOne.Models;

namespace MVCAssignmentsOne.Repository
{
    public class PatientRepositoryImpl : IPatientRepository
    {
        private readonly string _connectionString;

        public PatientRepositoryImpl(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MVCConnectionString");
        }

        // 🔹 Add a new patient
        public void AddPatient(Patient patient)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_AddPatient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@RegistrationNo", patient.RegistrationNo);
                    cmd.Parameters.AddWithValue("@PatientName", patient.PatientName);
                    cmd.Parameters.AddWithValue("@Dob", patient.Dob);
                    cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                    cmd.Parameters.AddWithValue(
                        "@Address",
                        (object?)patient.Address ?? DBNull.Value
                    );
                    cmd.Parameters.AddWithValue(
                        "@PhoneNo",
                        (object?)patient.PhoneNo ?? DBNull.Value
                    );
                    cmd.Parameters.AddWithValue("@MembershipId", patient.MembershipId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Patient> GetAllPatientsWithMembership()
        {
            var patients = new List<Patient>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllPatientsWithMembership", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var patient = new Patient
                        {
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            RegistrationNo = reader["RegistrationNo"].ToString(),
                            PatientName = reader["PatientName"].ToString(),
                            Dob = Convert.ToDateTime(reader["Dob"]),
                            Gender = reader["Gender"].ToString(),
                            Address = reader["Address"].ToString(),
                            PhoneNo = reader["PhoneNo"].ToString(),
                            MembershipId = Convert.ToInt32(reader["MembershipId"]),
                            Membership = new Membership
                            {
                                MembershipId = Convert.ToInt32(reader["MembershipId"]),
                                MembershipDesc = reader["MembershipDesc"].ToString(),
                                InsuredAmount = Convert.ToDecimal(reader["InsuredAmount"]),
                            },
                        };
                        patients.Add(patient);
                    }
                }
            }

            return patients;
        }

        public IEnumerable<Membership> GetMemberships()
        {
            List<Membership> memberships = new List<Membership>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT MembershipId, MembershipDesc, InsuredAmount FROM Membership";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            memberships.Add(
                                new Membership
                                {
                                    MembershipId = Convert.ToInt32(reader["MembershipId"]),
                                    MembershipDesc = reader["MembershipDesc"].ToString(),
                                    InsuredAmount = Convert.ToDecimal(reader["InsuredAmount"]),
                                }
                            );
                        }
                    }
                }
            }

            return memberships;
        }

        public Patient GetPatientById(int id)
        {
            Patient patient = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query =
                    @"SELECT 
                            p.PatientId,
                            p.RegistrationNo,
                            p.PatientName,
                            p.Dob,
                            p.Gender,
                            p.Address,
                            p.PhoneNo,
                            p.MembershipId,
                            m.MembershipDesc,
                            m.InsuredAmount
                         FROM Patient p
                         INNER JOIN Membership m ON p.MembershipId = m.MembershipId
                         WHERE p.PatientId = @PatientId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PatientId", id);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        patient = new Patient
                        {
                            PatientId = (int)dr["PatientId"],
                            RegistrationNo = dr["RegistrationNo"].ToString(),
                            PatientName = dr["PatientName"].ToString(),
                            Dob = Convert.ToDateTime(dr["Dob"]),
                            Gender = dr["Gender"].ToString(),
                            Address = dr["Address"] as string,
                            PhoneNo = dr["PhoneNo"] as string,
                            MembershipId = (int)dr["MembershipId"],
                            Membership = new Membership
                            {
                                MembershipId = (int)dr["MembershipId"],
                                MembershipDesc = dr["MembershipDesc"].ToString(),
                                InsuredAmount = Convert.ToDecimal(dr["InsuredAmount"]),
                            },
                        };
                    }
                }
            }

            return patient;
        }

        public void UpdatePatient(Patient patient)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_UpdatePatient", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PatientId", patient.PatientId);
                cmd.Parameters.AddWithValue("@RegistrationNo", patient.RegistrationNo);
                cmd.Parameters.AddWithValue("@PatientName", patient.PatientName);
                cmd.Parameters.AddWithValue("@Dob", patient.Dob);
                cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                cmd.Parameters.AddWithValue("@Address", (object?)patient.Address ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PhoneNo", (object?)patient.PhoneNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MembershipId", patient.MembershipId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
