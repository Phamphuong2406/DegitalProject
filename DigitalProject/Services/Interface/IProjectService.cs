using DigitalProject.Entitys;
using DigitalProject.Models.User.Project;

namespace DigitalProject.Services.Interface
{
    public interface IProjectService
    {
        List<Project> GetListProject();
        Project getByProjectId(int projectId);
        bool AddProject(ProjectDTO model, int currentUserId);
        bool DeleteProject(int projectId);
    }
}
