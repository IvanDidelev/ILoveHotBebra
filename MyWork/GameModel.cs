using Microsoft.EntityFrameworkCore;

namespace Bebra
{
    public class GameModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Company { get; set; }
        public string? Version { get; set; }
        public int Cost { get; set; }
    }

    public class AutoModelContext : DbContext
    {
        public DbSet<GameModel> Games { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Привязка контекста к существующей бд
            optionsBuilder.UseMySql("Server=MYSQL8001.site4now.net;Database=db_a8ee9f_games;Uid=a8ee9f_games;Pwd=dsafsf243",
            new MySqlServerVersion(new Version(5,0)));
        }
    }
}
