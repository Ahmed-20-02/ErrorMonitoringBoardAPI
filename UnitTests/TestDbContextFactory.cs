namespace UnitTests
{
    using Microsoft.EntityFrameworkCore;
    using DevelopmentProjectErrorBoardAPI.Data;
    
    public class TestDbContextFactory : IDbContextFactory<DataContext>
    {
        private DbContextOptions<DataContext> _options;

        public TestDbContextFactory(string databaseName = "InMemoryTest")
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        public DataContext CreateDbContext()
        {
            return new DataContext(_options);
        }
    }
}

//References
//https://stackoverflow.com/questions/66101618/moq-idbcontextfactory-with-in-memory-ef-core
//https://stackoverflow.com/questions/71115151/blazor-mocking-idbcontextfactory
//03/01/2024