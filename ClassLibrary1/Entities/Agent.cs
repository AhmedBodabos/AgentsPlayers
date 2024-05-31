using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentsPlayers.Domain.Entities
{

    public class Agent
    {
        public int Id { get; set; }
        [MaxLength(60)]
        public string FullName { get; set; }
        [MaxLength(24)]
        public double PhoneNumber { get; set; }
        [MaxLength(60)]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public DateTime Experience { get; set; }
        public List<Player> RepresentedPlayers { get; set; }


    }
}
