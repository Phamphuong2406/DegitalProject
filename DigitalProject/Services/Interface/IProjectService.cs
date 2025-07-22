using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Project;

namespace DigitalProject.Services.Interface
{
    public interface IProjectService
    {
        List<ProjectDTO> GetListProject();
        PagingModel<ProjectDTO> getListProjectByKeyword(string? key, string? structuralEngineer, DateTime? postingStartDate , DateTime? postingEndDate , int pageNumber, int pageSize);
        Project getByProjectId(int projectId);
        void AddProject(ProjectDTO model, int currentUserId);
        bool EditProject(ProjectDTO model, int projectId);
        bool DeleteProject(int projectId);
    }
}
