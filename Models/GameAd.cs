namespace wepAPI_denemeler.Models
{
    public class GameAd : BaseEntity
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;

        public User? User { get; set; }
        public Game? Game { get; set; }
    }
}