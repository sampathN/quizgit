using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class db
{
    private SqlConnection conn;
    private SqlDataReader sqldatareader;
    private DataSet sqldataset = new DataSet();
    private DataTable sqldatatable = new DataTable();

    string databaseString = ConfigurationManager.ConnectionStrings["quizConnectionString"].ConnectionString;

    //get connection string based on the url
    public db()
    {       
         conn = new SqlConnection(databaseString);        
    }

    //open the db connection
    public SqlConnection Openconn()
    {
        if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
        {
            conn.Open();
        }
        return conn;
    }

    //close the db connection
    public SqlConnection Closeconn()
    {
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
        return conn;
    }

    //to execute a nonquery
    public void ExecuteQuery(SqlCommand Execcmd)
    {
        try
        {
            Execcmd.Connection = Openconn();
            Execcmd.ExecuteNonQuery();
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
        finally
        {
            Execcmd.Connection = Closeconn();
        }
    }

    //to execute a nonquery
    public int ReturnIDonExecuteQuery(SqlCommand Execcmd)
    {
        int insertid = 0;
        try
        {
            Execcmd.Connection = Openconn();
            insertid = Convert.ToInt32(Execcmd.ExecuteScalar());
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
        finally
        {
            Execcmd.Connection = Closeconn();
        }
        return insertid;
    }

    //returns a dataset
    public DataSet returnDataSet(SqlCommand DScmd)
    {
        SqlDataAdapter DSadapater;
        try
        {
            sqldataset.Clear();
            DScmd.Connection = Openconn();         
            DSadapater = new SqlDataAdapter(DScmd);
            DSadapater.Fill(sqldataset);
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
        finally
        {
            DScmd.Connection = Closeconn();
        }
        return sqldataset;
    }

    //returns a datatable
    public DataTable returnDataTable(SqlCommand DTcmd)
    {
        SqlDataAdapter DTadapter;
        try
        {
            sqldatatable.Clear();
            DTcmd.Connection = Openconn();
            DTadapter = new SqlDataAdapter(DTcmd);
            DTadapter.Fill(sqldatatable);            
        }
        catch (Exception Ex)
        {
            Ex.Message.ToString();
        }
        finally
        {
            DTcmd.Connection = Closeconn();
        }
        return sqldatatable;
    }

    //returns a datareader
    public SqlDataReader returnDataReader(SqlCommand DRcmd)
    {
        try
        {
            DRcmd.Connection = Openconn();
            sqldatareader = DRcmd.ExecuteReader();
            return sqldatareader;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
        finally
        {
            DRcmd.Connection = null;
        }
    }
}