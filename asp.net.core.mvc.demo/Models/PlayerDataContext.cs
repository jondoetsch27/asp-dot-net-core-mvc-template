using System;
using Microsoft.EntityFrameworkCore;

namespace asp.net.core.mvc.demo.Models
{
    public class PlayerDataContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public PlayerDataContext(DbContextOptions<PlayerDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
