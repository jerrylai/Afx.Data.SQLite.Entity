using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

#if NETCOREAPP || NETSTANDARD
using Microsoft.Data.Sqlite;
#else
using System.Data.SQLite;
#endif
using Afx.Data;
using Afx.Data.Entity.Schema;

namespace Afx.Data.SQLite.Entity.Schema
{
    /// <summary>
    /// 获取model信息
    /// </summary>
    public sealed class SQLiteModelSchema : ModelSchema
    {
        /// <summary>
        /// 获取model属性类型string
        /// </summary>
        /// <param name="column">列信息</param>
        /// <returns>model属性类型string</returns>
        public override string GetPropertyType(ColumnInfoModel column)
        {
            string type = "";
            DataTypeModel m = null;
            dic.TryGetValue(column.DataType, out m);
            if (m != null)
            {
                type = column.IsNullable ? m.NullableType : m.Type;
            }

            return type;
        }

        private static Dictionary<string, DataTypeModel> dic = new Dictionary<string, DataTypeModel>(StringComparer.OrdinalIgnoreCase);

        static SQLiteModelSchema()
        {
            dic.Add("int", new DataTypeModel() { DbDataType = "int", Type = "int", NullableType = "Nullable<int>" });
            dic.Add("integer", new DataTypeModel() { DbDataType = "integer", Type = "int", NullableType = "Nullable<int>" });
            dic.Add("mediumint", new DataTypeModel() { DbDataType = "mediumint", Type = "int", NullableType = "Nullable<int>" });

            dic.Add("bigint", new DataTypeModel() { DbDataType = "bigint", Type = "long", NullableType = "Nullable<long>" });
            dic.Add("int8", new DataTypeModel() { DbDataType = "int8", Type = "long", NullableType = "Nullable<long>" });
            dic.Add("unsigned big int", new DataTypeModel() { DbDataType = "unsigned big int", Type = "long", NullableType = "Nullable<long>" });

            dic.Add("smallint", new DataTypeModel() { DbDataType = "smallint", Type = "short", NullableType = "Nullable<short>" });
            dic.Add("int2", new DataTypeModel() { DbDataType = "int2", Type = "short", NullableType = "Nullable<short>" });

            dic.Add("tinyint", new DataTypeModel() { DbDataType = "tinyint", Type = "byte", NullableType = "Nullable<byte>" });

            dic.Add("boolean", new DataTypeModel() { DbDataType = "boolean", Type = "bool", NullableType = "Nullable<bool>" });

            dic.Add("date", new DataTypeModel() { DbDataType = "date", Type = "DateTime", NullableType = "Nullable<DateTime>" });
            dic.Add("datetime", new DataTypeModel() { DbDataType = "datetime", Type = "DateTime", NullableType = "Nullable<DateTime>" });

            dic.Add("numeric", new DataTypeModel() { DbDataType = "numeric", Type = "decimal", NullableType = "Nullable<decimal>" });
            dic.Add("decimal", new DataTypeModel() { DbDataType = "decimal", Type = "decimal", NullableType = "Nullable<decimal>" });

            dic.Add("real", new DataTypeModel() { DbDataType = "real", Type = "double", NullableType = "Nullable<double>" });
            dic.Add("double", new DataTypeModel() { DbDataType = "double", Type = "double", NullableType = "Nullable<double>" });
            dic.Add("double precision", new DataTypeModel() { DbDataType = "double precision", Type = "double", NullableType = "Nullable<double>" });
            dic.Add("float", new DataTypeModel() { DbDataType = "float", Type = "float", NullableType = "Nullable<float>" });
            
            dic.Add("char", new DataTypeModel() { DbDataType = "char", Type = "string", NullableType = "string" });
            dic.Add("nchar", new DataTypeModel() { DbDataType = "nchar", Type = "string", NullableType = "string" });
            dic.Add("varchar", new DataTypeModel() { DbDataType = "varchar", Type = "string", NullableType = "string" });
            dic.Add("nvarchar", new DataTypeModel() { DbDataType = "nvarchar", Type = "string", NullableType = "string" });
            dic.Add("text", new DataTypeModel() { DbDataType = "text", Type = "string", NullableType = "string" });
            dic.Add("clob", new DataTypeModel() { DbDataType = "clob", Type = "string", NullableType = "string" });
            dic.Add("character", new DataTypeModel() { DbDataType = "character", Type = "string", NullableType = "string" });
            dic.Add("varying character", new DataTypeModel() { DbDataType = "varying character", Type = "string", NullableType = "string" });
            dic.Add("native character", new DataTypeModel() { DbDataType = "native character", Type = "string", NullableType = "string" });

            dic.Add("none", new DataTypeModel() { DbDataType = "none", Type = "byte[]", NullableType = "byte[]" });
            dic.Add("blob", new DataTypeModel() { DbDataType = "blob", Type = "byte[]", NullableType = "byte[]" });
        }
    }
}
