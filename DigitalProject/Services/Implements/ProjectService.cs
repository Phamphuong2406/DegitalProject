using DigitalProject.Entitys;
using DigitalProject.Models.User.Project;
using DigitalProject.Repositories.Interface;
using DigitalProject.Services.Interface;

namespace DigitalProject.Services.Implements
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        public ProjectService(IProjectRepository projectRepo)
        {

            _projectRepo = projectRepo;
        }
        public List<Project> GetListProject()
        {
            return _projectRepo.getListProject();
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

        public bool DeleteProject(int projectId)
        {
            var project = _projectRepo.GetProjectById(projectId);
            if (project == null) return false;
            _projectRepo.DeleteProject(project);
            return true;
        }
    }
}
