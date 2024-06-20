using AgentsPlayers.Domain.Entities;
using AgentsPlayers.Persistance;
using AgentsPlayers.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AgentsPlayers.Tests.IntegrationTests
{
    public class PlayerServiceTests
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
        public async Task Save_ShouldAddPlayer()
        {
            // Arrangeتجهيز الاختبار
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerService(factory);
            var player = new Player { FullName = "Player1", Nationality = "England", Position = "ST" , Height  = 1.80 , Weight = 70 , MarketValue = 5000000, PreferredFoot = "Right" , CurrentClub  = "Everton" , HealthStatus  = "good"};

            // Act استدعاء الدالة
            await service.Save(player);

            // Assert تأكد
            using var context = new AgentsPlayersContext(options);
            var savedPlayer = await context.Players.FirstOrDefaultAsync(b => b.FullName == "Player1");
            Assert.NotNull(savedPlayer);
        }
        [Fact]
        public async Task Get_ShouldReturnPlayerById()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerService(factory);
            var player = new Player { FullName = "Player1", Nationality = "England", Position = "ST", Height = 1.80, Weight = 70, MarketValue = 5000000, PreferredFoot = "Right", CurrentClub = "Everton", HealthStatus = "good" };
            await service.Save(player);

            // Act
            var fetchedPlayer = await service.Get(player.Id);

            // Assert
            Assert.NotNull(fetchedPlayer);
            Assert.Equal(player.FullName, fetchedPlayer.FullName);
        }
        [Fact]
        public async Task GetList_ShouldReturnPlayerByName()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerService(factory);
            await service.Save(new Player { FullName = "Player1", Nationality = "England", Position = "ST", Height = 1.80, Weight = 70, MarketValue = 5000000, PreferredFoot = "Right", CurrentClub = "Everton", HealthStatus = "good" });
            await service.Save(new Player { FullName = "Player2", Nationality = "England", Position = "ST", Height = 1.80, Weight = 70, MarketValue = 5000000, PreferredFoot = "Right", CurrentClub = "Everton", HealthStatus = "good" });

            // Act
            var players = await service.GetList("Player");

            // Assert
            Assert.Equal(2, players.Count);
        }
        [Fact]
        public async Task GetAll_ShouldReturnAllPlayers()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerService(factory);
            await service.Save(new Player { FullName = "Player1", Nationality = "England", Position = "ST", Height = 1.80, Weight = 70, MarketValue = 5000000, PreferredFoot = "Right", CurrentClub = "Everton", HealthStatus = "good" });
            await service.Save(new Player { FullName = "Player2", Nationality = "England", Position = "ST", Height = 1.80, Weight = 70, MarketValue = 5000000, PreferredFoot = "Right", CurrentClub = "Everton", HealthStatus = "good" });

            // Act
            var players = await service.GetAll();

            // Assert
            Assert.Equal(2, players.Count);
        }
        [Fact]
        public async Task Delete_ShouldRemovePlayer()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerService(factory);
            var player = new Player { FullName = "Player1", Nationality = "England", Position = "ST", Height = 1.80, Weight = 70, MarketValue = 5000000, PreferredFoot = "Right", CurrentClub = "Everton", HealthStatus = "good", DateOfBirth = DateTime.Now, ContractExpirationDate = DateTime.Now, Languages = { "d", "d" } , AwardsAndAchievements = { "d","d"} };
            await service.Save(player);

            // Act
            await service.Delete(player);

            // Assert
            using var context = new AgentsPlayersContext(options);
            var deletedPlayer = await context.Players.FindAsync(player.Id);
            Assert.Null(deletedPlayer);
        }
        [Fact]
        public async Task Update_ShouldModifyPlayer()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new PlayerService(factory);
            var player = new Player { FullName = "Player1", Nationality = "England", Position = "ST", Height = 1.80, Weight = 70, MarketValue = 5000000, PreferredFoot = "Right", CurrentClub = "Everton", HealthStatus = "good" };
            await service.Save(player);

            // Act
            player.FullName = "Updated Player";
            player.Nationality = "Libya";
            player.Position = "LW";
            player.Height = 1.79;
            player.Weight = 90;
            player.MarketValue = 7500000;
            player.PreferredFoot = "Left";
            player.CurrentClub = "Aston Villa";
            player.HealthStatus = "Bad";

            await service.Update(player);

            // Assert
            using var context = new AgentsPlayersContext(options);
            var updatedPlayer = await context.Players.FindAsync(player.Id);
            Assert.Equal("Updated Player", updatedPlayer.FullName);
            Assert.Equal("Libya", updatedPlayer.Nationality);
            Assert.Equal("LW", updatedPlayer.Position);
            Assert.Equal(1.79, updatedPlayer.Height);
            Assert.Equal(90, updatedPlayer.Weight);
            Assert.Equal(7500000, updatedPlayer.MarketValue);
            Assert.Equal("Left", updatedPlayer.PreferredFoot);
            Assert.Equal("Aston Villa", updatedPlayer.CurrentClub);
            Assert.Equal("Bad", updatedPlayer.HealthStatus);
            
        }
    }
}

