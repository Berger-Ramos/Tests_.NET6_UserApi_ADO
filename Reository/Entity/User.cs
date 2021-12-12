using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entity
{
    public class User : BaseEntity
    {
        [Key]
        [Column("UserId")]
        public override long Id { get => base.Id; set => base.Id = value; }

        [Required(ErrorMessage = "Campo Name obrigatório")]
        public string Name { get; set; }
    }
}
