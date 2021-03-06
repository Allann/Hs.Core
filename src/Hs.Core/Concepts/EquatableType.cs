﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hs.Core.Concepts
{
    /// <summary>
    /// A base class for providing value object equality semantics.  A EquatableType Object does not have an identity and its value comes from its properties.
    /// </summary>
    /// <typeparam name="T">The specific type to provide value object equality semantics to.</typeparam>
    public abstract class EquatableType<T> : IEquatable<T> where T : EquatableType<T>
    {
        static IList<FieldInfo> Fields { get; set; } = new List<FieldInfo>();

        /// <summary>
        /// Checks for Equality between this instance and the obj
        /// </summary>
        /// <param name="obj">An istance of an object to check equality with</param>
        /// <returns>True if equal, false otherwise</returns>
        public override bool Equals(object? obj)
        {
            return obj is T typed && Equals(typed);
        }

        /// <summary>
        /// Gets a Hash Code to identify this instance
        /// </summary>
        /// <returns>Hashcode value</returns>
        public override int GetHashCode()
        {
            var fields = GetFields()
                .Select(field => field.GetValue(this))
                .Where(value => value != null)
                .ToList();

            fields.Add(GetType());

            return HashCodeHelper.Generate(fields.ToArray());
        }

        /// <summary>
        /// Checks for Equality between this instance and the Other
        /// </summary>
        /// <param name="other">Another instance of type T to check equality with</param>
        /// <returns>True if equal, false otherwise</returns>
        public virtual bool Equals(T? other)
        {
            if (other is null)
            {
                return false;
            }

            var t = GetType();
            var otherType = other.GetType();

            if (t != otherType)
            {
                return false;
            }

            var fields = GetFields();

            foreach (var field in fields)
            {
                var value1 = field.GetValue(other);
                var value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                    {
                        return false;
                    }
                }
                else if (!value1.Equals(value2))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Equates two objects to check that they are equal
        /// </summary>
        /// <param name="x">First EquatableType</param>
        /// <param name="y">Second value</param>
        /// <returns>True if the objects are equal, false is they are not</returns>
        public static bool operator ==(EquatableType<T> x, EquatableType<T> y)
        {
            return ReferenceEquals(x, y) || x.Equals(y);
        }

        /// <summary>
        /// Equates two objects to check that they are not equal
        /// </summary>
        /// <param name="x">First EquatableType</param>
        /// <param name="y">Second value</param>
        /// <returns>True if the objects are not equal, false is they are</returns>
        public static bool operator !=(EquatableType<T> x, EquatableType<T> y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Converts this EquatableType into a string representation.
        /// </summary>
        /// <returns>A string containing each property name and its corresponding value</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("{[Type: " + GetType() + "]");
            foreach (var field in GetFields())
            {
                sb.AppendFormat(@"{{ {0} : {1} }}", RemoveBackingAutoBackingFieldPropertyName(field.Name), field.GetValue(this) ?? "[NULL]");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        IEnumerable<FieldInfo> GetFields()
        {
            if (!Fields.Any())
            {
                Fields = new List<FieldInfo>(BuildFieldCollection());
            }

            return Fields;
        }

        IEnumerable<FieldInfo> BuildFieldCollection()
        {
            var t = typeof(T);
            var fields = new List<FieldInfo>();
            if (t == null)
            {
                return fields;
            }

            while (t != typeof(object))
            {
                var typeInfo = t.GetTypeInfo();

                fields.AddRange(typeInfo.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
                var fieldInfoCache = typeInfo.GetField("_fields");
                if (fieldInfoCache != null)
                {
                    fields.Remove(fieldInfoCache);
                }
                t = typeInfo.BaseType;
                if (t == null)
                {
                    break;
                }
            }
            return fields;
        }

        string RemoveBackingAutoBackingFieldPropertyName(string fieldName)
        {
            var field = fieldName.TrimStart('<');
            return field.Replace(@">k__BackingField", string.Empty);
        }
    }
}
