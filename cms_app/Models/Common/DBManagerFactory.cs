using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
/// <summary>
/// Summary description for DataAccessLayer
/// </summary>

public enum DataProvider
{
    SqlServer, OleDb, Odbc, TEst
}
public interface IDBManager
{

    DataProvider ProviderType
    {
        get;
        set;
    }
    string ConnectionString
    {
        get;
        set;
    }
    IDbConnection Connection
    {
        get;
    }
    IDbTransaction Transaction
    {
        get;
    }

    IDataReader DataReader
    {
        get;
    }

    IDbCommand Command
    {
        get;
    }

    IDbDataParameter[] Parameters
    {
        get;
    }

    void Open();
    void BeginTransaction();
    void CommitTransaction();
    void CreateParameters(int paramsCount);
    void AddParameters(int index, string paramName, object objValue);
    void AddxmlParameters(int index, string paramName, object objValue);
    void AddOutParameters(int index, string paramName, DbType ParamType, int size);
    IDataReader ExecuteReader(CommandType cobmmandType, string commandText);
    DataSet ExecuteDataSet(CommandType commandType, string commandText);
    DataTable ExecuteDataTable(CommandType commandType, string commandText);
    object ExecuteScalar(CommandType commandType, string commandText);
    int ExecuteNonQuery(CommandType commandType, string commandText);
    void CloseReader();
    void Close();
    void Dispose();
}


public sealed class DBManagerFactory
{
    private DBManagerFactory() { }
    public static IDbConnection GetConnection(DataProvider providerType)
    {
        IDbConnection iDbConnection = null;
        switch (providerType)
        {
            case DataProvider.SqlServer:
                iDbConnection = new SqlConnection();
                break;
            case DataProvider.OleDb:
                iDbConnection = new OleDbConnection();
                break;
            case DataProvider.Odbc:
                iDbConnection = new OdbcConnection();
                break;
            //case DataProvider.Oracle:
            //iDbConnection = new OracleConnection();
            //break;
            default:
                return null;
        }
        return iDbConnection;
    }

    public static IDbCommand GetCommand(DataProvider providerType)
    {
        switch (providerType)
        {
            case DataProvider.SqlServer:
                return new SqlCommand();
            case DataProvider.OleDb:
                return new OleDbCommand();
            case DataProvider.Odbc:
                return new OdbcCommand();
            //case DataProvider.Oracle:
            //return new OracleCommand();
            default:
                return null;
        }
    }

    public static IDbDataAdapter GetDataAdapter(DataProvider providerType)
    {
        switch (providerType)
        {
            case DataProvider.SqlServer:
                return new SqlDataAdapter();
            case DataProvider.OleDb:
                return new OleDbDataAdapter();
            case DataProvider.Odbc:
                return new OdbcDataAdapter();
            //case DataProvider.Oracle:
            //return new OracleDataAdapter();
            default:
                return null;
        }
    }

    public static IDbTransaction GetTransaction(DataProvider providerType)
    {
        IDbConnection iDbConnection = GetConnection(providerType);
        IDbTransaction iDbTransaction = iDbConnection.BeginTransaction();
        return iDbTransaction;
    }

    public static IDataParameter GetParameter(DataProvider providerType)
    {
        IDataParameter iDataParameter = null;
        switch (providerType)
        {
            case DataProvider.SqlServer:
                iDataParameter = new SqlParameter();
                break;
            case DataProvider.OleDb:
                iDataParameter = new OleDbParameter();
                break;
            case DataProvider.Odbc:
                iDataParameter = new OdbcParameter();
                break;
                //case DataProvider.Oracle:
                //iDataParameter = newOracleParameter();
                //break;

        }
        return iDataParameter;
    }

    public static IDbDataParameter[] GetParameters(DataProvider providerType, int paramsCount)
    {
        IDbDataParameter[] idbParams = new IDbDataParameter[paramsCount];

        switch (providerType)
        {
            case DataProvider.SqlServer:
                for (int i = 0; i < paramsCount; ++i)
                {
                    idbParams[i] = new SqlParameter();
                }
                break;
            case DataProvider.OleDb:
                for (int i = 0; i < paramsCount; ++i)
                {
                    idbParams[i] = new OleDbParameter();
                }
                break;
            case DataProvider.Odbc:
                for (int i = 0; i < paramsCount; ++i)
                {
                    idbParams[i] = new OdbcParameter();
                }
                break;
            // case DataProvider.Oracle:
            // for (int i = 0; i <intParamsLength; ++i)
            //{
            //idbParams[i] = newOracleParameter();
            //}
            //break;
            default:
                idbParams = null;
                break;
        }
        return idbParams;
    }
}



//The DBManager Class

public sealed class DBManager : IDBManager, IDisposable
{
    private IDbConnection idbConnection;
    private IDataReader idataReader;
    private IDbCommand idbCommand;
    private DataProvider providerType;
    private IDbTransaction idbTransaction = null;
    private IDbDataParameter[] idbParameters = null;
    private string strConnection;

    public DBManager()
    {

    }

    public DBManager(DataProvider providerType)
    {
        this.providerType = providerType;
    }

    public DBManager(DataProvider providerType, string connectionString)
    {
        this.providerType = providerType;
        this.strConnection = connectionString;
    }

    public IDbConnection Connection
    {
        get
        {
            return idbConnection;
        }
    }

    public IDataReader DataReader
    {
        get
        {
            return idataReader;
        }
        set
        {
            idataReader = value;
        }
    }

    public DataProvider ProviderType
    {
        get
        {
            return providerType;
        }
        set
        {
            providerType = value;
        }
    }

    public string ConnectionString
    {
        get
        {
            return strConnection;
        }
        set
        {
            strConnection = value;
        }
    }

    public IDbCommand Command
    {
        get
        {
            return idbCommand;
        }
    }

    public IDbTransaction Transaction
    {
        get
        {
            return idbTransaction;
        }
    }

    public IDbDataParameter[] Parameters
    {
        get
        {
            return idbParameters;
        }
    }

    public void Open()
    {
        idbConnection = DBManagerFactory.GetConnection(this.providerType);
        idbConnection.ConnectionString = this.ConnectionString;
        if (idbConnection.State != ConnectionState.Open)
            idbConnection.Open();
        this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
    }

    public void Close()
    {
        if (idbConnection.State != ConnectionState.Closed)
            idbConnection.Close();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.Close();
        this.idbCommand = null;
        this.idbTransaction = null;
        this.idbConnection = null;
    }

    public void CreateParameters(int paramsCount)
    {
        idbParameters = new IDbDataParameter[paramsCount];
        idbParameters = DBManagerFactory.GetParameters(this.ProviderType, paramsCount);
    }

    //public void AddParameters_old(int index, string paramName, object objValue)
    //{
    //    if (index < idbParameters.Length)
    //    {
    //        idbParameters[index].ParameterName = paramName;
    //        idbParameters[index].Value = objValue;
    //    }
    //}

    public void AddParameters(int index, string paramName, object objValue)
    {
        if (preventInjection(objValue.ToString()))
        {
            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                idbParameters[index].Value = objValue;
            }
        }
    }

    public void AddxmlParameters(int index, string paramName, object objValue)
    {
        if (index < idbParameters.Length)
        {
            idbParameters[index].ParameterName = paramName;
            idbParameters[index].Value = objValue;
        }
    }

    public void AddParameters(int index, string paramName, object objValue, ParameterDirection PD, DbType stype, int len)
    {

        if (preventInjection(objValue.ToString()))
        {
            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                idbParameters[index].Value = objValue.ToString().Replace("'", "''").Trim();
                idbParameters[index].Direction = PD;
                idbParameters[index].DbType = stype;
                idbParameters[index].Size = len;
            }
        }
    }
    public void AddParameters(int index, string paramName, object objValue, ParameterDirection PD)
    {

        if (preventInjection(objValue.ToString()))
        {
            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                idbParameters[index].Value = objValue.ToString().Replace("'", "''").Trim();
                idbParameters[index].Direction = PD;
            }
        }
    }

    public void AddOutParameters(int index, string paramName, DbType ParamType, int size)
    {
        if (index < idbParameters.Length)
        {
            idbParameters[index].ParameterName = paramName;
            idbParameters[index].Direction = ParameterDirection.Output;
            idbParameters[index].DbType = ParamType;
            idbParameters[index].Size = size;
        }
    }


    //for sql security 
    public bool preventInjection(string s)
    {
        s = s.ToLower();
        string cleanedData = string.Empty;
        //s.Contains(";") || 
        //if (s.Contains("--") || s.Contains("format ") || s.Contains("insert ") || s.Contains("delete ") || s.Contains("xp_") || s.Contains("update ") || s.Contains("drop ") || s.Contains("sp_"))
        if (s.Contains("--") || s.Contains("format ") || s.Contains("insert ") || s.Contains("delete ") || s.Contains("xp_") || s.Contains("update ") || s.Contains("drop ") || s.Contains("sp_") || s.Contains("/*") || s.Contains("*/") || s.Contains("@@") || s.Contains("delete ") || s.Contains("execute ") || s.Contains("insert ") || s.Contains("sysobjects") || s.Contains("syscolumns") || s.Contains("db_") || s.Contains("<") || s.Contains(">") || s.Contains("&gt;") || s.Contains("&lt;") || s == "table " || s == "kill" || s == "create " || s == "drop" || s == "alter" || s == "exec " || s == "cast " || s == "sys" || s == "update " || s == "@")
        {
            HttpContext.Current.Response.Write("<script>alert('Your data contains potentially harmful element(s) and has been blocked. Please correct your data or contact Administrator at televident@reckittbenckiser.com for assistance !');</script>");
            return false;
        }
        return true;
    }


    public void BeginTransaction()
    {
        if (this.idbTransaction == null)
            idbTransaction = DBManagerFactory.GetTransaction(this.ProviderType);
        this.idbCommand.Transaction = idbTransaction;
    }

    public void CommitTransaction()
    {
        if (this.idbTransaction != null)
            this.idbTransaction.Commit();
        idbTransaction = null;
    }

    public IDataReader ExecuteReader(CommandType commandType, string commandText)
    {
        this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
        idbCommand.Connection = this.Connection;
        PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
        this.DataReader = idbCommand.ExecuteReader();
        idbCommand.Parameters.Clear();
        return this.DataReader;
    }

    public void CloseReader()
    {
        if (this.DataReader != null)
            this.DataReader.Close();
    }

    private void AttachParameters(IDbCommand command, IDbDataParameter[] commandParameters)
    {
        foreach (IDbDataParameter idbParameter in commandParameters)
        {
            if ((idbParameter.Direction == ParameterDirection.InputOutput)
            &&
              (idbParameter.Value == null))
            {
                idbParameter.Value = DBNull.Value;
            }
            command.Parameters.Add(idbParameter);
        }
    }

    private void GetParametersValue(IDbCommand command, IDbDataParameter[] commandParameters)
    {
        foreach (IDbDataParameter idbParameter in commandParameters)
        {
            if ((idbParameter.Direction == ParameterDirection.InputOutput)
            &&
              (idbParameter.Value == null))
            {
                idbParameter.Value = DBNull.Value;
            }
            command.Parameters.Add(idbParameter);
        }
    }

    private void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters)
    {
        command.Connection = connection;
        command.CommandText = commandText;
        command.CommandType = commandType;
        //command.CommandTimeout = 200000;
        command.CommandTimeout = 9999;

        if (transaction != null)
        {
            command.Transaction = transaction;
        }

        if (commandParameters != null)
        {
            AttachParameters(command, commandParameters);
        }
    }

    public int ExecuteNonQuery(CommandType commandType, string commandText)
    {
        this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
        PrepareCommand(idbCommand, this.Connection, this.Transaction,
        commandType, commandText, this.Parameters);
        int returnValue = idbCommand.ExecuteNonQuery();
        idbCommand.Parameters.Clear();
        return returnValue;
    }

    public object ExecuteScalar(CommandType commandType, string commandText)
    {
        this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
        PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
        object returnValue = idbCommand.ExecuteScalar();
        idbCommand.Parameters.Clear();
        return returnValue;
    }

    public DataSet ExecuteDataSet(CommandType commandType, string commandText)
    {
        this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
        PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
        IDbDataAdapter dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
        dataAdapter.SelectCommand = idbCommand;
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        idbCommand.Parameters.Clear();
        return dataSet;
    }


    public DataTable ExecuteDataTable(CommandType commandType, string commandText)
    {
        DataTable dt = new DataTable();
        this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
        PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
        IDbDataAdapter dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
        dataAdapter.SelectCommand = idbCommand;
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        idbCommand.Parameters.Clear();
        if (dataSet.Tables.Count > 0)
            dt = dataSet.Tables[0];
        return dt;
    }


    public int ExecuteNonQueryForHtml(CommandType commandType, string commandText)
    {
        this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);

        int returnValue = idbCommand.ExecuteNonQuery();
        idbCommand.Parameters.Clear();
        return returnValue;
    }
}

