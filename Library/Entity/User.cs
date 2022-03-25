using Library.Entity.EntittyInterface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Library.Entity
{
    public class User : BaseEntity ,IEntity
    {
        [JsonIgnore]
        [Key]
        [Column("UserId")]
        public override long Id { get => base.Id; set => base.Id = value; }

        [Required(ErrorMessage = "Campo Name obrigatório")]
        public string Name { get; set; }
        
        public bool IsAdmin { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
    }
}
