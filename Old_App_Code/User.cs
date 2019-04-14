using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// User의 요약 설명입니다.
/// </summary>
public class User
{
    private String userId; //사용자 id
    private String userPw; //사용자 비밀번호
    private String userName; //사용자 이름

    // userId, userPw, userName로 객체 생성해주는 메서드
    public User (String userId, String userPw, String userName)
	{
        this.userId = userId;
        this.userPw = userPw;
        this.userName = userName;
	}

    // userId로 set
    public void setUserId (String userId)
    {
        this.userId = userId;
    }

    // this.usreId return
    public String getUserId ()
    {
        return this.userId;     
    }

    // userPw로 set
    public void setUserPw (String userPw)
    {
        this.userPw = userPw;
    }

    // this.userPw return
    public String getUserPw ()
    {
        return this.userPw;
    }

    // userName로 set
    public void setUserName (String userName)
    {
        this.userName = userName;
    }

    // this.userName return
    public String getUserName ()
    {
        return this.userName;
    }
}