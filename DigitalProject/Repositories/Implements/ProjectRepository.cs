using DigitalProject.Entitys;
using DigitalProject.Models.User.Project;
using DigitalProject.Repositories.Interface;
using System.Linq;

namespace DigitalProject.Repositories.Implements
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly MyDbContext _context;
        public ProjectRepository(MyDbContext context)
        {
            _context = context;

        }
        public Project GetProjectById(int projectId)
        {
            var project = _context.projects.FirstOrDefault(x => x.ProjectId == projectId);
            if (project == null) { return null; }
            return project;
        }
        public bool GetProjectByName(string ProjectName)
        {
            var project = _context.projects.FirstOrDefault(x => x.ProjectName == ProjectName);
            if (project == null) { return false; } else { return true; }
        }
        public void CreateProject(Project project)
        {
            project.PostedTime = DateTime.Now;
            _context.projects.Add(project);
            _context.SaveChanges();

        }
    }
}
