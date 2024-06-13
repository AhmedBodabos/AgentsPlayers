using AgentsPlayers.Domain.Entities;

namespace AgentsPlayers.ServicesInterfaces
{
    public interface IPlayerService
    {
       Task  Delete(Player player);
       Task <Player> Get(int id);
        Task <List<Player>> GetList(string FullName);
       Task  Save(Player player);
       Task  Update(Player player);
        Task<List<Player>> GetAll();
    }
}