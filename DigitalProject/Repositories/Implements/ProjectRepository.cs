using AutoMapper;
using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Project;
using DigitalProject.Models.User;
using DigitalProject.Repositories.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DigitalProject.Repositories.Implements
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly MyDbContext _context;

        private readonly IMapper _mapper;
        public ProjectRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Project> getListProject()
        {
            return _context.projects.ToList();
        }
        public PagingModel<ProjectDTO> getListProjectByKey(string? key, string? structuralEngineer, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize)
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
            var totalRecords = query.Count();
            var pagedData = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var data = _mapper.Map<List<ProjectDTO>>(pagedData);
            return new PagingModel<ProjectDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = data,
                TotalRecords = totalRecords
            };
        }
        public Project? GetProjectById(int projectId)
        {
            return _context.projects.FirstOrDefault(x => x.ProjectId == projectId);
        }
        public bool GetProjectByName(string ProjectName)
        {
            var project = _context.projects.FirstOrDefault(x => x.ProjectName == ProjectName);
            return project != null;
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
            return result >0;
        }
        public void DeleteProject(Project model)
        {
            _context.projects.Remove(model);
            _context.SaveChanges();
        }

    }
}
