using AutoMapper;
using DigitalProject.Entitys;
using DigitalProject.Models.User.Project;
using DigitalProject.Repositories.Interface;
using DigitalProject.Services.Interface;

namespace DigitalProject.Services.Implements
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository projectRepo)
        {

            _projectRepo = projectRepo;
        }
        public List<Project> GetListProject()
        {
            return _projectRepo.getListProject();
        }
        public List<Project> getListProjectByKeyword(string? key, string? structuralEngineer, DateTime? postingStartDate = null, DateTime? postingEndDate = null)
        {
            try
            {

                return _projectRepo.getListProjectByKey(key, structuralEngineer, postingStartDate, postingEndDate);

            }
            catch (Exception)
            {

                throw new ApplicationException("Có lỗi xảy ra khi gọi danh sách bài viết");
            }
        }
        public Project getByProjectId(int projectId)
        {
            var project = _projectRepo.GetProjectById(projectId);
            if (project == null)
            {
                return null;
            }
            return project;
        }
        
        public bool AddProject(ProjectDTO model,int  currentUserId)
        {
            try
            {
                var result = _projectRepo.GetProjectByName(model.ProjectName);
                if(result == true)
                {
                    return false ;
                }
                
                var project = new Project
                {
                    ProjectName = model.ProjectName,
                    ProjectType = model.ProjectType,
                    ImageUrl = model.ImageUrl,
                    Shortdescription = model.Shortdescription,
                    DetailedDescription = model.DetailedDescription,
                    architect = model.architect,
                    structuralEngineer = model.structuralEngineer,
                    ConstructionEndTime = model.ConstructionEndTime,
                    ConstructionStartTime = model.ConstructionStartTime,
                    PostedTime = DateTime.Now,
                    DisplayOnHeader = model.DisplayOnHeader,
                    DisplayOnhome = model.DisplayOnhome,
                    DisplayOrderOnHeader = model.DisplayOrderOnHeader,
                    DisplayOrderOnHome = model.DisplayOrderOnHome,
                    ExpirationTimeOnHeader = model.ExpirationTimeOnHeader,
                    IdPoster = currentUserId,
                };
                _projectRepo.CreateProject(project);
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool EditProject(ProjectDTO model, int projectId)
        {

            var project = _projectRepo.GetProjectById(projectId);
            if (project == null) return false;

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
        public bool DeleteProject(int projectId)
        {
            var project = _projectRepo.GetProjectById(projectId);
            if (project == null) return false;
            _projectRepo.DeleteProject(project);
            return true;
        }
       
    }
}
