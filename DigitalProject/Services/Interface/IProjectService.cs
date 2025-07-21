using DigitalProject.Entitys;
using DigitalProject.Models.User.Project;

namespace DigitalProject.Services.Interface
{
    public interface IProjectService
    {
        List<Project> GetListProject();
        List<Project> getListProjectByKeyword(string? key, string? structuralEngineer, DateTime? postingStartDate = null, DateTime? postingEndDate = null);
        Project getByProjectId(int projectId);
        bool AddProject(ProjectDTO model, int currentUserId);
        bool EditProject(ProjectDTO model, int projectId);
        bool DeleteProject(int projectId);
    }
}
