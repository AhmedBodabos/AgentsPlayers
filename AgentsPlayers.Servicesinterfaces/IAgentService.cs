using AgentsPlayers.Domain.Entities;

namespace AgentsPlayers.ServicesInterfaces
{
    public interface IAgentService
    {
        Task Delete(Agent agentr);
        Task <Agent> Get(int id);
        Task <List<Agent>> GetList(string FullName);
        Task Save(Agent agent);
        Task Update(Agent agent);
    }
}