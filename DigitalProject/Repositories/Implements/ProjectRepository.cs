using DigitalProject.Entitys;
using DigitalProject.Repositories.Interface;

namespace DigitalProject.Repositories.Implements
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly MyDbContext _context;
        public ProjectRepository(MyDbContext context)
        {
            _context = context;

        }
        public List<Project> getListProject()
        {
            return _context.projects.Select(x => new Project
            {
                ProjectId = x.ProjectId,
                ProjectName = x.ProjectName,
                ProjectType = x.ProjectType,
                ImageUrl = x.ImageUrl,
                Shortdescription = x.Shortdescription,
                DetailedDescription = x.DetailedDescription,
                architect = x.architect,
                structuralEngineer = x.structuralEngineer,
                ConstructionEndTime = x.ConstructionEndTime,
                ConstructionStartTime = x.ConstructionStartTime,
                PostedTime = x.PostedTime,
                DisplayOnHeader = x.DisplayOnHeader,
                DisplayOnhome = x.DisplayOnhome,
                DisplayOrderOnHeader = x.DisplayOrderOnHeader,
                DisplayOrderOnHome = x.DisplayOrderOnHome,
                ExpirationTimeOnHeader = x.ExpirationTimeOnHeader,
                IdPoster = x.IdPoster,
            }).ToList();
        }
        public List<Project> getListProjectByKey(string? key, string? structuralEngineer, DateTime? postingStartDate = null, DateTime? postingEndDate = null)
        {
            var query = _context.projects.AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                key = key.ToLower();
                query = query.Where(z =>
                   z.ProjectName.ToLower().Contains(key) || z.Shortdescription.ToLower().Contains(key));
            }

            if (!string.IsNullOrEmpty(structuralEngineer))
            {
                query = query.Where(z => z.structuralEngineer.ToLower().Contains(structuralEngineer));
            }

            if (postingStartDate.HasValue)
            {
                query = query.Where(z => z.PostedTime >= postingStartDate);
            }
            if (postingEndDate.HasValue)
            {
                var adjustedEnd = postingEndDate.Value.AddDays(1).AddMinutes(-1);
                query = query.Where(z => z.PostedTime <= adjustedEnd);
            }


            return query.Select(x => new Project
            {
                ProjectId = x.ProjectId,
                ProjectName = x.ProjectName,
                ProjectType = x.ProjectType,
                ImageUrl = x.ImageUrl,
                Shortdescription = x.Shortdescription,
                DetailedDescription = x.DetailedDescription,
                architect = x.architect,
                structuralEngineer = x.structuralEngineer,
                ConstructionEndTime = x.ConstructionEndTime,
                ConstructionStartTime = x.ConstructionStartTime,
                PostedTime = x.PostedTime,
                DisplayOnHeader = x.DisplayOnHeader,
                DisplayOnhome = x.DisplayOnhome,
                DisplayOrderOnHeader = x.DisplayOrderOnHeader,
                DisplayOrderOnHome = x.DisplayOrderOnHome,
                ExpirationTimeOnHeader = x.ExpirationTimeOnHeader,
                IdPoster = x.IdPoster,
            }).ToList();
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
            _context.projects.Add(project);
            _context.SaveChanges();

        }
        public bool EditProject(Project project)
        {
            _context.projects.Update(project);
            var result = _context.SaveChanges();
            return result > 0;
        }
        public void DeleteProject(Project model)
        {
            _context.projects.Remove(model);
            _context.SaveChanges();
        }

    }
}
