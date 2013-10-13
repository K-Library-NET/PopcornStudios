using System;

namespace Telerik.Data
{
    /// <summary>
    /// Data Column Class
    /// </summary>
    public class SLDataColumn
    {
        #region public SLDataColumn()
        /// <summary>
        /// Initializes a new instance of the <see cref="SLDataColumn"/> class.
        /// </summary>
        public SLDataColumn()
        {
            this.DataType = typeof(object);
        } 
        #endregion

        #region public Type DataType { get; set; } 
        public Type DataType { get; set; } 
        #endregion

        #region public string ColumnName { get; set; }
        public string ColumnName { get; set; }
        #endregion
    }
}
