<%@ Page Title="" Language="C#" MasterPageFile="~/Homepage.master" AutoEventWireup="true" Inherits="Board_Write" Codebehind="Board_Write.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
    <form id="boardForm" runat="server"  action = "Board_Write_Save.aspx" method = "post">
    <div id = "board_write_content">
         <input type="hidden" id="isUpdate" name="isboardUpdate" value="<%=isUpdate %>"/>
        <input type="hidden" id="boardId" name="BoardId" value="<%=boardid %>"/>
        <div class = "board_write_content_row">
            <asp:Label ID="lblBoardTitle" runat="server" Text="제목" class = "boardInfo"></asp:Label>
            <input type="text" id="txtBoardTitle" name="boardTitle" maxlength="39" style="width: 300px" value= "<%=title %>"/>
        </div><br />
         <div class = "board_write_content_row">
            <asp:Label class = "boardInfo" ID="lblBoardWriter" runat="server" Text="작성자"></asp:Label>
            <input type="text" id="txtBoardWriter" name="boardWriter" maxlength="9" value= "<%=writer %>" <%=writerDisabled %>/>
            <asp:Label ID="lblBoardPw" runat="server" Text="비밀번호" class = "boardInfo"></asp:Label>
            <input type="text" id="txtBoardPw" name="boardPw" maxlength="4" value= "<%=pw %>"  <%=pwDisabled %>/>
         </div><br />
         <div class = "board_write_content_row">
            <asp:Label ID="lblBoardContent" runat="server" Text="내용" class = "boardInfo" ></asp:Label>
            <textarea id="txtBoardContent" name="boardContent" maxlength="199" style="width: 300px; height : 200px;"> <%=content %></textarea>
        </div><br />
         <div class = "board_write_content_row">
             <asp:Button ID="btnboardWrite" runat="server" Text="작성"  class = "boardInfo" onClientClick = "return board_write(this)" UseSubmitBehavior="false" />
             <asp:Button ID="btnboardCancel" runat="server" Text="취소"   class = "boardInfo"  OnClientClick="return cancel()" UseSubmitBehavior="false"/>
         </div>
        

        <script type =" text/javascript">
    
        </script>
    </div>
    </form>
</asp:Content>

