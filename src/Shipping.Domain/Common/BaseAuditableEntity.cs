using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
