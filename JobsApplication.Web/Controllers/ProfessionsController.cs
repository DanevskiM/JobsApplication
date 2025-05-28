using JobsApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobsApplication.Web.Controllers
{
    public class ProfessionsController : Controller
    {
        private readonly IProfessionService _professionService;

        public ProfessionsController(IProfessionService professionService)
        {
            _professionService = professionService;
        }

        // GET: Professions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var profession = _professionService.Create(userId);
            return RedirectToAction(nameof(Details), new { id=profession.Id });
        }
        // GET: Professions/Details/5
        public IActionResult Details(Guid id)
        {
            var profession = _professionService.GetDetailsById(id);
            if (profession == null)
                return NotFound();
            return View(profession);
        }
    }
}
