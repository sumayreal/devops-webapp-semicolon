using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Board : CWebBase
{
    private String dateOrderSQL; // 날짜(최신순 / 오래된순)
    private int numberPage; // 페이지당 게시글 개수(10 / 15 / 20)
    private int pageNum; // 현재 페이지 수 
    private String writerOrderSQL; // 작성자명(가나다순)
    private String titleOrderSQL; // 제목(가나다순)
    private ArrayList orderList;
    public Boolean isSelectDelete; // 체크 박스가 나타나있는지 여부 (0: 나타나있음 1:나타나있지 않음)

    private DataTable reader;

    // gridView에 data bind 해주는 부분
    protected void Page_Load(object sender , EventArgs e)
    {

        BoardDAO boarddao = new BoardDAO();

        this.dateOrderSQL = "";
        this.writerOrderSQL = "";
        this.titleOrderSQL = "";


        if (!IsPostBack)
        {
            this.dateOrderSQL = "board_date DESC";

            this.writerOrderSQL = "";
            this.titleOrderSQL = "";

            this.isSelectDelete = false;


            this.numberPage = 10;
            this.pageNum = 1;
            reader = (DataTable)ViewState["boards"];
            if (reader == null)
            {
                reader = boarddao.selectAll();
                ViewState["boards"] = reader;
            }

            setBoardList();
        }


    }

    // 페이지에 보여지는 글 수 선택하면 실행되는 함수
    // gridView의 사이즈 조정해주는 함수
    protected void ddlPageNumPerPage_SelectedIndexChanged(object sender , EventArgs e)
    {
        BoardDAO boarddao = new BoardDAO();

        this.numberPage = int.Parse(ddlPageNumPerPage.SelectedValue);
        boardList.PageSize = numberPage;

        reader = (DataTable)ViewState["boards"];

        setBoardList();
    }

    // 정렬 순서 정해주는 함수 
    // 날짜 , 제목 , 작성자
    protected String setOrder()
    {
        String result = "";
        if(this.dateOrderSQL != "")
        {
            result += this.dateOrderSQL + ",";
        }

        if(this.titleOrderSQL != "")
        {
            result += this.titleOrderSQL + ",";
        }

        if(this.writerOrderSQL != "")
        {
            result += this.writerOrderSQL + ",";
        }

       return result.Substring(0 , result.Length - 1);
    }


    // 제목 정렬 선택하면 수행되는 함수
    protected void ddlTitleOrder_SelectedIndexChanged(object sender , EventArgs e)
    {

        String titleOption = ddlTitleOrder.SelectedValue;

        // 제목
        if (titleOption == "-1")
        {
            this.titleOrderSQL = "";
        }

        // 가나다순
        if (titleOption == "0")
        {
            BoardDAO boarddao = new BoardDAO();
            this.titleOrderSQL = "board_title ASC";

            String order = setOrder();
            reader = boarddao.selectAll(order);
            ViewState["boards"] = reader;
            setBoardList();
        }  
    }

    // 작성자명 정렬 선택하면 수행되는 함수
    protected void ddlWriterOrder_SelectedIndexChanged(object sender , EventArgs e)
    {
        String writerOption = ddlWriterOrder.SelectedValue;

        // 작성자명
        if (writerOption == "-1")
        {
            this.writerOrderSQL = "";
        }

        // 가나다순
        if (writerOption == "0")
        {
            BoardDAO boarddao = new BoardDAO();
            this.writerOrderSQL = "board_writer ASC";

            String order = setOrder();

            reader = boarddao.selectAll(order);
            ViewState["boards"] = reader;

            setBoardList();
        }

    }

    // 날짜 정렬 선택하면 수행되는 함수
    protected void ddlDateOrder_SelectedIndexChanged(object sender , EventArgs e)
    {
        String dateOption = ddlDateOrder.SelectedValue;

        // 날짜
        if (dateOption == "-1")
        {
            this.dateOrderSQL = "";
        }

        // 최신순
        if (dateOption == "0")
        {
            BoardDAO boarddao = new BoardDAO();
            this.dateOrderSQL = "board_date DESC";

            String order = setOrder();
            reader = boarddao.selectAll(order);
            ViewState["boards"] = reader;

            setBoardList();
        }

        //오래된 순 
        if (dateOption == "1")
        {
            BoardDAO boarddao = new BoardDAO();
            this.dateOrderSQL = "board_date ASC";

            String order = setOrder();
            reader = boarddao.selectAll(order);
            ViewState["boards"] = reader;

            setBoardList();
        }
    }

    // gridView에서 한 줄이 생성되었을 때 실행되는 함수
    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Width = 600;
            e.Row.Cells[3].Width = 200;
            e.Row.Cells[4].Width = 100;
            e.Row.Cells[2].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(boardList, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "자세히 보려면 제목을 클릭해주세요!";
        }
    }

    // gridView의 row 선택했을 때 연결되어서 실행되는 함수
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in boardList.Rows)
        {
            if (row.RowIndex == boardList.SelectedIndex)
            {
                JustGo("Board_Detail.aspx?boardid="+boardList.Rows[boardList.SelectedIndex].Cells[1].Text);
                row.ToolTip = string.Empty;
            }
        }
    }

    // gridView의 page가 바뀌면 수행되는 함수
    protected void OnPageIndexChanged(Object sender , GridViewPageEventArgs e)
    {
        reader = (DataTable)ViewState["boards"];
        this.numberPage = int.Parse(ddlPageNumPerPage.SelectedValue);
        this.pageNum = e.NewPageIndex;

        setBoardList();
        boardList.PageIndex = e.NewPageIndex;
    }


    // row 클릭되었을 때 수행되는 함수
    protected void select()
    {
        JustGo("Board_Detail.aspx");
    }

    // 검색 버튼이 눌렀을 때 수행되는 함수
    protected void search(object sender , EventArgs e)
    {
        String searchoption = ddlSearchOption.SelectedValue;
        String search_keyword = txtSearchOptionDetail.Text;

        BoardDAO boarddao = new BoardDAO();


        switch (searchoption)
        {
            // 제목
            case "0":
                {
                    reader = boarddao.SelectTitle(search_keyword);
                    ViewState["boards"] = reader;
                    setBoardList();
                    break;
                }

            // 작성자명
            case "1":
                {
                    reader = boarddao.SelectWriter(search_keyword);
                    ViewState["boards"] = reader;
                    setBoardList();
                    break;
                }

            // 내용
            case "2":
                {
                    reader = boarddao.SelectContent(search_keyword);
                    ViewState["boards"] = reader;
                    setBoardList();
    
                    break;
                }

            // 전체
            case "3":
                {
                    reader = boarddao.SelectSum(search_keyword);
                    ViewState["boards"] = reader;
                    setBoardList();
                    break;
                }
            default:
                {
                    ErrorGo("다시 한 번 확인해주세요" , "Board_Main.aspx");
                    break;
                }
        }


    }
    

    // gridView의 databind해주는 함수
    protected void setBoardList()
    {
        BoardDAO boarddao = new BoardDAO();
       

        boardList.DataSource = reader;
        boardList.DataBind();

        lblPageDetail.Text = "전체 " + reader.Rows.Count + " 건 / " + (boardList.PageIndex + 1) + " 페이지";
    }
        

    // 게시글 작성 버튼 눌렀을 때 게시글 작성 화면으로 이동시켜줄 함 수
    protected void write(object sender , EventArgs e)
    {
        JustGo("Board_write.aspx");
    }

    // 삭제해주는 함수
    protected void delete(object sender , EventArgs e)
    {
        if (btnDelete.Text == "삭제")
        {
            isSelectDelete = !isSelectDelete;
            boardList.Columns[0].Visible = isSelectDelete;

            txtPw.Visible = true;
            txtPw.Text = "";
            btnDelete.Text = "확인";
        } 

        else if (btnDelete.Text == "확인")
        {
            BoardDAO boarddao = new BoardDAO();

            int deleteNum = 0;
            ArrayList deleteBoardNum = new ArrayList();
            String deleteBoardPw = "";

            // 체크된 체크박스 확인해주는 부분
            for (int i = 0; i < boardList.Rows.Count; i++)
            {
                if(((CheckBox)boardList.Rows[i].FindControl("board_checkbox")).Checked)
                {
                    deleteBoardNum.Add(boardList.Rows[i].Cells[1].Text);
                    deleteBoardPw = boardList.Rows[i].Cells[5].Text;
                    deleteNum++;
                }
            }

            // 삭제하려는 게시글 수가 한 개일경우
            if(deleteNum == 1 && isDelete(txtPw.Text , deleteBoardPw ))
            {
                if (boarddao.Delete(((string)deleteBoardNum[0])))
                {
                    ErrorGo("삭제하였습니다", "Board_Main.aspx");
                }
            }

            // 삭제하려는 게시글 수가 여러개인 경우
            // 관리자 비밀번호는 1220
            else if(deleteNum > 1 && isDelete(txtPw.Text , "1220" ))
            {
                foreach (object boardId in deleteBoardNum)
                {
                    string boardnum = (string)boardId;
                    Boolean result = boarddao.Delete(boardnum);
                }

                ErrorGo("삭제하였습니다", "Board_Main.aspx");
            }

   
        }
    }

    public Boolean isDelete ( String userPw , String boardPw )
    {

        if (userPw == boardPw)
        {
            return true;
        }

        else
        {
          ErrorGo("비밀번호를 확인해주세요" , "Board_Main.aspx");
            return false;
        }
    }




    // 검색기준 버튼 눌렀을 때 실행되는 함수
    protected void ddlSearchOption_SelectedIndexChanged(object sender , EventArgs e)
    {
        // 검색기준
        if(ddlSearchOption.SelectedValue == "-1")
        {
            JustGo("Board_Main.aspx");
        }

    }
}
 