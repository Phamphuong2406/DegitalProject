using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Project;

namespace DigitalProject.Services.Interface
{
    public interface IProjectService
    {
        List<ProjectDTO> GetListProject();
        PagingModel<ProjectDTO> GetListProjectByKeyword(string? key, string? structuralEngineer, DateTime? postingStartDate , DateTime? postingEndDate , int pageNumber, int pageSize);
        Project GetByProjectId(int projectId);
        void AddProject(ProjectDTO model, int currentUserId);
        void EditProject(ProjectDTO model, int projectId);
        void DeleteProject(int projectId);
    }
}
