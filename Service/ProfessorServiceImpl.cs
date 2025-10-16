using System.Collections;
using MVCAssignmentTwo.Models;
using MVCAssignmentTwo.Repository;

namespace MVCAssignmentTwo.Service
{
    public class ProfessorServiceImpl : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorServiceImpl(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public void AddProfessor(Professor professor)
        {
            _professorRepository.AddProfessor(professor);
        }

        public List<Professor> GetAllProfessors()
        {
            return _professorRepository.GetAllProfessors();
        }

        public IEnumerable GetDepartments()
        {
            return _professorRepository.GetDepartments();
        }

        public Professor? GetProfessorById(int id)
        {
            return _professorRepository.GetProfessorById(id);
        }

        public void UpdateProfessor(Professor professor)
        {
            _professorRepository.UpdateProfessor(professor);
        }
    }
}
