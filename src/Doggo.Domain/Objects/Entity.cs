using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Doggo.Domain.Objects
{
    public abstract class Entity<TKey, TEntity> where TEntity : class
    {
        public TKey Id { get; protected set; }
        public Guid UniqueId { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }

        protected Entity()
        {
            UniqueId = Guid.NewGuid();
        }

        public abstract bool IsValid();

        /// <summary>
        /// Get all properties used to be internally compared or copied by Entity.Equals and Entity.CopyDataFrom.
        /// </summary>
        /// <returns>Collection of PropertyInfo</returns>
        protected abstract IEnumerable<PropertyInfo> GetEqualityProperties();

        /// <summary>
        /// Compare all properties listed by GetEqualityProperties with another class of the same type.
        /// Do not override default Equals.
        /// </summary>
        /// <param name="source">Type T</param>
        /// <returns>True: If Equality Properties are Equals</returns>
        public virtual bool Equals([AllowNull] TEntity other)
        {
            if (other is null)
                return false;

            var equalityProperties = GetEqualityProperties();
            object thisValue;
            object otherValue;

            foreach (var property in equalityProperties)
            {
                thisValue = property.GetValue(this, null);
                otherValue = other.GetType()
                    .GetProperty(property.Name)
                    .GetValue(other, null);

                if (!Equals(thisValue, otherValue))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Copy value of all properties listed by GetEqualityProperties, if not equals, from another class of the same type.
        /// </summary>
        /// <param name="source">Type T</param>
        /// <returns>True: If at least one value were copied</returns>
        public virtual bool CopyDataFrom(TEntity source)
        {
            if (source is null)
                return false;

            if (Equals(source))
                return false;

            var targetProperties = GetEqualityProperties();
            object targetValue;
            object sourceValue;

            foreach (var property in targetProperties)
            {
                targetValue = property.GetValue(this, null);
                sourceValue = source.GetType()
                    .GetProperty(property.Name)
                    .GetValue(source, null);

                if (!Equals(targetValue, sourceValue) && property.CanWrite)
                    property.SetValue(this, sourceValue);
            }

            return Equals(source);
        }

        /// <summary>
        /// Generate hash based on all properties listed by GetEqualityProperties.
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            var comparableProperties = GetEqualityProperties();
            var hash = new HashCode();

            foreach (var property in comparableProperties)
                hash.Add(property.GetValue(this, null));

            return hash.ToHashCode();
        }
    }
}
