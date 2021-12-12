using System.ComponentModel.DataAnnotations;

namespace Library.Entity
{
    public class BaseEntity
    {
        [Key]
        public virtual long Id { get; set; }
    }
}
