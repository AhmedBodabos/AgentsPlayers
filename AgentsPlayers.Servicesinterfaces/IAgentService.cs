using AgentsPlayers.Domain.Entities;

namespace AgentsPlayers.ServicesInterfaces
{
    public interface IAgentService
    {
        Task Delete(Agent agent);
       Task <Agent> Get(int id);
       Task <List<Agent>> GetList(string FullName);
        Task Save(Agent agent);
        Task Update(Agent agent);
        Task<List<Agent>> GetAll();
        Task AddPlayerToAgent(Agent agent, Player player);
        Task RemovePlayerFromAgent(Agent agent, Player player);
    }
}