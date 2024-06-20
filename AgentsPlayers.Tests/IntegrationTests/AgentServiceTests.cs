using AgentsPlayers.Domain.Entities;
using AgentsPlayers.Persistance;
using AgentsPlayers.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AgentsPlayers.Tests.IntegrationTests
{
    public class AgentServiceTests
    {
        private readonly IDbContextFactory<AgentsPlayersContext> _factory;
        private DbContextOptions<AgentsPlayersContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<AgentsPlayersContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        private IDbContextFactory<AgentsPlayersContext> GetDbContextFactoryAsync(DbContextOptions<AgentsPlayersContext> options)
        {
            var mockDbFactory = new Mock<IDbContextFactory<AgentsPlayersContext>>();
            mockDbFactory.Setup(f => f.CreateDbContext()).Returns(() => new AgentsPlayersContext(options));
            return mockDbFactory.Object;

        }
        [Fact]
        public async Task Save_ShouldAddAgent()
        {
            // Arrangeتجهيز الاختبار
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new AgentService(factory);
            var agent = new Agent { FullName = "Agent1", PhoneNumber = 092222222, EmailAddress = "aa", Experience = DateTime.Now };

            // Act استدعاء الدالة
            await service.Save(agent);

            // Assert تأكد
            using var context = new AgentsPlayersContext(options);
            var savedAgent = await context.Agents.FirstOrDefaultAsync(b => b.FullName == "Agent1");
            Assert.NotNull(savedAgent);
        }
        [Fact]
        public async Task Get_ShouldReturnAgentById()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new AgentService(factory);
            var agent = new Agent { FullName = "Agent1", PhoneNumber = 092222222, EmailAddress = "aa", Experience = DateTime.Now };
            await service.Save(agent);

            // Act
            var fetchedAgent = await service.Get(agent.Id);

            // Assert
            Assert.NotNull(fetchedAgent);
            Assert.Equal(agent.FullName, fetchedAgent.FullName);
        }
        [Fact]
        public async Task GetList_ShouldReturnAgentByName()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new AgentService(factory);
            await service.Save(new Agent { FullName = "Agent1", PhoneNumber = 092222222, EmailAddress = "aa", Experience = DateTime.Now });
            await service.Save(new Agent { FullName = "Agent2", PhoneNumber = 092222222, EmailAddress = "aa", Experience = DateTime.Now });

            // Act
            var agents = await service.GetList("Agent");

            // Assert
            Assert.Equal(2, agents.Count);
        }
        [Fact]
        public async Task GetAll_ShouldReturnAllAgents()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new AgentService(factory);
            await service.Save(new Agent { FullName = "Agent1", PhoneNumber = 092222222, EmailAddress = "aa", Experience = DateTime.Now });
            await service.Save(new Agent { FullName = "Agent2", PhoneNumber = 092222222, EmailAddress = "aa", Experience = DateTime.Now });

            // Act
            var agents = await service.GetAll();

            // Assert
            Assert.Equal(2, agents.Count);
        }
        [Fact]
        public async Task Delete_ShouldRemoveAgent()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new AgentService(factory);
            var agent = new Agent { FullName = "Agent2", PhoneNumber = 092222222, EmailAddress = "aa", Experience = DateTime.Now };
            await service.Save(agent);

            // Act
            await service.Delete(agent);

            // Assert
            using var context = new AgentsPlayersContext(options);
            var deletedAgent = await context.Agents.FindAsync(agent.Id);
            Assert.Null(deletedAgent);
        }
        [Fact]
        public async Task Update_ShouldModifyAgent()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new AgentService(factory);
            var agent = new Agent { FullName = "Agent2", PhoneNumber = 092222222, EmailAddress = "aa", Experience = DateTime.Now };
            await service.Save(agent);

            // Act
            agent.FullName = "Agent2";
            agent.PhoneNumber = 092222222;
            agent.EmailAddress = "aa";
            
          

            await service.Update(agent);

            // Assert
            using var context = new AgentsPlayersContext(options);
            var updatedAgent = await context.Agents.FindAsync(agent.Id);
            Assert.Equal("Agent2", updatedAgent.FullName);
            Assert.Equal(092222222, updatedAgent.PhoneNumber);
            Assert.Equal("aa", updatedAgent.EmailAddress);

           
        }
    }
    
}
