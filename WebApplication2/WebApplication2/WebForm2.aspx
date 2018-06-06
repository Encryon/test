<%@ Page Language="C#" AutoEventWireup="true" Inherits="WebApplication2.WebForm2" Codebehind="WebForm2.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="header" style="padding-bottom: 100px; margin-top: 0px; background-color: black;"></div>
    <div id="gauche" style="padding: 100px 100px 230px; float: left; background-color: blue;">
    </div>
    <div id="center" style="padding: 10px 10px 100px; float: left; background-color: transparent;">
    
       <asp:Calendar id="Calendar1" OnDayRender=" Calendar1_DayRender" runat="server" 
            BackColor="White" BorderColor="Black" DayNameFormat="Shortest" 
            FirstDayOfWeek="Monday" Font-Names="Times New Roman" Font-Size="10pt" 
            ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" 
            onselectionchanged="Calendar1_SelectionChanged" ShowGridLines="True" 
            TitleFormat="Month" Width="400px" 
            onvisiblemonthchanged="Calendar1_VisibleMonthChanged1" >
           <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" 
               ForeColor="#333333" Height="10pt" />
           <DayStyle Width="14%" />
           <NextPrevStyle Font-Size="8pt" ForeColor="White" />
           <OtherMonthDayStyle ForeColor="#999999" />
           <%--<SelectedDayStyle BackColor="#CC3333" ForeColor="White" />--%>
           <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" 
               Font-Size="8pt" ForeColor="#333333" Width="1%" />
           <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" 
               ForeColor="White" Height="14pt" />
           <TodayDayStyle BackColor="#CCCC99" />
        </asp:Calendar>
        </div>
     <div style="padding: 40px; float: left; background-color: orange;">
        <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Selected="True"></asp:ListItem>
        <asp:ListItem Value="2">Travail journée</asp:ListItem>
        <asp:ListItem Value="3">Travail demi-journée</asp:ListItem>
        <asp:ListItem Value="0">Absence</asp:ListItem>
        <asp:ListItem Value="1">Absence demi-journée</asp:ListItem>
    </asp:DropDownList>
    </div>
    
    <div style="margin-top: 99px; margin-left: -237px; float: left; background-color: yellow;">
    
        <asp:BulletedList ID="BulletedList1" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="TYPE" DataValueField="COULEUR">
        </asp:BulletedList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ApplicationDb %>" 
            SelectCommand="LoadLegende" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
    
    </div>
    <div style="padding: 40px; margin-top: 222px; margin-left: -272px; float: left; background-color: Navy;">
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    
    
    </div>
    <input id="datemy" runat="server" type="hidden" value="" />

    
    <div id="droite" style="padding: 165px 165px 165px 100px; float: right; background-color: pink;"></div>
    <div id="footer" style="padding: 50px; margin-top: 330px; margin-bottom: 150px; background-color: black;"></div>
    </form>
</body>
</html>
