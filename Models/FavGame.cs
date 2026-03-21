namespace wepAPI_denemeler.Models
{
    public class FavGame : BaseEntity
    {
        public int UserId { get; set; }
        public int GameId { get; set; }

        // Navigation Properties (İlişkileri EF Core'a tanıtmak için)
        public User? User { get; set; }
        public Game? Game { get; set; }
    }
}