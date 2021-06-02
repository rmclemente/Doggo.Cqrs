using System;

namespace Doggo.Domain.Objects
{
    public abstract class AuditableEntity<TKey, TEntity> : Entity<TKey, TEntity> where TEntity : class
    {
        public DateTime? CreatedAt { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public string UpdatedBy { get; protected set; }
    }
}
