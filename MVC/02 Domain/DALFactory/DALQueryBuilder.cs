using System;
using System.Text;
using System.Collections;

namespace BLS_AspNet.DALFactory
{
	
	internal enum QueryType { Insert, Update };

	public struct QueryItem
	{
		public object value;
		public string name;

		public string GetSafeValue()
		{
			if (value != null)
				return value.ToString();

			return "";
		}
	}

/*	public abstract class BaseClass
	{
		ArrayList keyValues = new ArrayList();

		public void AddKeyValue(string field, object value)
		{
			QueryItem item;
			item.value = value;
			item.name = field;

			keyValues.Add(item);
		}
	}
*/

	public class DALQueryBuilder
	{
		ArrayList dataFields = new ArrayList();
		ArrayList keyFields = new ArrayList();
		string tableName;
		QueryType queryType = QueryType.Insert;
		string clause;

		public DALQueryBuilder(string tableName)
		{
			this.tableName = tableName;
		}

		public DALQueryBuilder(string tableName, string clause)
		{
			queryType = QueryType.Update;
			this.tableName = tableName;
			this.clause = clause;
		}

		public bool IsInserting
		{
			get { return queryType == QueryType.Insert; }
		}

		public string dbaction
		{
			set
			{
				if( value.ToString() == "insert" )
				{
					queryType = QueryType.Insert;
				}
				else
				{
					queryType = QueryType.Update;
				}
			}
		}

		public void AddDataField(string field, object value)
		{
			QueryItem item;
			item.value = value;
			item.name = field;

			dataFields.Add(item);
		}


		public void AddKeyField(string field, object value)
		{
			QueryItem item;
			item.value = value;
			item.name = field;

			keyFields.Add(item);
		}



		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			foreach (QueryItem item in keyFields)
			{
				if (item.value.GetType() == typeof(string) || item.value.GetType() == typeof(char) ||
					item.value.GetType() == typeof(DateTime))
				{
					if (item.GetSafeValue() == "")
					{
						throw new Exception("Primary Key字段不能有空值!!!");
					}
				}
				else
				{
					if (item.GetSafeValue() == "0")
					{
						throw new Exception("Primary Key字段不能有空值!!!");
					}
				}
			}

			if (IsInserting)
			{
				sb.Append("INSERT INTO ");
				sb.Append(tableName);
				sb.Append(" (");

				StringBuilder sbFields = new StringBuilder();
				StringBuilder sbValues = new StringBuilder();

				foreach (QueryItem item in dataFields)
				{
					//sbFields.Append("[");
					sbFields.Append(item.name);
					sbFields.Append(", ");

					if (item.value.GetType() == typeof(string) || item.value.GetType() == typeof(char) ||
					    item.value.GetType() == typeof(DateTime))
					{
						sbValues.Append("'");
						sbValues.Append(item.GetSafeValue());
						sbValues.Append("', ");
					}
					else
					{
						sbValues.Append(item.GetSafeValue());
						sbValues.Append(", ");
					}
				}

				sbFields.Remove(sbFields.Length - 2, 2);
				sbValues.Remove(sbValues.Length - 2, 2);

				sb.Append(sbFields.ToString());

				sb.Append(") VALUES (");
				sb.Append(sbValues.ToString());
				sb.Append(")");
			}
			else
			{
				sb.Append("UPDATE ");
				sb.Append(tableName);
				sb.Append(" SET ");

				foreach (QueryItem item in dataFields)
				{
					//sb.Append("");
					sb.Append(item.name);
					sb.Append(" = ");

					if (item.value.GetType() == typeof(string) || item.value.GetType() == typeof(char) ||
					    item.value.GetType() == typeof(DateTime))
					{
						sb.Append("'");
						sb.Append(item.GetSafeValue());
						sb.Append("', ");
					}
					else
					{
						sb.Append(item.GetSafeValue());
						sb.Append(", ");
					}
				}

				StringBuilder cb = new StringBuilder();
				foreach (QueryItem item in keyFields)
				{
					cb.Append(item.name);
					cb.Append(" = ");

					if (item.value.GetType() == typeof(string) || item.value.GetType() == typeof(char) ||
						item.value.GetType() == typeof(DateTime))
					{
						cb.Append("'");
						cb.Append(item.GetSafeValue());
						cb.Append("' and ");
					}
					else
					{
						cb.Append(item.GetSafeValue());
						cb.Append(" and ");
					}
				}

				cb.Remove(cb.Length - 4, 4);
				sb.Remove(sb.Length - 2, 2);
				sb.Append(" WHERE " + cb.ToString());
			}			

			return sb.ToString();
		}
	}
}