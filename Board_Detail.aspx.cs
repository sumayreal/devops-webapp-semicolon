using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Board_Detail : CWebBase
{
    String boardid;
    String isUpdated;
    Boards board;


    // board 상세 데이터 가져와서 화면에 보여주는 함수 
    protected void Page_Load(object sender , EventArgs e)
    {
        boardid = Request.QueryString["boardid"];
        isUpdated = Request.QueryString["update"];


        BoardDAO boarddao = new BoardDAO();


        if (boardid == "")
        {
            JustGo("Board_Main.aspx");
            return; 
        }


        board = boarddao.Select(boardid);
        txtBoardTitle.Text = board.getBoardTitle();
        txtBoardWriter.Text = board.getBoardWriter();
        txtBoardPw.Text = "****";
        txtBoardContent.Text = board.getBoardContent();
   


    }

    // 수정 버튼 눌렀을 때 실행되는 함수
    public void updateBoard(object sender, EventArgs e)
    {
        txtupdatePw.Visible = true;

        if (btnboardUpdate.Text == "확인")
        {
            if (isUpdate(board.getBoardPw() , txtupdatePw.Text))
            {
                JustGo("Board_Write.aspx?update=1&boardid=" + boardid);
            }
        }

        btnboardUpdate.Text = "확인";

    }

    // 업데이트 가능한 지 확인해주는 ㅎㅁ수 
    public Boolean isUpdate(String userPw , String boardPw)
    {

        if (userPw == boardPw)
        {
            return true;
        }

        else
        {
            ErrorGo("비밀번호를 확인해주세요" , "Board_Detail.aspx?boardid="+boardid.ToString());
            return false;
        }
    }

    //취소 버튼 눌렀을 때 실행될 함수
    protected void cancel(object sender , EventArgs e)
    {
        JustGo("Board_Main.aspx");
    }
}