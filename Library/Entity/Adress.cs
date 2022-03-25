using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entity
{
    public class Adress : BaseEntity
    {
        [Key]
        [Column("AdressId")]
        public override long Id { get => base.Id; set => base.Id = value; }

        public string City { get; set; }

    }
}
