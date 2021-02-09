using System;
using System.Collections.Generic;

namespace Hs.Core.Concepts
{
    /// <summary>
    ///     Expresses a Concept as a another type, usually a primitive such as Guid, Int or String
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConceptAs<T>
            : EquatableType<ConceptAs<T>>, IComparable<ConceptAs<T>>, IComparable
        where T : notnull
    {
        #region Constructors

        public ConceptAs(T value)
        {
            Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     The underlying primitive value of this concept
        /// </summary>
        public T Value { get; }

        /// <summary>
        ///     The underlying primitive type of this concept.
        /// </summary>
        public static Type UnderlyingType => typeof(T);

        #endregion

        public static implicit operator T(ConceptAs<T> value) => value.Value;

        public override bool Equals(object? obj) => obj is ConceptAs<T> typed && Equals(typed);

        public static bool operator ==(ConceptAs<T> a, ConceptAs<T> b)
        {
            return a is null && b is null || !(a is null) && !(b is null) && a.Equals(b);
        }

        public static bool operator !=(ConceptAs<T> a, ConceptAs<T> b) => !(a == b);

        public static bool operator >(ConceptAs<T> a, ConceptAs<T> b) => a.CompareTo(b) == 1;

        public static bool operator <(ConceptAs<T> a, ConceptAs<T> b) => a.CompareTo(b) == -1;

        public static bool operator >=(ConceptAs<T> a, ConceptAs<T> b) => a.CompareTo(b) > -1;

        public static bool operator <=(ConceptAs<T> a, ConceptAs<T> b) => a.CompareTo(b) < 1;

        public override int GetHashCode() => HashCodeHelper.Generate(typeof(T), Value);

        public bool IsEmpty() => Value is null || (Value is string value ? value == string.Empty : Value.Equals(default(T)));

        public virtual int CompareTo(ConceptAs<T>? other) => other is null ? 1 : Comparer<T>.Default.Compare(Value, other.Value);

        public virtual int CompareTo(object? obj)
        {
            var other = obj as ConceptAs<T>;
            return CompareTo(other);
        }
    }

}
