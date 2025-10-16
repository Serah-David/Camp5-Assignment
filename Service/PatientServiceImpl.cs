using MVCAssignmentsOne.Models;
using MVCAssignmentsOne.Repository;

namespace MVCAssignmentsOne.Service
{
    public class PatientServiceImpl : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientServiceImpl(IPatientRepository repository)
        {
            _repository = repository;
        }

        // Fixed: AddPatient should return void, not IEnumerable<Patient>
        public void AddPatient(Patient patient)
        {
            _repository.AddPatient(patient);
        }

        public IEnumerable<Patient> GetAllPatientsWithMembership()
        {
            return _repository.GetAllPatientsWithMembership();
        }

        public IEnumerable<Membership> GetMemberships()
        {
            return _repository.GetMemberships();
        }

        public Patient? GetPatientById(int id)
        {
            return _repository.GetPatientById(id);
        }

        public void UpdatePatient(Patient patient)
        {
            _repository.UpdatePatient(patient);
        }
    }
}
