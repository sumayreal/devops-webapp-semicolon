<%@ Page Title = "Main page" Language = "C#" MasterPageFile = "~/Homepage.master" AutoEventWireup = "true"  EnableEventValidation="False" Inherits = "Board" Codebehind="Board_Main.aspx.cs" %>


<%-- 여기에 Content 컨트롤 추가 --%>
<asp:Content ID = "main" ContentPlaceHolderID = "main" runat = "server">

    <form id = "mainForm" runat = "server">

    <!--현재 페이지 수, 정렬 기준 설정 가져오는 부분-->
    <div class = "upper_page_detail">
        <asp:Label ID = "lblPageDetail" runat = "server" Text = "Label"></asp:Label>
        <asp:DropDownList ID = "ddlPageNumPerPage" runat = "server" AutoPostBack="true" EnableViewState="true"  OnSelectedIndexChanged="ddlPageNumPerPage_SelectedIndexChanged" style="height: 17px; width: 96px " >
            <asp:ListItem Value="10">10개씩 보기</asp:ListItem>
            <asp:ListItem Value="15">15개씩 보기</asp:ListItem>
            <asp:ListItem Value="20">20개씩 보기</asp:ListItem>
        </asp:DropDownList>
         <asp:DropDownList ID = "ddlTitleOrder" runat = "server" AutoPostBack="true" EnableViewState="true"   OnSelectedIndexChanged="ddlTitleOrder_SelectedIndexChanged">
            <asp:ListItem Value="-1">제목</asp:ListItem>
            <asp:ListItem Value="0">가나다순</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID = "ddlWriterOrder" runat = "server" AutoPostBack="true" EnableViewState="true"  OnSelectedIndexChanged="ddlWriterOrder_SelectedIndexChanged">
            <asp:ListItem Value="-1">작성자명</asp:ListItem>
            <asp:ListItem Value="0">가나다순</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID = "ddlDateOrder" runat = "server" AutoPostBack="true" EnableViewState="true"  OnSelectedIndexChanged="ddlDateOrder_SelectedIndexChanged">
            <asp:ListItem Value="-1">날짜</asp:ListItem>
            <asp:ListItem Value="0">최신순</asp:ListItem>
            <asp:ListItem Value="1">오래된순</asp:ListItem>
        </asp:DropDownList>
    
    </div>

    <div class = "board_overview">
    <!--글 가져오는 부분 -->
    
        <asp:GridView ID = "boardList" runat = "server" class = "boardList_table" AutoGenerateColumns = "false" CellPadding = "3" AllowPaging = "true" PageSize = "10" HorizontalAlign="Center" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" OnPageIndexChanging = "OnPageIndexChanged" >
            <RowStyle BackColor = "#EFF3FB" />
            <AlternatingRowStyle BackColor = "White" />

            <HeaderStyle BackColor = "#E68A7B" Font-Bold = "True" />
            <PagerStyle BackColor = "#FFFFFF" HorizontalAlign = "Left" />
        
            <EmptyDataTemplate>
                게시물이 없습니다.
            </EmptyDataTemplate>

            <Columns>
               <asp:templatefield HeaderText="삭제" Visible="false">
                    <itemtemplate>
                        <asp:checkbox ID="board_checkbox" runat="server"></asp:checkbox>
                    </itemtemplate>
                </asp:templatefield>
                <asp:BoundField DataField = "board_id" HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide"/> 
                <asp:BoundField DataField = "board_title" HeaderText = "title" 
                SortExpression = "boardTitle"  />
                <asp:BoundField DataField = "board_writer" HeaderText = "writer" 
                SortExpression = "boardWriter" />   
                <asp:BoundField DataField = "board_date" HeaderText = "날짜" 
                SortExpression = "boardDate" DataFormatString="{0:MM/dd/yyyy}"/>   
                <asp:BoundField DataField = "board_pw" HeaderStyle-CssClass = "Hide" ItemStyle-CssClass = "Hide"/> 

            </Columns>
 

        </asp:GridView>
    </div>
    
    <!-- 검색, 작성, 삭제 할 수 있는 부분-->
    <div class = "bottom_page_detail">
         <asp:DropDownList ID = "ddlSearchOption" runat = "server" AutoPostBack="True" EnableViewState="true" OnSelectedIndexChanged="ddlSearchOption_SelectedIndexChanged" >
            <asp:ListItem>검색기준</asp:ListItem>
            <asp:ListItem Value="0">제목</asp:ListItem>
            <asp:ListItem Value="1">작성자명</asp:ListItem>
            <asp:ListItem Value="2">내용</asp:ListItem>
            <asp:ListItem Value="3">전체</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID = "txtSearchOptionDetail" runat = "server"></asp:TextBox>
        <asp:Button ID = "btnSearch" runat = "server" Text = "검색" OnClick = "search" class = "boardInfo"/>
        <asp:Button ID = "btnWrite" runat = "server" Text = "작성" OnClick = "write" class = "boardInfo" />
        <asp:Button ID = "btnDelete" runat = "server" Text = "삭제" OnClick = "delete" class = "boardInfo"/>
       <asp:TextBox ID = "txtPw" runat = "server" Visible="false"></asp:TextBox>

    </div>

    </form>
 
    </asp:Content>
