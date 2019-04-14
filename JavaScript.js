// 작성 버튼을 눌렀을 때 실행되는 함수 
function board_write(target)
{


    var boardForm = document.getElementById("boardForm");

    
    if (!checkTitle(boardForm.boardTitle.value)) 
    {
        return false;
    }

    else if (!checkWriter(boardForm.boardWriter.value))
    {
        return false;
    }

    else if (!checkPw(boardForm.boardPw.value))
    {
        return false;
    }

    else if (!checkContent(boardForm.boardContent.value))
    {
        return false;
    }

    else {
        boardForm.submit();
    }

   
    
}

// 취소 버튼 눌렀을 때 실행되는 함수
function cancel()
{
    window.location.href = "/semicolon/Board_Main.aspx";
}

//제목 체크해주는 함수
function checkTitle(text)
{
    if (text.length === 0) {
        alert("제목을 입력해주세요");
        return false;
    }

    if (text.length > 40) {
        alert("제목은 40글자까지만 가능합니다");
        return false;
    }

    else {
        return true;
    }
    
}

// 작성자 체크해주는 함수
function checkWriter(text)
{
    if (text.length === 0)
    {
        alert("작성자를 입력해주세요");
        return false;
    }

    if (text.length > 10) {
        alert("작성자는 10글자까지만 가능합니다");
        return false;
    }

    else {
        return true;
    }
}

// 비밀번호 체크해주는 함수
function checkPw(text)
{

    
    if (text.length === 0)
    {
        alert("비밀번호를 입력해주세요");
        return false;
    }

    var filter = /^[0-9]*$/
    if (!filter.test(text) || text.length !== 4)
    {
        alert("비밀번호는 숫자만 가능하며 4글자입니다. 확인해주세요");
        return false;
    }

    else
    {
        return true;
    }


}

// 내용 체크해주는 함수
function checkContent(text)
{
    if (text.length === 0)
    {
        alert("내용을 입력해주세요");
        return false;
    }

    if (text.length > 100)
    {
        alert("내용은 100글자까지만 가능합니다");
        return false;
    }

    else {
        return true;
    }
}


function select () {
   
}