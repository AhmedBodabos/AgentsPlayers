using AgentsPlayers.Domain.Entities;

namespace AgentsPlayers.ServicesInterfaces
{
    public interface IPlayerService
    {
       Task  Delete(Player player);
       Task <Player> Get(int id);
        Task <List<Player>> GetList(string title);
       Task  Save(Player book);
       Task  Update(Player book);
        Task<List<Player>> GetAll();
    }
}