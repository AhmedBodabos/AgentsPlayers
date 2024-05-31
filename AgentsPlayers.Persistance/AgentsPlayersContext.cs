using AgentsPlayers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AgentsPlayers.Persistance
{
    public class AgentsPlayersContext : DbContext
    {
        public AgentsPlayersContext(DbContextOptions<AgentsPlayersContext> options): 
            base(options)
        { 

        }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Player> Players { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>()
                .Property(x => x.FullName)
                .HasMaxLength(200)
                .IsRequired(true);

            modelBuilder.Entity<Player>()
               .Property(x => x.Nationality)
               .HasMaxLength(30)
               .IsRequired(true);
               
               modelBuilder.Entity<Player>()
               .Property(x => x.Position)
               .HasMaxLength(30)
               .IsRequired(true);

            modelBuilder.Entity<Player>()
            .Property(x => x.Height)
            .HasMaxLength(8)
            .IsRequired(true);

            modelBuilder.Entity<Player>()
          .Property(x => x.Weight)
          .HasMaxLength(8)
          .IsRequired(true);

            modelBuilder.Entity<Player>()
         .Property(x => x.MarketValue)
         .HasMaxLength(14)
         .IsRequired(true);

            modelBuilder.Entity<Player>()
        .Property(x => x.PreferredFoot)
        .HasMaxLength(14)
        .IsRequired(true);

            modelBuilder.Entity<Player>()
        .Property(x => x.CurrentClub)
        .HasMaxLength(14)
        .IsRequired(true);
        }
    }
}
