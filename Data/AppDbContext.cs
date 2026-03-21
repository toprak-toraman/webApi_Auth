using Microsoft.EntityFrameworkCore;
using wepAPI_denemeler.Models;

namespace wepAPI_denemeler.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tüm tabloların burada tanımlı olduğundan emin ol
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<FavGame> FavGames { get; set; }
        public DbSet<GameAd> GameAds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PostgreSQL uyumu için tüm tablo isimlerini küçük harfe zorlar
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (!string.IsNullOrEmpty(tableName))
                {
                    entity.SetTableName(tableName.ToLower());
                }
            }

            // İlişkileri (Foreign Key) burada daha detaylı yapılandırabilirsin
            modelBuilder.Entity<FavGame>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<FavGame>()
                .HasOne(f => f.Game)
                .WithMany()
                .HasForeignKey(f => f.GameId);
        }
    }
}