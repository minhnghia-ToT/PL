using Microsoft.AspNetCore.Mvc;
using PFL_API.Models;
using PFL_API.Models.DTO;
using PFL_API.Services.Interfaces;

namespace PFL_API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // =========================
        // GET BY PROFILE ID
        // =========================
        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> GetByProfileId(int profileId)
        {
            var projects = await _projectService.GetProjectsByProfileIdAsync(profileId);
            return Ok(projects);
        }

        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            if (project == null)
                return NotFound("Project not found");

            return Ok(project);
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            var result = await _projectService.CreateProjectAsync(project);
            return Ok(result);
        }

        // =========================
        // UPDATE
        // =========================
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, UpdateProjectDto dto)
            {
                var result = await _projectService.UpdateProjectAsync(id, dto);

                if (result == null)
                    return NotFound("Project not found");

                return Ok(result);
            }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _projectService.DeleteProjectAsync(id);

            if (!success)
                return NotFound("Project not found");

            return Ok("Deleted successfully");
        }
    }
}
