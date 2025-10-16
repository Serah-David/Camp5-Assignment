using MVCAssignmentTwo.Models;

namespace MVCAssignmentTwo.Repository
{
    public interface IProfessorRepository
    {
        void AddProfessor(Professor professor);
        List<Professor> GetAllProfessors();
        IEnumerable<Department> GetDepartments();
        Professor? GetProfessorById(int id);
        void UpdateProfessor(Professor professor);
    }
}
