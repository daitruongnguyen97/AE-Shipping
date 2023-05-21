
using System.ComponentModel.DataAnnotations;

namespace Domain.Common;

public abstract class BaseEntity : IBaseEntity
{
    [Key]
    public Guid Id { get; set; }       
}

public interface IBaseEntity
{
    public Guid Id { get; set; }
}