using Microsoft.EntityFrameworkCore;
using PFL_API.Data;
using PFL_API.Models;
using PFL_API.Models.DTO;
using PFL_API.Services.Interfaces;

namespace PFL_API.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET ALL BY PROFILE ID
        // =========================
        public async Task<List<Project>> GetProjectsByProfileIdAsync(int profileId)
        {
            return await _context.Projects
                .Where(p => p.ProfileId == profileId)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<Project?> GetProjectByIdAsync(int projectId)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Project> CreateProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<Project?> UpdateProjectAsync(int projectId, UpdateProjectDto dto)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
                return null;

            project.Name = dto.Name;
            project.Description = dto.Description;
            project.Technologies = dto.Technologies;
            project.Role = dto.Role;
            project.ProjectUrl = dto.ProjectUrl;

            await _context.SaveChangesAsync();
            return project;
        }

        // =========================
        // DELETE
        // =========================
        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
                return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
