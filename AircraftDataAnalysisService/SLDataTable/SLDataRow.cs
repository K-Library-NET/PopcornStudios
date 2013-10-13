using System;

namespace Telerik.Data
{
    /// <summary>
    /// Data Row Class
    /// </summary>
    public class SLDataRow
    {
        #region Private Members
        private readonly SLDataTable owner;
        private DynamicObject rowObject; 
        #endregion

        #region protected internal SLDataRow(SLDataTable owner)
        /// <summary>
        /// Initializes a new instance of the <see cref="SLDataRow"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        protected internal SLDataRow(SLDataTable owner)
        {
            this.owner = owner;
        } 
        #endregion

        #region public object this[string columnName]
        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified column name.
        /// </summary>
        /// <value></value>
        public object this[string columnName]
        {
            get
            {
                return this.RowObject.GetValue<object>(columnName);
            }
            set
            {
                this.RowObject.SetValue(columnName, value);
            }
        } 
        #endregion

        #region internal DynamicObject RowObject
        /// <summary>
        /// Gets the row object.
        /// </summary>
        /// <value>The row object.</value>
        internal DynamicObject RowObject
        {
            get
            {
                this.EnsureRowObject();
                return this.rowObject;
            }
        } 
        #endregion

        #region private void EnsureRowObject()
        /// <summary>
        /// Ensures the row object.
        /// </summary>
        private void EnsureRowObject()
        {
            if (this.rowObject == null)
            {
                this.rowObject = (DynamicObject)Activator.CreateInstance(this.owner.ElementType);
            }
        } 
        #endregion
    }
}
