using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace MoqEntityFrameworkIssueReproduction
{
    //.Net Core 2.2.0
    //Moq 4.10.1
    //EF Core 2.2.0

    public class TestEntity1
    {

    }

    public class TestDbContext : DbContext
    {
        public virtual DbSet<TestEntity1> TestEntities1 { get; set; }
    }

    public class UnitTest
    {
        [Test]
        public void Test()
        {
            //Arrange - just usual context mock + dbset mock to use when setting a property
            var contextMock = new Mock<TestDbContext>();
            var dbSetMock = new Mock<DbSet<TestEntity1>>();

            //Act - just invoke any property setter
            contextMock.Object.TestEntities1 = dbSetMock.Object;

            //Assert - should fail with a descriptive message
            contextMock.Verify(m => m.Add(It.IsAny<object>()));

            //System.ArgumentNullException: 'Value cannot be null.'
            //Parameter name: model
        }
    }
}