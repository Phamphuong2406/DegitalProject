using DigitalProject.Entitys;

namespace DigitalProject.Repositories.Interface
{
    public interface IProjectRepository
    {
          public List<Project> getListProject();
        Project GetProjectById(int projectId);
        bool GetProjectByName(string ProjectName);
        void CreateProject(Project project);
        void DeleteProject(Project model);
    }
}
