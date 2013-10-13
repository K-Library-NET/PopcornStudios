using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Telerik.Data
{
    /// <summary>
    /// This class will create a bindable collection from and ObservableCollection of SerializableDictionary
    /// </summary>
    public class SLDataTable : IEnumerable, INotifyCollectionChanged
    {

        #region Private Members
        private IList<SLDataColumn> columns;
        private ObservableCollection<SLDataRow> rows;
        private IList internalView;
        private Type elementType; 
        #endregion

        #region public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged; 
        #endregion

        #region SLDataTable()
        /// <summary>
        /// Initializes a new instance of the <see cref="SLDataTable"/> class.
        /// </summary>
        public SLDataTable()
        {
            // Intentionally left blank
        }
        #endregion
            
        #region public SLDataTable(IEnumerable<Dictionary<string, object>> source)
        /// <summary>
        /// Initializes a new instance of the <see cref="SLDataTable"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public SLDataTable(IEnumerable<Dictionary<string, object>> source)
        {
            if (source != null)
            {
                var firstRow = source.FirstOrDefault();

                if (firstRow != null)
                {
                    foreach (var column in firstRow)
                    {
                        // Default to string for all nulls returned
                        Type columnDataType = Type.GetType("System.String");

                        // Find first row where column value is not null
                        var colNotNull = source.AsEnumerable().Select(col => col[column.Key]).Where(col => col != null).Take(1);
                        if (colNotNull.Count() > 0)
                        {
                            // We have a value so let's override the string data type from above
                            // Also, we need to make it a nullable type if the type supports it
                            columnDataType = GetNullableType(colNotNull.ElementAt(0).GetType());
                        }

                        Columns.Add(new SLDataColumn()
                        {
                            ColumnName = column.Key,
                            DataType = columnDataType
                        });
                    }

                    foreach (var item in source)
                    {
                        var row = new SLDataRow(this);

                        foreach (var key in item)
                        {
                            row[key.Key] = key.Value;
                        }
                        Rows.Add(row);
                    }
                }
            }
        }  
        #endregion

        #region public IList<SLDataColumn> Columns
        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <value>The columns.</value>
        public IList<SLDataColumn> Columns
        {
            get
            {
                if (columns == null)
                {
                    columns = new List<SLDataColumn>();
                }

                return columns;
            }
        } 
        #endregion

        #region public IList<SLDataRow> Rows
        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public IList<SLDataRow> Rows
        {
            get
            {
                if (this.rows == null)
                {
                    this.rows = new ObservableCollection<SLDataRow>();
                    this.rows.CollectionChanged += OnRowsCollectionChanged;
                }

                return rows;
            }
        } 
        #endregion

        #region public SLDataRow NewRow()
        /// <summary>
        /// News the row.
        /// </summary>
        /// <returns></returns>
        public SLDataRow NewRow()
        {
            return new SLDataRow(this);
        } 
        #endregion

        #region private void OnRowsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        /// <summary>
        /// Called when [rows collection changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnRowsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.InternalView.Insert(e.NewStartingIndex, ((SLDataRow)e.NewItems[0]).RowObject);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.InternalView.RemoveAt(e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    this.InternalView.Remove(((SLDataRow)e.OldItems[0]).RowObject);
                    this.InternalView.Insert(e.NewStartingIndex, ((SLDataRow)e.NewItems[0]).RowObject);
                    break;
                case NotifyCollectionChangedAction.Reset:
                default:
                    this.InternalView.Clear();
                    this.Rows.Select(r => r.RowObject).ToList().ForEach(o => this.InternalView.Add(o));
                    break;
            }
        } 
        #endregion

        #region private IList InternalView
        /// <summary>
        /// Gets the internal view.
        /// </summary>
        /// <value>The internal view.</value>
        private IList InternalView
        {
            get
            {
                if (this.internalView == null)
                {
                    this.CreateInternalView();
                }

                return this.internalView;
            }
        } 
        #endregion

        #region private void CreateInternalView()
        /// <summary>
        /// Creates the internal view.
        /// </summary>
        private void CreateInternalView()
        {
            this.internalView = (IList)Activator.CreateInstance(typeof(ObservableCollection<>).MakeGenericType(this.ElementType));
            ((INotifyCollectionChanged)internalView).CollectionChanged += (s, e) => this.OnCollectionChanged(e);
        } 
        #endregion

        #region internal Type ElementType
        /// <summary>
        /// Gets the type of the element.
        /// </summary>
        /// <value>The type of the element.</value>
        internal Type ElementType
        {
            get
            {
                if (this.elementType == null)
                {
                    this.InitializeElementType();
                }

                return this.elementType;
            }
        } 
        #endregion

        #region private void InitializeElementType()
        /// <summary>
        /// Initializes the type of the element.
        /// </summary>
        private void InitializeElementType()
        {
            this.Seal();
            this.elementType = DynamicObjectBuilder.GetDynamicObjectBuilderType(this.Columns);
        } 
        #endregion

        #region private void Seal()
        /// <summary>
        /// Seals this instance.
        /// </summary>
        private void Seal()
        {
            this.columns = new ReadOnlyCollection<SLDataColumn>(this.Columns);
        } 
        #endregion

        #region public IEnumerator GetEnumerator()
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.InternalView.GetEnumerator();
        } 
        #endregion

        #region public IList ToList()
        /// <summary>
        /// Toes the list.
        /// </summary>
        /// <returns></returns>
        public IList ToList()
        {
            return this.InternalView;
        } 
        #endregion

        #region protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        /// <summary>
        /// Raises the <see cref="E:CollectionChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = this.CollectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        } 
        #endregion

        #region private Type GetNullableType(Type sourceType)
        /// <summary>
        /// Gets the type of the nullable.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <returns></returns>
        private Type GetNullableType(Type sourceType)
        {
            if (sourceType == null || sourceType == typeof(void))
            {
                // This should never be returned in this project
                return null;
            }

            // if sourceType is already a nullable type, then just return that type i.e. string
            if (!sourceType.IsValueType || (sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                return sourceType;
            }
            else
            {
                return typeof(Nullable<>).MakeGenericType(sourceType);
            }
        } 
        #endregion
    }
}