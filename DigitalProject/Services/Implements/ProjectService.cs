using AutoMapper;
using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Project;
using DigitalProject.Repositories.Interface;
using DigitalProject.Services.Interface;

namespace DigitalProject.Services.Implements
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }
        public List<ProjectDTO> GetListProject()
        {
            try
            {
                var data = _projectRepo.getListProject();
                return _mapper.Map<List<ProjectDTO>>(data);
            }
            catch (Exception)
            {
                throw;
            }
         
        }
        public PagingModel<ProjectDTO> getListProjectByKeyword(string? key, string? structuralEngineer, DateTime? postingStartDate , DateTime? postingEndDate, int pageNumber, int pageSize)
        {
            try
            {
                return _projectRepo.getListProjectByKey(key, structuralEngineer, postingStartDate, postingEndDate, pageNumber, pageSize);
            }
            catch (Exception)
            {

                throw new ApplicationException("Có lỗi xảy ra khi gọi danh sách bài viết");
            }
        }
        public Project getByProjectId(int projectId)
        {
            try
            {
                return _projectRepo.GetProjectById(projectId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AddProject(ProjectDTO model, int currentUserId)
        {
            try
            {
                var result = _projectRepo.GetProjectByName(model.ProjectName);
               
                var project = _mapper.Map<Project>(model);
                project.IdPoster = currentUserId;
                project.PostedTime = DateTime.Now;
                _projectRepo.CreateProject(project);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool EditProject(ProjectDTO model, int projectId)
        {
            try
            {
                var project = _projectRepo.GetProjectById(projectId);
                project.ProjectName = model.ProjectName;
                project.ProjectType = model.ProjectType;
                project.ImageUrl = model.ImageUrl;
                project.Shortdescription = model.Shortdescription;
                project.DetailedDescription = model.DetailedDescription;
                project.architect = model.architect;
                project.structuralEngineer = model.structuralEngineer;
                project.ConstructionEndTime = model.ConstructionEndTime;
                project.ConstructionStartTime = model.ConstructionStartTime;
                project.PostedTime = DateTime.Now;
                project.DisplayOnHeader = model.DisplayOnHeader;
                project.DisplayOnhome = model.DisplayOnhome;
                project.DisplayOrderOnHeader = model.DisplayOrderOnHeader;
                project.DisplayOrderOnHome = model.DisplayOrderOnHome;
                project.ExpirationTimeOnHeader = model.ExpirationTimeOnHeader;

                return _projectRepo.EditProject(project);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public bool DeleteProject(int projectId)
        {
            try
            {
                var project = _projectRepo.GetProjectById(projectId);
                _projectRepo.DeleteProject(project);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
          
        }

    }
}
