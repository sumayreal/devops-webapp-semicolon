using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// CDatabase의 요약 설명입니다.
/// </summary>
public class CDatabase
{
    protected SqlConnection _conn = null;

    public CDatabase(string pConnstr)
    {
        _conn = new SqlConnection(pConnstr);
        _conn.Open();
    }

    public CDatabase()
    {
        _conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["DSN"]);
        _conn.Open();
    }

    public void Dispose()
    {
        _conn.Close();
        _conn.Dispose();
        _conn = null;
    }

    ~CDatabase()
    {
        if (_conn != null)
        {
            _conn.Close();
            _conn.Dispose();
        }
    }

    // 삭제, 수정, 추가등 결과값을 반환하지 않는 SQL문을 실행하는 함수
    public bool ExecuteSQL(string pSQL)
    {
        //	try
        //	{
        pSQL = "SET XACT_ABORT ON\n Begin Tran \n " + pSQL + "\n Commit Tran";

        SqlCommand comd = new SqlCommand(pSQL, _conn);
        comd.ExecuteNonQuery();
        comd.Dispose();

        return true;
        //	}
        //	catch (Exception e)
        //	{
        //		return false;
        //	}
    }

    // 삭제, 수정, 추가등 결과값을 반환하지 않는 SQL문을 실행하는 함수
    public bool ExecuteSQL_NoTransaction(string pSQL)
    {
        //	try
        //	{
        SqlCommand comd = new SqlCommand(pSQL, _conn);
        comd.ExecuteNonQuery();
        comd.Dispose();

        return true;
        //	}
        //	catch (Exception e)
        //	{
        //		return false;
        //	}
    }

    // Count와 같은 숫자 1개의 결과만을 가져올 때 사용하는 함수
    public int GetQueryInt(string pSQL)
    {
        int result = 0;

        //	try
        //	{
        SqlCommand comd = new SqlCommand(pSQL, _conn);
        SqlDataReader dr = comd.ExecuteReader();
        comd.Dispose();

        if (dr.Read())
        {
            string sresult = dr.GetSqlValue(0).ToString();
            result = Convert.ToInt32(sresult);
        }

        dr.Close();

        return result;
        //	}
        //	catch (Exception e)
        //	{
        //		return 0;
        //	}
    }

    // 문자열 하나만의 결과를 가져올 때 사용하는 함수
    public string GetQueryString(string pSQL)
    {
        string result = "";

        //	try
        //	{
        SqlCommand comd = new SqlCommand(pSQL, _conn);
        SqlDataReader dr = comd.ExecuteReader();
        comd.Dispose();

        if (dr.Read())
            result = dr.GetSqlValue(0).ToString();

        dr.Close();

        return result;
        //	}
        //	catch (Exception e)
        //	{
        //		return "";
        //	}
    }

    // 쿼리문에 해당하는 레코드셋을 반환하는 함수
    public SqlDataReader GetQueryResult(string pSQL)
    {
        //	try
        //	{
        SqlCommand comd = new SqlCommand(pSQL, _conn);
        comd.CommandTimeout = 60 * 10;
        SqlDataReader dr = comd.ExecuteReader();
        comd.Dispose();

        return dr;
        //	}
        //	catch (Exception e)
        //	{
        //		return null;
        //	}
    }
}
