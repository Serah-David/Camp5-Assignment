using MVCAssignmentsOne.Models;

namespace MVCAssignmentsOne.Service
{
    public interface IPatientService
    {
        void AddPatient(Patient patient);
        IEnumerable<Patient> GetAllPatientsWithMembership();
        IEnumerable<Membership> GetMemberships();
        Patient? GetPatientById(int id);
        void UpdatePatient(Patient patient);
    }
}
