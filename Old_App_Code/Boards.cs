using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Board의 요약 설명입니다.
/// </summary>
public class Boards
{
    
    private int boardId; // 게시글 id
    private String boardTitle; //게시글 제목
    private String boardWriter; //게시글 작성자
    private DateTime boardDate; //게시글 작성한 시간
    private String boardContent; //게시글 내용
    private String boardPw;

    public Boards()
    {

    }


    // userId, boardTitle, boardWriter, boardDate, boardContent로 객체 생성해주는 메서드
	public Boards(int boardId , String boardTitle , String boardWriter , DateTime boardDate , String boardContent)
	{
        this.boardId = boardId;
        this.boardTitle = boardTitle;
        this.boardWriter = boardWriter;
        this.boardDate = boardDate;
        this.boardContent = boardContent;
	}

    // boardId로 set
    public void setBoardId(int boardId)
    {
        this.boardId = boardId;
    }

    // this.boardId return
    public int getBoardId()
    {
        return this.boardId;
    }
    
    // boardTitle로 set
    public void setBoardTitle(String boardTitle)
    {
        this.boardTitle = boardTitle;
    }

    // this.boardTitle return
    public String getBoardTitle()
    {
        return this.boardTitle;
    }

    // boardWriter로 set
    public void setBoardWriter(String boardWriter)
    {
        this.boardWriter = boardWriter;
    }

    // this.boardWriter return
    public String getBoardWriter()
    {
        return this.boardWriter;
    }

    // boardDate로 set
    public void setBoardDate(DateTime boardDate)
    {
        this.boardDate = boardDate;
    }

    // this.boardDate return
    public DateTime getBoardDate()
    {
        return this.boardDate;
    }

    // boardContent로 set
    public void setBoardContent(String boardContent)
    {
        this.boardContent = boardContent;
    }

    // this.boardContent return
    public String getBoardContent()
    {
        return this.boardContent;
    }

    // boardPw로 set
    public void setBoardPw(String boardPw)
    {
        this.boardPw = boardPw;
    }

    // this.boardPw return
    public String getBoardPw()
    {
        return this.boardPw;
    }
    
}