using System.ComponentModel.DataAnnotations.Schema;

namespace ReactAppBackend.Database
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            CreatedTime = DateTime.UtcNow;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }

        public DateTime CreatedTime { get; set; }

        public DateTime? ArchivedTime { get; set; }
    }
}
