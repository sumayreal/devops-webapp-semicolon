using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Board_Write_Save : CWebBase
{
    // 데이터베이스에 저장해주는 함수 
    protected void Page_Load(object sender , EventArgs e)
    {

        string boardTitle = Request.Form["boardTitle"] == null ? "" : Request.Form["boardTitle"].ToString();
        string boardWriter = Request.Form["boardWriter"] == null ? "" : Request.Form["boardWriter"].ToString();
        string boardPw = Request.Form["boardPw"] == null ? "" : Request.Form["boardPw"].ToString();
        string boardContent = Request.Form["boardContent"] == null ? "" : Request.Form["boardContent"].ToString();
        string isboardUpdate = Request.Form["isboardUpdate"] == null ? "" : Request.Form["isboardUpdate"].ToString();
        string boardId = Request.Form["BoardId"] == null ? "" : Request.Form["BoardId"].ToString();


        BoardDAO boarddao = new BoardDAO();

        // 수정이 아닐 경우 
        if (isboardUpdate != "true")
        {
            boarddao.Insert(boardTitle, boardWriter, boardContent, boardPw);
        }

        // 수정일 경우 
        else if(isboardUpdate == "true")
        {
            boarddao.Update(boardTitle, boardWriter, boardContent,  boardId);
        }

        ErrorGo("완료되었습니다", "Board_Main.aspx");

    }
}