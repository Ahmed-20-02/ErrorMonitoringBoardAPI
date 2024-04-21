namespace DevelopmentProjectErrorBoardAPI.Business.Getters
{
    using DevelopmentProjectErrorBoardAPI.Data.Queries.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Getters.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Business.Mappers.Interfaces;
    using DevelopmentProjectErrorBoardAPI.Resources;

    public class ProjectsGetter : IProjectsGetter
    {
        private readonly IGetProjectsQuery _getProjectsQuery;
        private readonly IProjectModelMapper _projectModelMapper;

        public ProjectsGetter(IGetProjectsQuery getProjectsQuery, 
            IProjectModelMapper projectModelMapper)
        {
            _getProjectsQuery = getProjectsQuery;
            _projectModelMapper = projectModelMapper;
        }
        
        public List<ProjectModel> Get()
        {
            try
            {
                var projects = _getProjectsQuery.Get().Result;
                var projectModels = new List<ProjectModel>();
                
                for (int i = 0; i < projects.Count; i++)
                {
                    projectModels.Add(_projectModelMapper.Map(projects[i]));
                }

                return projectModels;
 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}