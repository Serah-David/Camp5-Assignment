using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAssignmentsOne.Models;
using MVCAssignmentsOne.Service;

namespace MVCAssignmentsOne.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // Show all patients
        public IActionResult Index()
        {
            var patients = _patientService.GetAllPatientsWithMembership();
            return View(patients);
        }

        // Show create form
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Memberships = _patientService.GetMemberships();
            return View();
        }

        // Handle form submission
        [HttpPost]
        public IActionResult Create([Bind] Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _patientService.AddPatient(patient);
                    TempData["SuccessMessage"] = "Patient added successfully!";
                    return RedirectToAction("Index"); // Go back to patient list
                }

                // If validation fails, reload memberships for dropdown
                ViewBag.Memberships = _patientService.GetMemberships();
                return View(patient);
            }
            catch (Exception ex)
            {
                // Log or handle errors
                ViewBag.ErrorMessage = "Error adding patient: " + ex.Message;
                ViewBag.Memberships = _patientService.GetMemberships();
                return View(patient);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var patient = _patientService.GetPatientById(id);

            var memberships = _patientService.GetMemberships();

            ViewBag.Memberships = new SelectList(
                memberships,
                "MembershipId", // value field
                "MembershipDesc", // text field
                patient.MembershipId // selected value
            );

            return View(patient);
        }

        // ✅ Handle edit form submission
        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _patientService.UpdatePatient(patient);
                    return RedirectToAction("Index");
                }

                // Validation failed — reload dropdown
                ViewBag.Memberships = _patientService.GetMemberships();
                return View(patient);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error updating patient: " + ex.Message;
                ViewBag.Memberships = _patientService.GetMemberships();
                return View(patient);
            }
        }
    }
}
