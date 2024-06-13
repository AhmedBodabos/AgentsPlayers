using AgentsPlayers.Domain.Entities;
using AgentsPlayers.Persistance;
using AgentsPlayers.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AgentsPlayers.Services
{
    public class AgentService(IDbContextFactory<AgentsPlayersContext> contextFactory) : IAgentService
    {
        private readonly IDbContextFactory<AgentsPlayersContext> _contextFactory = contextFactory;

        public async Task Save(Agent agent)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmp = db.Agents.FirstOrDefault(x => x.Id == agent.Id);
            if (tmp == null)
            {
                db.Agents.Add(agent);
                await db.SaveChangesAsync();
            }

        }

        public async Task Update(Agent agent)
        {
            using var db = (_contextFactory.CreateDbContext());

            var tmp = db.Agents.FirstOrDefault(x => x.Id == agent.Id);
            if (tmp != null)
            {
                tmp.FullName = agent.FullName;
                tmp.PhoneNumber = agent.PhoneNumber;
                tmp.EmailAddress = agent.EmailAddress;
                tmp.Experience = agent.Experience;
                tmp.RepresentedPlayers = agent.RepresentedPlayers;


              await db.SaveChangesAsync();
            }
        }

        public async Task Delete(Agent agent)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Agents.FirstOrDefault(x => x.Id == agent.Id);
            if (tmp != null)
            {
                db.Agents.Remove(tmp);
               await db.SaveChangesAsync();
            }
        }

        public async Task <Agent> Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var agent = await db.Agents.FirstOrDefaultAsync(x => x.Id == id);
            return agent;
        }


        public async Task<List<Agent>> GetList(string FullName)
        {
            using var db = _contextFactory.CreateDbContext();

            var agents = db.Agents.Where(x => x.FullName.Contains(FullName));
            return [..await agents.ToListAsync()];
        }
        public async Task<List<Agent>> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return [.. await db.Agents.Include(x => x.RepresentedPlayers).ToListAsync()];
        }
        public async Task AddPlayerToAgent(Agent agent, Player player)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmpAgent = db.Agents.Include(x => x.RepresentedPlayers).FirstOrDefault(x => x.Id == agent.Id);
            if (tmpAgent != null)
            {
                var tmpPlayer = db.Players.FirstOrDefault(x => x.Id == player.Id);
                if (tmpPlayer != null)
                {
                    tmpAgent.RepresentedPlayers.Add(tmpPlayer);
                }
                else
                {
                    db.Players.Add(player);
                    await db.SaveChangesAsync();
                    tmpAgent.RepresentedPlayers.Add(player);
                }
                await db.SaveChangesAsync();
            }
        }

        public async Task RemovePlayerFromAgent(Agent agent , Player player)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmpAgent = db.Agents.Include(x => x.RepresentedPlayers).FirstOrDefault(x => x.Id == agent.Id);
            if (tmpAgent != null)
            {
                var agentPlayer = tmpAgent.RepresentedPlayers.FirstOrDefault(x => x.Id == player.Id);
                if (agentPlayer != null)
                {
                    tmpAgent.RepresentedPlayers.Remove(agentPlayer);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
