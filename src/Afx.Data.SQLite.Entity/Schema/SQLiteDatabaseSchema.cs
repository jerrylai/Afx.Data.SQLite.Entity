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
    /// 获取数据库结构信息
    /// </summary>
    public sealed class SQLiteDatabaseSchema : DatabaseSchema
    {
        private string file;
        private string database;
        private string connectionString;
        /// <summary>
        /// 执行sql logs
        /// </summary>
        public override Action<string> Log
        {
            get;
            set;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="connectionString">数据库链接字符串</param>
        public SQLiteDatabaseSchema(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");
            //Data Source=C:\FileSystem.db;Password=mycsv.cn;Version=3;Pooling=True;UseUTF16Encoding=False;DateTimeKind=Local
            this.connectionString = connectionString;
#if NETCOREAPP || NETSTANDARD
            var stringBuilder = new SqliteConnectionStringBuilder(connectionString);
#else
            var stringBuilder = new SQLiteConnectionStringBuilder(connectionString);
#endif
            this.file = stringBuilder.DataSource;
            this.database = System.IO.Path.GetFileNameWithoutExtension(this.file);
        }
        /// <summary>
        /// 是否存在数据库
        /// </summary>
        /// <returns>true：存在，false：不存在</returns>
        public override bool Exist()
        {
            int count = System.IO.File.Exists(this.file) ? 1 : 0;

            return count > 0;
        }
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <returns>true：创建成功，false：创建失败</returns>
        public override bool CreateDatabase()
        {
            int count = 0;
            string path = System.IO.Path.GetDirectoryName(this.file);
            if(!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            using (var db = new SQLiteDatabase(this.connectionString))
            {
                count += db.ExecuteNonQuery("create table _tb_create_db_temp(id int not null)");
                count += db.ExecuteNonQuery("drop table _tb_create_db_temp");
            }

            return count > 0;
        }
        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <returns>true：删除成功，false：删除失败</returns>
        public override bool DeleteDatabase()
        {
            if (System.IO.File.Exists(this.file))
            {
                System.IO.File.Delete(this.file);
            }

            return true;
        }
        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns>数据库名称</returns>
        public override string GetDatabase()
        {
            return this.database;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
