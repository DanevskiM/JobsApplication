﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using JobsApplication.Service.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using JobsApplication.Service.Implementation;

namespace JobsApplication.Web.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IJobPositionService _jobPositionService;

        public ApplicationsController(IApplicationService applicationService, IJobPositionService jobPositionService)
        {
            _applicationService = applicationService;
            _jobPositionService = jobPositionService;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return View(_applicationService.GetAllByCurrentUser(userId));
        }


        // GET: Applications/Create
        [Authorize]
        public IActionResult Create(Guid candidateId)
        {
            ViewData["CandidateId"] = candidateId;
            ViewData["JobPositionId"] = new SelectList(_jobPositionService.GetAll(), "Id", "Department");
            return View();
        }

        // POST: Applications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("CandidateId,JobPositionId,Status")] Domain.DomainModels.Application application)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _applicationService.ScheduleApplicationForCandidateAndPosition(userId, application.CandidateId, application.JobPositionId);
            return RedirectToAction(nameof(Index));
        }


        // POST: Applications/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
                return NotFound();
            var application = _applicationService.GetById(id);
            if (application!=null)
            {
                _applicationService.DeleteById(id);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
