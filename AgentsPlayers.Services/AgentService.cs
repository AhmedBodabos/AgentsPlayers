using AgentsPlayers.Domain.Entities;
using AgentsPlayers.Persistance;
using AgentsPlayers.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AgentsPlayers.Services
{
    public class AgentService(IDbContextFactory<AgentsPlayersContext> contextFactory) : IAgentService
    {
        private readonly IDbContextFactory<AgentsPlayersContext> _contextFactory = contextFactory;

        public void Save(Agent agent)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmp = db.Agents.FirstOrDefault(x => x.Id == agent.Id);
            if (tmp == null)
            {
                db.Agents.Add(agent);
                db.SaveChanges();
            }

        }

        public void Update(Agent agent)
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


                db.SaveChanges();
            }
        }

        public void Delete(Agent agent)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Agents.FirstOrDefault(x => x.Id == agent.Id);
            if (tmp != null)
            {
                db.Agents.Remove(tmp);
                db.SaveChanges();
            }
        }

        public Agent Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var agent = db.Agents.FirstOrDefault(x => x.Id == id);
            return agent;
        }


        public List<Agent> GetList(string FullName)
        {
            using var db = _contextFactory.CreateDbContext();

            var agents = db.Agents.Where(x => x.FullName.Contains(FullName));
            return [.. agents];
        }
    }
}
