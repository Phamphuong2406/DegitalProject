using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Project;

namespace DigitalProject.Repositories.Interface
{
    public interface IProjectRepository
    {
         List<Project> GetListProject();
        PagingModel<ProjectDTO> GetListProjectByKey(string? key, string? structuralEngineer, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize);
        Project? FindById(int projectId);
        bool FindByName(string ProjectName);
        void CreateProject(Project project);
        void EditProject(Project project);
        void DeleteProject(Project model);
    }
}
