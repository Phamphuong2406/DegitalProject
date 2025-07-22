using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Project;

namespace DigitalProject.Repositories.Interface
{
    public interface IProjectRepository
    {
         List<Project> getListProject();
        PagingModel<ProjectDTO> getListProjectByKey(string? key, string? structuralEngineer, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize);
        Project? GetProjectById(int projectId);
        bool GetProjectByName(string ProjectName);
        void CreateProject(Project project);
        bool EditProject(Project project);
        void DeleteProject(Project model);
    }
}
