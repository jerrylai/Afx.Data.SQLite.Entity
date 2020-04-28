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

namespace Afx.Data.SQLite
{
    /// <summary>
    /// SQLite数据库访问类
    /// </summary>
    public sealed class SQLiteDatabase : Database
    {
#if NETCOREAPP || NETSTANDARD
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public SQLiteDatabase(string connectionString)
            : base(connectionString, SqliteFactory.Instance)
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <param name="isOwnsConnection">释放资源时是否释放链接</param>
        public SQLiteDatabase(SqliteConnection connection, bool isOwnsConnection = true)
            : base(connection, isOwnsConnection)
        {

        }
#else
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public SQLiteDatabase(string connectionString)
            : base(connectionString, SQLiteFactory.Instance)
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <param name="isOwnsConnection">释放资源时是否释放链接</param>
        public SQLiteDatabase(SQLiteConnection connection, bool isOwnsConnection = true)
            : base(connection, isOwnsConnection)
        {

        }
#endif
        /// <summary>
        /// 参数化查询名称加前缀
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string EncodeParameterName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            return string.Format("@{0}", name);
        }
        /// <summary>
        /// 列名转义
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public override string EncodeColumn(string column)
        {
            if (string.IsNullOrEmpty(column)) throw new ArgumentNullException("column");
            return string.Format("[{0}]", column);
        }

        //public override DateTime GetNow()
        //{
        //    this.ClearParameters();
        //    this.CommandText = "select strftime('%Y-%m-%d %H:%M:%f','now','localtime')";
        //    object obj = this.ExecuteScalar();

        //    return Convert.ToDateTime(obj);
        //}
    }
}
