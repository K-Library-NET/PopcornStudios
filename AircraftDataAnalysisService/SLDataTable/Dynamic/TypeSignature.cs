using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Data
{
    /// <summary>
    /// Type Signature Class
    /// </summary>
    internal class TypeSignature : IEquatable<TypeSignature>
    {
        #region Private Members
        private readonly int hashCode; 
        #endregion

        #region public TypeSignature(IEnumerable<SLDataColumn> columns)
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSignature"/> class.
        /// </summary>
        /// <param name="columns">The columns.</param>
        public TypeSignature(IEnumerable<SLDataColumn> columns)
        {
            this.hashCode = 0;
            foreach (var column in columns.OrderBy(p => p.ColumnName))
            {
                this.hashCode ^= column.ColumnName.GetHashCode() ^ column.DataType.GetHashCode();
            }
        } 
        #endregion

        #region public override bool Equals(object obj)
        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return ((obj is TypeSignature) && this.Equals((TypeSignature)obj));
        } 
        #endregion

        #region public bool Equals(TypeSignature other)
        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(TypeSignature other)
        {
            return this.hashCode.Equals(other.hashCode);
        } 
        #endregion

        #region public override int GetHashCode()
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.hashCode;
        } 
        #endregion
    }
}
