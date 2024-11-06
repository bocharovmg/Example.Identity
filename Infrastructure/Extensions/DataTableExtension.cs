using System.Data;

namespace Infrastructure.Extensions;

public static class DataTableExtension
{
    /// <summary>
    /// Convert list to DataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="valueColumnName"></param>
    /// <param name="rowNumberColumnName">if the parameter is specified, it adds a column with the specified name to the beginning of the table</param>
    /// <returns></returns>
    public static DataTable AsDataTable<T>(this IEnumerable<T>? values, string valueColumnName, string? rowNumberColumnName = null)
    {
        if (string.IsNullOrWhiteSpace(valueColumnName))
        {
            throw new ArgumentNullException($"{nameof(valueColumnName)} is null or white space");
        }

        var dt = new DataTable();

        if (!string.IsNullOrWhiteSpace(rowNumberColumnName))
        {
            dt.Columns.Add(rowNumberColumnName, typeof(int));
        }

        dt.Columns.Add(valueColumnName, typeof(T));

        if (values != null)
        {
            foreach (var value in values)
            {
                DataRow dr = dt.NewRow();

                if (!string.IsNullOrWhiteSpace(rowNumberColumnName))
                {
                    dr[rowNumberColumnName] = dt.Rows.Count;
                }

                dr[valueColumnName] = value;

                dt.Rows.Add(dr);
            }
        }

        return dt;
    }
}
