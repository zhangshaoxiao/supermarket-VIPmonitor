using System;

using System.Data;

using System.Data.Common;

using System.Configuration;



public class DbHelper
{

    private static string dbProviderName = ConfigurationManager.AppSettings["DbHelperProvider"];

    private static string dbConnectionString = ConfigurationManager.AppSettings["DbHelperConnectionString"];



    private DbConnection connection;

    public DbHelper()
    {

        this.connection = CreateConnection(DbHelper.dbConnectionString);

    }

    public DbHelper(string connectionString)
    {

        this.connection = CreateConnection(connectionString);

    }

    public static DbConnection CreateConnection()
    {

        DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);

        DbConnection dbconn = dbfactory.CreateConnection();

        dbconn.ConnectionString = DbHelper.dbConnectionString;

        return dbconn;

    }

    public static DbConnection CreateConnection(string connectionString)
    {

        DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);

        DbConnection dbconn = dbfactory.CreateConnection();

        dbconn.ConnectionString = connectionString;

        return dbconn;

    }



    public DbCommand GetStoredProcCommond(string storedProcedure)
    {

        DbCommand dbCommand = connection.CreateCommand();

        dbCommand.CommandText = storedProcedure;

        dbCommand.CommandType = CommandType.StoredProcedure;

        return dbCommand;

    }

    public DbCommand GetSqlStringCommond(string sqlQuery)
    {

        DbCommand dbCommand = connection.CreateCommand();

        dbCommand.CommandText = sqlQuery;

        dbCommand.CommandType = CommandType.Text;

        return dbCommand;

    }



    #region 增加参数

    public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
    {

        foreach (DbParameter dbParameter in dbParameterCollection)
        {

            cmd.Parameters.Add(dbParameter);

        }

    }

    public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
    {

        DbParameter dbParameter = cmd.CreateParameter();

        dbParameter.DbType = dbType;

        dbParameter.ParameterName = parameterName;

        dbParameter.Size = size;

        dbParameter.Direction = ParameterDirection.Output;

        cmd.Parameters.Add(dbParameter);

    }

    public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
    {

        DbParameter dbParameter = cmd.CreateParameter();

        dbParameter.DbType = dbType;

        dbParameter.ParameterName = parameterName;

        dbParameter.Value = value;

        dbParameter.Direction = ParameterDirection.Input;

        cmd.Parameters.Add(dbParameter);

    }

    public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
    {

        DbParameter dbParameter = cmd.CreateParameter();

        dbParameter.DbType = dbType;

        dbParameter.ParameterName = parameterName;

        dbParameter.Direction = ParameterDirection.ReturnValue;

        cmd.Parameters.Add(dbParameter);

    }

    public DbParameter GetParameter(DbCommand cmd, string parameterName)
    {

        return cmd.Parameters[parameterName];

    }



    #endregion



    #region 执行

    public DataSet ExecuteDataSet(DbCommand cmd)
    {

        DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);

        DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();

        dbDataAdapter.SelectCommand = cmd;

        DataSet ds = new DataSet();

        dbDataAdapter.Fill(ds);

        return ds;

    }



    public DataTable ExecuteDataTable(DbCommand cmd)
    {

        DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);

        DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();

        dbDataAdapter.SelectCommand = cmd;

        DataTable dataTable = new DataTable();

        dbDataAdapter.Fill(dataTable);

        return dataTable;

    }



    public DbDataReader ExecuteReader(DbCommand cmd)
    {

        cmd.Connection.Open();

        DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        return reader;

    }

    public int ExecuteNonQuery(DbCommand cmd)
    {

        cmd.Connection.Open();

        int ret = cmd.ExecuteNonQuery();

        cmd.Connection.Close();

        return ret;

    }



    public object ExecuteScalar(DbCommand cmd)
    {

        cmd.Connection.Open();

        object ret = cmd.ExecuteScalar();

        cmd.Connection.Close();

        return ret;

    }

    #endregion



    #region 执行事务

    public DataSet ExecuteDataSet(DbCommand cmd, Trans t)
    {

        cmd.Connection = t.DbConnection;

        cmd.Transaction = t.DbTrans;

        DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);

        DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();

        dbDataAdapter.SelectCommand = cmd;

        DataSet ds = new DataSet();

        dbDataAdapter.Fill(ds);

        return ds;

    }



    public DataTable ExecuteDataTable(DbCommand cmd, Trans t)
    {

        cmd.Connection = t.DbConnection;

        cmd.Transaction = t.DbTrans;

        DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);

        DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();

        dbDataAdapter.SelectCommand = cmd;

        DataTable dataTable = new DataTable();

        dbDataAdapter.Fill(dataTable);

        return dataTable;

    }



    public DbDataReader ExecuteReader(DbCommand cmd, Trans t)
    {

        cmd.Connection.Close();

        cmd.Connection = t.DbConnection;

        cmd.Transaction = t.DbTrans;

        DbDataReader reader = cmd.ExecuteReader();

        DataTable dt = new DataTable();

        return reader;

    }

    public int ExecuteNonQuery(DbCommand cmd, Trans t)
    {

        cmd.Connection.Close();

        cmd.Connection = t.DbConnection;

        cmd.Transaction = t.DbTrans;

        int ret = cmd.ExecuteNonQuery();

        return ret;

    }



    public object ExecuteScalar(DbCommand cmd, Trans t)
    {

        cmd.Connection.Close();

        cmd.Connection = t.DbConnection;

        cmd.Transaction = t.DbTrans;

        object ret = cmd.ExecuteScalar();

        return ret;

    }

    #endregion

}



public class Trans : IDisposable
{

    private DbConnection conn;

    private DbTransaction dbTrans;

    public DbConnection DbConnection
    {

        get { return this.conn; }

    }

    public DbTransaction DbTrans
    {

        get { return this.dbTrans; }

    }



    public Trans()
    {

        conn = DbHelper.CreateConnection();

        conn.Open();

        dbTrans = conn.BeginTransaction();

    }

    public Trans(string connectionString)
    {

        conn = DbHelper.CreateConnection(connectionString);

        conn.Open();

        dbTrans = conn.BeginTransaction();

    }

    public void Commit()
    {

        dbTrans.Commit();

        this.Colse();

    }



    public void RollBack()
    {

        dbTrans.Rollback();

        this.Colse();

    }



    public void Dispose()
    {

        this.Colse();

    }



    public void Colse()
    {

        if (conn.State == System.Data.ConnectionState.Open)
        {

            conn.Close();

        }

    }

}

//那么如何使用它呢?下面我给出一些基本的使用示例,基本能满足你大部分的数据库操作需要了.

//1)直接执行sql语句



//        DbHelper db = new DbHelper();

//        DbCommand cmd = db.GetSqlStringCommond("insert t1 (id)values(‘haha‘)");

//        db.ExecuteNonQuery(cmd);

//2)执行存储过程



//        DbHelper db = new DbHelper();

//        DbCommand cmd = db.GetStoredProcCommond("t1_insert");

//        db.AddInParameter(cmd, "@id", DbType.String, "heihei");

//        db.ExecuteNonQuery(cmd);

//3)返回DataSet



//        DbHelper db = new DbHelper();

//        DbCommand cmd = db.GetSqlStringCommond("select * from t1");

//        DataSet ds = db.ExecuteDataSet(cmd);

//4)返回DataTable



//        DbHelper db = new DbHelper();

//        DbCommand cmd = db.GetSqlStringCommond("t1_findall");

//        DataTable dt = db.ExecuteDataTable(cmd);

//5)输入参数/输出参数/返回值的使用(比较重要哦)



//        DbHelper db = new DbHelper();

//        DbCommand cmd = db.GetStoredProcCommond("t2_insert");

//        db.AddInParameter(cmd, "@timeticks", DbType.Int64, DateTime.Now.Ticks);

//        db.AddOutParameter(cmd, "@outString", DbType.String, 20);

//        db.AddReturnParameter(cmd, "@returnValue", DbType.Int32);



//        db.ExecuteNonQuery(cmd);



//        string s = db.GetParameter(cmd, "@outString").Value as string;//out parameter

//        int r = Convert.ToInt32(db.GetParameter(cmd, "@returnValue").Value);//return value



//6)DataReader使用



//      DbHelper db = new DbHelper();

//        DbCommand cmd = db.GetStoredProcCommond("t2_insert");

//        db.AddInParameter(cmd, "@timeticks", DbType.Int64, DateTime.Now.Ticks);

//        db.AddOutParameter(cmd, "@outString", DbType.String, 20);

//        db.AddReturnParameter(cmd, "@returnValue", DbType.Int32);



//        using (DbDataReader reader = db.ExecuteReader(cmd))

//        {

//            dt.Load(reader);

//        }       

//        string s = db.GetParameter(cmd, "@outString").Value as string;//out parameter

//        int r = Convert.ToInt32(db.GetParameter(cmd, "@returnValue").Value);//return value





//7)事务的使用.(项目中需要将基本的数据库操作组合成一个完整的业务流时,代码级的事务是必不可少的哦)



//以上我们好像没有指定数据库连接字符串,大家如果看下DbHelper的代码,就知道要使用它必须在config中配置两个参数,如下:



//    pubic void DoBusiness()

//    {

//        using (Trans t = new Trans())

//        {

//            try

//            {

//                D1(t);

//                throw new Exception();//如果有异常,会回滚滴

//                D2(t);

//                t.Commit();

//            }

//            catch

//            {

//                t.RollBack();

//            }

//        }

//    }

//    public void D1(Trans t)

//    {

//        DbHelper db = new DbHelper();

//        DbCommand cmd = db.GetStoredProcCommond("t2_insert");

//        db.AddInParameter(cmd, "@timeticks", DbType.Int64, DateTime.Now.Ticks);

//        db.AddOutParameter(cmd, "@outString", DbType.String, 20);

//        db.AddReturnParameter(cmd, "@returnValue", DbType.Int32);



//        if (t == null) db.ExecuteNonQuery(cmd);

//        else db.ExecuteNonQuery(cmd,t);



//        string s = db.GetParameter(cmd, "@outString").Value as string;//out parameter

//        int r = Convert.ToInt32(db.GetParameter(cmd, "@returnValue").Value);//return value

//    }

//    public void D2(Trans t)

//    {

//        DbHelper db = new DbHelper();

//        DbCommand cmd = db.GetSqlStringCommond("insert t1 (id)values(‘..‘)");       

//        if (t == null) db.ExecuteNonQuery(cmd);

//        else db.ExecuteNonQuery(cmd, t);

//    }

//    <appSettings>

//        <add key="DbHelperProvider" value="System.Data.SqlClient"/>

//        <add key="DbHelperConnectionString" value="Data Source=(local);Initial Catalog=DbHelperTest;Persist Security Info=True;User;Password=sa"/>

//    appSettings>

