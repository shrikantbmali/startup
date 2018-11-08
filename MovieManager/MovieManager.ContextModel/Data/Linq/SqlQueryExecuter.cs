using System;
using System.Common;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using MovieManager.ContextModel.Properties;

namespace MovieManager.ContextModel.Data.Linq
{
	public static class SqlQueryExecuter
	{
		public static void ExecuteScalar(this StringBuilder query)
		{
			using (var connection = new SqlConnection(Settings.Default.LMDBConnectionString))
			{
				try
				{
					connection.Open();

					using (var command = new SqlCommand(query.ToString(), connection))
					{
						command.ExecuteScalar();
					}
				}
				catch (Exception)
				{
				}
				finally
				{
					connection.Close();
				}
			}
		}

		public static IResult<DataTable, DBFailureReason> ExecuteReader(this StringBuilder query)
		{
			IResult<DataTable, DBFailureReason> result;
			using (var connection = new SqlConnection(Settings.Default.LMDBConnectionString))
			{
				try
				{
					connection.Open();

					using (var command = new SqlCommand(query.ToString(), connection))
					{
						using (var reader = command.ExecuteReader())
						{
							var dataTable = new DataTable();
							dataTable.Load(reader);
							result = new Result<DataTable, DBFailureReason>(dataTable, true);
						}
					}
				}
				catch (Exception ex)
				{
					result = new Result<DataTable, DBFailureReason>(GetDbFailureReason(ex)) { Exception = ex };
				}
				finally
				{
					connection.Close();
				}
			}

			return result;
		}

		private static DBFailureReason GetDbFailureReason(Exception ex)
		{
			var reason = DBFailureReason.None;

			if (ex is ObjectDisposedException
				|| ex is SqlException
				|| ex is InvalidOperationException
				|| ex is InvalidCastException)

				reason = DBFailureReason.SqlException;
			else if (ex is IOException)
				reason = DBFailureReason.IOException;

			return reason;
		}
	}
}