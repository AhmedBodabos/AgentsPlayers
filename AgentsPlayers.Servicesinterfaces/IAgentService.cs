using AgentsPlayers.Domain.Entities;

namespace AgentsPlayers.ServicesInterfaces
{
    public interface IAgentService
    {
        void Delete(Agent agentr);
        Agent Get(int id);
        List<Agent> GetList(string FullName);
        void Save(Agent agent);
        void Update(Agent agent);
    }
}