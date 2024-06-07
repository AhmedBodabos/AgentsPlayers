using AgentsPlayers.Domain.Entities;
using AgentsPlayers.Persistance;
using AgentsPlayers.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AgentsPlayers.Services
{
    public class PlayerService(IDbContextFactory<AgentsPlayersContext> dbContextFactory) : IPlayerService
    {
        private readonly IDbContextFactory<AgentsPlayersContext> _contextFactory = dbContextFactory;

        public async Task Save(Player player)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmp = db.Players.FirstOrDefault(x => x.Id == player.Id);
            if (tmp == null)
            {
                db.Players.Add(player);
                await db.SaveChangesAsync();
            }

        }

        public async Task Update(Player player)
        {
            using var db = (_contextFactory.CreateDbContext());

            var tmp = db.Players.FirstOrDefault(x => x.Id == player.Id);
            if (tmp != null)
            {
                tmp.FullName = player.FullName;
                tmp.DateOfBirth = player.DateOfBirth;
                tmp.Nationality = player.Nationality;
                tmp.Position = player.Position;
                tmp.Height = player.Height;
                tmp.Weight = player.Weight;
                tmp.MarketValue = player.MarketValue;
                tmp.PreferredFoot = player.PreferredFoot;
                tmp.ContractExpirationDate = player.ContractExpirationDate;
                tmp.CurrentClub = player.CurrentClub;
                tmp.Agent = player.Agent;
                tmp.AwardsAndAchievements = player.AwardsAndAchievements;
                tmp.HealthStatus = player.HealthStatus;
                tmp.Languages = player.Languages;


                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(Player player)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Players.FirstOrDefault(x => x.Id == player.Id);
            if (tmp != null)
            {
                db.Players.Remove(tmp);
                await db.SaveChangesAsync();
            }
        }

        public async Task <Player> Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var player =await db.Players.FirstOrDefaultAsync(x => x.Id == id);
            return player;
        }


        public async Task <List<Player>> GetList(string FullName)
        {
            using var db = _contextFactory.CreateDbContext();

            var players = await db.Players.Where(x => x.FullName.Contains(FullName)).ToListAsync();
            return [.. players];
        }
    }
}
