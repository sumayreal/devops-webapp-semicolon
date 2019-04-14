using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// CWebBase의 요약 설명입니다.
/// </summary>
public class CWebBase : System.Web.UI.Page
{

  

    // 쿠키를 세팅할 도메인을 결정하는 함수
    protected string GetCookieDomain()
    {
        string domain = Request.ServerVariables["Server_Name"];

        if (domain.IndexOf(".") == -1)
            return domain;
        else
        {
            domain = domain.Substring(domain.IndexOf(".") + 1).ToLower();
            if (domain == "ac.kr" || domain == "co.kr" || domain == "com" || domain == "org")
                return Request.ServerVariables["Server_Name"];
            else
                return domain;
        }
    }

    // XMLHTTP를 통해 전달된 XML을 읽어오는 함수
    protected string GetRequestXML()
    {
        StreamReader reader = new StreamReader(Request.InputStream);
        string result = reader.ReadToEnd();
        reader.Close();
        reader = null;

        return result;
    }

    // 텍스트 파일을 읽는 함수
    protected string ReadTextFile(string pFilePath)
    {
        StreamReader reader = new StreamReader(pFilePath, System.Text.Encoding.Default);
        string result = reader.ReadToEnd();
        reader.Close();

        return result;
    }

    // XML텍스트에 알맞게 인코딩해주는 함수
    protected string MakeXMLString(string pOrgStr)
    {
        if (pOrgStr == null)
            return "";

        return pOrgStr.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
    }

    // 태그의 프로퍼티값에 들어가는 형태로 변환하는 함수
    protected string MakePropertyString(string pOrgStr)
    {
        if (pOrgStr == null)
            return "";

        return pOrgStr.Replace("&", "&amp;").Replace("\"", "&quot;");
    }

    // 쿼리문의 검색 문자열에 사용할 수 있게 값을 변경한다.
    protected string MakeSearchField(string pOrgStr)
    {
        if (pOrgStr == null)
            return "";

        return pOrgStr.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");

    }

    // 쿼리문에 사용할 수 있게 값을 변경한다.
    protected string MakeRightField(string pOrgStr)
    {
        if (pOrgStr == null)
            return "";

        return pOrgStr.Replace("'", "''").Replace("\0", "");
    }

    // MakePropertyString의 단축 함수
    protected string MPS(string pOrgStr)
    {
        return MakePropertyString(pOrgStr);
    }

    // MakeRightField의 단축 함수
    protected string MRF(string pOrgStr)
    {
        return MakeRightField(pOrgStr);
    }

    // 금액에 ',' 표시
    protected string MakePriceFormat(string pOrgStr)
    {
        if (pOrgStr == null)
            return "";

        string price = "";

        while (true)
        {
            if (pOrgStr.Length <= 3)
            {
                price = pOrgStr + price;
                break;
            }
            else
            {
                price = "," + pOrgStr.Substring(pOrgStr.Length - 3) + price;
                pOrgStr = pOrgStr.Substring(0, pOrgStr.Length - 3);
            }
        }

        return price;
    }

    // 
    // 에러 표시 및 URL 이동 관련 함수 모음
    //

    protected void ErrorBack(string pMessage)
    {
        Response.Write("<script>alert(\"" + pMessage + "\");history.go(-1);</script>");
        Response.End();
    }

    protected void ErrorGo(string pMessage, string pUrl)
    {
        Response.Write("<script>alert(\"" + pMessage + "\");window.location.href='" + pUrl + "';</script>");
        Response.End();
    }

    protected void JustGo(string pUrl)
    {
        Response.Write("<script>window.location.href='" + pUrl + "';</script>");
        Response.End();
    }

    protected void ErrorTopGo(string pMessage, string pUrl)
    {
        Response.Write("<script>alert(\"" + pMessage + "\");window.top.location.href='" + pUrl + "';</script>");
        Response.End();
    }

    protected void ErrorClose(string pMessage)
    {
        Response.Write("<script>alert(\"" + pMessage + "\");window.close();</script>");
        Response.End();
    }

    protected void ErrorCloseRefresh(string pMessage)
    {
        Response.Write("<script>alert(\"" + pMessage + "\");window.opener.location.reload(false);window.close();</script>");
        Response.End();
    }

    protected void ErrorCloseOpenerGo(string pMessage, string pUrl)
    {
        Response.Write("<script>alert(\"" + pMessage + "\");window.opener.location.href='" + pUrl + "';window.close();</script>");
        Response.End();
    }

    // 긴 제목을 자르는 함수
    protected string GetLeftTitle(string pTitle, int pLength)
    {
        double count = 0;
        string title = "";

        for (int i = 0; i < pTitle.Length; i++)
        {
            if (pTitle.ToCharArray()[i] > 256)
                count += 2;
            else
                count += 1.1;

            if (count > pLength * 2)
                break;

            title += pTitle.Substring(i, 1);
        }

        if (title != pTitle)
            return title + "...";
        else
            return title;
    }

    //
    // 에디터 관련 함수
    //

    protected void insert_editor(string fieldname, string content, string width, string height)
    {
        Response.Write("<div><input type='hidden' id='" + fieldname + "' name='" + fieldname + "' value=\"" + MakePropertyString(content) + "\" style='display:none' /><input type='hidden' id='" + fieldname + "___Config' value='SkinPath=/common/FCKeditor/editor/skins/office2003/' style='display:none' /><iframe id='" + fieldname + "___Frame' src='/common/fckeditor/editor/fckeditor.html?InstanceName=" + fieldname + "&amp;Toolbar=Default' width='" + width + "' height='" + height + "' frameborder='0' scrolling='no'></iframe></div>");
    }

    //
    // 암호화, 복호화 관련 함수
    //

    protected string EncodeBase64(string pString)
    {
        System.Text.ASCIIEncoding AE = new System.Text.ASCIIEncoding();
        pString = System.Convert.ToBase64String(AE.GetBytes(pString));

        return pString;
    }

    protected string DecodeBase64(string pString)
    {
        pString = System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(pString));

        return pString;
    }

    //
    // SQL Injection 처리 관련 함수
    //

    protected string CheckNumber(string pString)
    {
        if (pString == null)
            return null;

        string number = "0123456789";

        for (int i = 0; i < pString.Length; i++)
        {
            if (number.IndexOf(pString.Substring(i, 1)) == -1)
            {
                Response.Write("잘못된 인자입니다.");
                Response.End();
            }
        }

        return pString;
    }
}
