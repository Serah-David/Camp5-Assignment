using Microsoft.AspNetCore.Mvc;
using MVCAssignmentTwo.Models;
using MVCAssignmentTwo.Service;

public class ProfessorController : Controller
{
    private readonly IProfessorService _professorService;

    public ProfessorController(IProfessorService professorService)
    {
        _professorService = professorService;
    }

    public IActionResult Index()
    {
        var professors = _professorService.GetAllProfessors();
        return View(professors);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Departments = _professorService.GetDepartments();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Professor professor)
    {
        if (ModelState.IsValid)
        {
            _professorService.AddProfessor(professor);
            return RedirectToAction("Index"); // Redirect to list after adding
        }
        ViewBag.Departments = _professorService.GetDepartments();
        return View(professor);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var professor = _professorService.GetProfessorById(id);
        if (professor == null)
        {
            return NotFound();
        }

        ViewBag.Departments = _professorService.GetDepartments();
        return View(professor);
    }

    [HttpPost]
    public IActionResult Edit(Professor professor)
    {
        if (ModelState.IsValid)
        {
            _professorService.UpdateProfessor(professor);
            return RedirectToAction("Index");
        }

        ViewBag.Departments = _professorService.GetDepartments();
        return View(professor);
    }
}
