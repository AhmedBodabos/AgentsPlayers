using AgentsPlayers.Domain.Entities;

namespace AgentsPlayers.ServicesInterfaces
{
    public interface IPlayerService
    {
        void Delete(Player player);
        Player Get(int id);
        List<Player> GetList(string title);
        void Save(Player book);
        void Update(Player book);
    }
}