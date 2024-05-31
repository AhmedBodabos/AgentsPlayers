using AgentsPlayers.Domain.Entities;
using AgentsPlayers.Persistance;
using AgentsPlayers.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AgentsPlayers.Services
{
    public class PlayerService(IDbContextFactory<AgentsPlayersContext> dbContextFactory) : IPlayerService
    {
        private readonly IDbContextFactory<AgentsPlayersContext> _contextFactory = dbContextFactory;

        public void Save(Player player)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmp = db.Players.FirstOrDefault(x => x.Id == player.Id);
            if (tmp == null)
            {
                db.Players.Add(player);
                db.SaveChanges();
            }

        }

        public void Update(Player player)
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


                db.SaveChanges();
            }
        }

        public void Delete(Player player)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Players.FirstOrDefault(x => x.Id == player.Id);
            if (tmp != null)
            {
                db.Players.Remove(tmp);
                db.SaveChanges();
            }
        }

        public Player Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var player = db.Players.FirstOrDefault(x => x.Id == id);
            return player;
        }


        public List<Player> GetList(string FullName)
        {
            using var db = _contextFactory.CreateDbContext();

            var players = db.Players.Where(x => x.FullName.Contains(FullName));
            return [.. players];
        }
    }
}
