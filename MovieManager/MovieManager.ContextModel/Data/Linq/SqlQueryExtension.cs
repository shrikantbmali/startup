using System;
using System.Text;

namespace MovieManager.ContextModel.Data.Linq
{
	public static class SqlQueryExtension
	{
		public static StringBuilder Select(this object any)
		{
			return new StringBuilder("SELECT ");
		}

		public static StringBuilder Insert(this object any)
		{
			return new StringBuilder("INSERT ");
		}

		public static StringBuilder Into(this StringBuilder query, string tableName)
		{
			return query.Append(" INTO ").Append(tableName);
		}

		public static StringBuilder All(this StringBuilder quary)
		{
			return quary.Append(" * ");
		}

		public static StringBuilder From(this StringBuilder query, string tableName)
		{
			return query.Append(" FROM ").Append(tableName);
		}

		public static StringBuilder Where(this StringBuilder query, object value)
		{
			return query.Append(" WHERE ").Append(value);
		}

		public static StringBuilder EqualsTo(this StringBuilder query, object value)
		{
			return query.Append(" = ").Append(value);
		}

		public static StringBuilder Values(this StringBuilder query, params object[] values)
		{
			query.Append(" VALUES (");

			if (values.Length <= 0)
				throw new ArgumentException("values are not mented!");

			foreach (var value in values)
			{
				if (value == null)
				{
					query.Append("''");
				}
				else if (value is int)
				{
					query.Append(value);
				}
				else if (value is string)
				{
					query.Append('\'').Append(value).Append('\'');
				}
				else if (value is bool)
				{
					query.Append((bool)value ? 1 : 0);
				}
				else
					throw new NotSupportedException("this value type is not supported");

				query.Append(',');
			}

			query.Remove(query.Length - 1, 1).Append(')');

			return query;
		}
	}
}