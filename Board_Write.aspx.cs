using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Board_Write : CWebBase
{
    public String title;
    public String writer;
    public String content;
    public String pw;
    public String date;
    public String isUpdate;
    public String boardid;
    public String writerDisabled;
    public String pwDisabled;

    // 글 작성시 실행 되는 함수
    // 수정일 경우와 수정이 아닐 경우에 따라 처리해주는 함수 
    protected void Page_Load(object sender, EventArgs e)
    {

        title = "";
        writer = "";
        pw = "";
        date = "";
        content = "";
        writerDisabled = "";
        pwDisabled = "";

        String update = Request.QueryString["update"];
        boardid = Request.QueryString["boardid"];

        BoardDAO boarddao = new BoardDAO();

        //수정
        if (update == "1")
        {
            Boards board = boarddao.Select(boardid);

            title = board.getBoardTitle();
            pw = board.getBoardPw();
            writer = board.getBoardWriter();
            date = board.getBoardDate().ToString();
            content = board.getBoardContent();

            btnboardWrite.Text = "수정";
            isUpdate = "true";

            writerDisabled = "readonly";
            pwDisabled = "readonly";
            


        }
    }

}