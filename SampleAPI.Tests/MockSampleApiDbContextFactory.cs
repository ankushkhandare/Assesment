using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SampleAPI.Infrastructure.Data;


namespace SampleAPI.Tests
{
    /// <summary>
    /// Mock database provided for your convenience.
    /// </summary>
    internal static class MockSampleApiDbContextFactory
    {
        public static SampleApiDbContext GenerateMockContext()
        {
            var options = new DbContextOptionsBuilder<SampleApiDbContext>()
                .UseInMemoryDatabase(databaseName: "mock_SampleApiDbContext")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var context = new SampleApiDbContext(options);
            return context;
        }
    }
}
