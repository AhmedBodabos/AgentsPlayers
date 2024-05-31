using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentsPlayers.Domain.Entities
{

    public class Player
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public double Height { get; set; }
        public int Weight { get; set; }
        public double MarketValue { get; set; }
        public string PreferredFoot { get; set; }
        public DateTime ContractExpirationDate { get; set; }
        public string CurrentClub { get; set; }
        public Agent Agent { get; set; }
        public List<string> AwardsAndAchievements { get; set; }
        public string HealthStatus { get; set; }
        public List<string> Languages { get; set; }
    }
}

