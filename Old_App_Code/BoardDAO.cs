using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Board의 요약 설명입니다.
/// </summary>
public class BoardDAO : CDatabase
{
    CDatabase db;

    public BoardDAO()
    {
    }

    // 전체 board 데이터 가져오는 함수
    public DataTable selectAll()
    {
        db = new CDatabase();
        DataTable table = new DataTable();

        string sql = "SELECT * FROM Board Order BY board_date DESC, board_id DESC";


        SqlDataReader reader = db.GetQueryResult(sql);
        table.Load(reader);

        db.Dispose();
        return table;
    }

    // order에 따라 정렬한 데이터 가져오는 함수
    public DataTable selectAll(String order)
    {
        db = new CDatabase();
        DataTable table = new DataTable();

        string sql = "SELECT * FROM Board Order BY "+ order +";";


        SqlDataReader reader = db.GetQueryResult(sql);
        table.Load(reader);

        db.Dispose();
        return table;
    }


    // 페이지와 개수에 따라 데이터 가져오는 함수 
    public SqlDataReader selectAll(int pageNum , int numberPage)
    {
        db = new CDatabase();

        string sql = "SELECT * FROM Board Order BY board_date DESC"
            + " OFFSET " + ((pageNum-1)*numberPage).ToString() + " ROWS;";


        SqlDataReader reader = db.GetQueryResult(sql);
        return reader;
    }

    // boardId에 따라 데이터 삭제해주는 함수 - 삭제하려는 개수가 하나일 경우
    public Boolean Delete (String boardId)
    {
        db = new CDatabase();
        string sql = "DELETE Board WHERE board_id =" + boardId;

        Boolean result = db.ExecuteSQL(sql);
        db.Dispose();

        return result;
    }

    // boardId에 따라 데이터 삭제해주는 함수 - 삭제하려는 개수가 여러개일 경우
    public Boolean Delete ( ArrayList boardNumList)
    {
        db = new CDatabase();
        Boolean result = false;
        String sql = "";
        foreach (object boardId in boardNumList)
        {
            string boardnum = (string)boardId;
            sql += "DELETE Board WHERE board_id =" + boardnum + "\n";
        }
        result = db.ExecuteSQL(sql);
        db.Dispose();
        return result;
    }

    // boardId에 따라 데이터 가져오는 함수 
    public Boards Select ( String boardId )
    {
        db = new CDatabase();
        Boards board = new Boards();

        string sql = "SELECT * FROM Board WHERE board_id =" + boardId + ";";

        SqlDataReader reader = db.GetQueryResult(sql);


        while (reader.Read())
        {
            board.setBoardId(reader.GetInt32(0));
            board.setBoardTitle(reader.GetString(1));
            board.setBoardWriter(reader.GetString(2));
            board.setBoardDate(reader.GetDateTime(3));
            board.setBoardContent(reader.GetString(4));
            board.setBoardPw(reader.GetString(5));
        }

        db.Dispose();

        return board;
    }

    // 제목 검색해주는 쿼리문
    public DataTable SelectTitle(String title)
    {
        db = new CDatabase();
        DataTable table = new DataTable();

        string sql = "SELECT * FROM Board WHERE board_title LIKE '%" + title + "%';";

        SqlDataReader reader = db.GetQueryResult(sql);
        table.Load(reader);

        db.Dispose();
        return table;
    }

    // 내용 검색해주는 쿼리문
    public DataTable SelectContent(String content)
    {
        db = new CDatabase();
        DataTable table = new DataTable();

        string sql = "SELECT * FROM Board WHERE board_content LIKE '%" + content + "%';";

        SqlDataReader reader = db.GetQueryResult(sql);
        table.Load(reader);

        db.Dispose();
        return table;
    }

    // 작성자 검색해주는 쿼리문
    public DataTable SelectWriter(String writer)
    {
        db = new CDatabase();
        DataTable table = new DataTable();

        string sql = "SELECT * FROM Board WHERE board_writer LIKE '%" + writer + "%';";

        SqlDataReader reader = db.GetQueryResult(sql);
        table.Load(reader);

        db.Dispose();
        return table;
    }

    // 전체 검색해주는 쿼리문
    public DataTable SelectSum(String all)
    {
        db = new CDatabase();
        DataTable table = new DataTable();

        string sql = "SELECT * FROM Board WHERE board_title LIKE '%" + all + "%'"
            +"UNION SELECT * FROM Board WHERE board_content LIKE '%" + all +"%'"
            +"UNION SELECT * FROM Board WHERE board_writer LIKE '%" + all + "%';";

        SqlDataReader reader = db.GetQueryResult(sql);
        table.Load(reader);

        db.Dispose();
        return table;
    }

    // board 삽입해주는 함수 
    public Boolean Insert(String boardTitle , String boardWriter , String boardContent , String boardPw)
    {
        CDatabase db = new CDatabase();


        string sql = "";
        sql = "INSERT INTO Board(board_title,board_writer,board_date,board_content,board_pw)";
        sql += "VALUES ('" + boardTitle + "','" + boardWriter + "',CAST( GETDATE() AS DATE),'" + boardContent + "','" + boardPw + "')";

        Boolean result =  db.ExecuteSQL(sql);

        db.Dispose();

        return result;
    }

    // board 수정해주는 함수
    public Boolean Update(String boardTitle , String boardWriter , String boardContent , String boardId)
    {
        CDatabase _db = new CDatabase();


        string sql = "";
        sql = "UPDATE Board SET board_title='" + boardTitle + "',board_date = CAST( GETDATE() AS DATE), board_content = '" + boardContent + "' WHERE board_id = " + boardId;
   

        Boolean result = _db.ExecuteSQL(sql);

        _db.Dispose();

        return result;
    }

}