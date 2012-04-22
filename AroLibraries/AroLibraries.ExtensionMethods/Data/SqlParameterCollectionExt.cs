using System.Collections.Generic;
using System.Data.SqlClient;

namespace AroLibraries.ExtensionMethods.Data
{
    public static class SqlParameterCollectionExt
    {
        public static IEnumerable<SqlParameter> ToSqlParameters(this SqlParameterCollection iParameterCollection)
        {
            var rList = new List<SqlParameter>();
            foreach (SqlParameter dbParameter in iParameterCollection)
            {
                var vParameter = new SqlParameter(dbParameter.ParameterName, dbParameter.SqlDbType, dbParameter.Size, dbParameter.Direction, dbParameter.IsNullable,
                    dbParameter.Precision, dbParameter.Scale, dbParameter.SourceColumn, dbParameter.SourceVersion, dbParameter.Value);
                yield return vParameter;
            }
        }
    }
}