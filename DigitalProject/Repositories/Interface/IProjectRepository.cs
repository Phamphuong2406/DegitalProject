using DigitalProject.Entitys;

namespace DigitalProject.Repositories.Interface
{
    public interface IProjectRepository
    {
        public List<Project> getListProject();
        List<Project> getListProjectByKey(string? key, string? structuralEngineer, DateTime? postingStartDate = null, DateTime? postingEndDate = null);
        Project GetProjectById(int projectId);
        bool GetProjectByName(string ProjectName);
        void CreateProject(Project project);
        bool EditProject(Project project);
        void DeleteProject(Project model);
    }
}
