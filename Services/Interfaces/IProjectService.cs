using PFL_API.Models;
using PFL_API.Models.DTO;

namespace PFL_API.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectsByProfileIdAsync(int profileId);
        Task<Project?> GetProjectByIdAsync(int projectId);
        Task<Project> CreateProjectAsync(Project project);
        Task<Project?> UpdateProjectAsync(int projectId, UpdateProjectDto dto);
        Task<bool> DeleteProjectAsync(int projectId);
    }
}
