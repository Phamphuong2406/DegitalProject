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
        public bool AddProject(ProjectDTO model)
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
                    PostedTime = model.PostedTime,
                    DisplayOnHeader = model.DisplayOnHeader,
                    DisplayOnhome = model.DisplayOnhome,
                    DisplayOrderOnHeader = model.DisplayOrderOnHeader,
                    DisplayOrderOnHome = model.DisplayOrderOnHome,
                    ExpirationTimeOnHeader = model.ExpirationTimeOnHeader,
                    IdPoster = model.IdPoster,
                };
                _projectRepo.CreateProject(project);
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
