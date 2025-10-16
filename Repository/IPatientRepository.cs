using MVCAssignmentsOne.Models;

namespace MVCAssignmentsOne.Repository
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAllPatientsWithMembership();
        IEnumerable<Membership> GetMemberships();

        void AddPatient(Patient patient);
        Patient? GetPatientById(int id);
        void UpdatePatient(Patient patient);
    }
}
