using System.Collections;
using MVCAssignmentTwo.Models;

namespace MVCAssignmentTwo.Service
{
    public interface IProfessorService
    {
        List<Professor> GetAllProfessors();
        IEnumerable GetDepartments();
        void AddProfessor(Professor professor);
        void UpdateProfessor(Professor professor);
        Professor? GetProfessorById(int id);
    }
}
