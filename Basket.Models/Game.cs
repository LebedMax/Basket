using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basket.Models
{
    [Table("dbo.Games")]
    public class Game
    {
        [Column("GameID")]
        [Required]
        public int GameId { get; set; }
       
        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Category")]
        public string Category { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }
    }
}
