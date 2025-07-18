using DigitalProject.Models.User.Project;

namespace DigitalProject.Services.Interface
{
    public interface IProjectService
    {
        bool AddProject(ProjectDTO model);
    }
}
