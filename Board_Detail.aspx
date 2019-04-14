<%@ Page Title="" Language="C#" MasterPageFile="~/Homepage.master" AutoEventWireup="true" Inherits="Board_Detail" Codebehind="Board_Detail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
 
   <form id = "updateForm" runat = "server" >
    <div id = "board_write_content">

        <div class = "board_write_content_row">
            <asp:Label ID ="lblBoardTitle" runat ="server" Text="제목" class = "boardInfo"></asp:Label>
            <asp:Label ID ="txtBoardTitle" runat ="server" style="width: 300px" />
        </div><br />
         <div class = "board_write_content_row">
            <asp:Label class = "boardInfo" ID="lblBoardWriter" runat="server" Text="작성자"></asp:Label>
            <asp:Label ID="txtBoardWriter" runat="server" />
            <asp:Label ID="lblBoardPw" runat="server" Text="비밀번호" class = "boardInfo"></asp:Label>
            <asp:Label ID="txtBoardPw" runat="server"/>
         </div><br />
         <div class = "board_write_content_row">
            <asp:Label ID="lblBoardContent" runat="server" Text="내용" class = "boardInfo"></asp:Label>
            <asp:Label ID="txtBoardContent" runat="server" style="width: 300px; height : 200px; background-color : white;"  />
        </div><br />
         <div class = "board_write_content_row">
                 <asp:TextBox ID = "txtupdatePw" runat = "server" Visible="false"></asp:TextBox>
             <asp:Button ID="btnboardUpdate" runat="server" Text="수정"  class = "boardInfo"  OnClick = "updateBoard"  UseSubmitBehavior="false"/>
             <asp:Button ID="btnboardBack" runat="server" Text="취소"   class = "boardInfo" OnClick = "cancel"  UseSubmitBehavior="false"/>
         </div>


        <script type =" text/javascript">
    
        </script>
    </div>
       </form>
</asp:Content>

