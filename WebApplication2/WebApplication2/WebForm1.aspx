<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication2.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divform" runat="server">
    
   
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
        BorderColor="#999999" Font-Names="Verdana" Font-Size="8pt" 
        ForeColor="Black" Height="241px" 
        ondayrender="Calendar1_DayRender" Width="328px" 
        onselectionchanged="Calendar1_SelectionChanged" 
        onprerender="Calendar1_PreRender" CellPadding="4" DayNameFormat="Shortest">
        <DayHeaderStyle Font-Bold="True" Font-Size="7pt" BackColor="#CCCCCC" />
        <NextPrevStyle 
            VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#808080" Wrap="False" />
        <SelectedDayStyle BackColor="#666666" ForeColor="White" Font-Bold="True" />
        <SelectorStyle BackColor="#CCCCCC" />
        <TitleStyle BackColor="#999999" BorderColor="Black" 
            Font-Bold="True" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <WeekendDayStyle BackColor="#FFFFCC" />
    </asp:Calendar>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:DropDownList ID="DropDownList1" runat="server" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>Travail journée</asp:ListItem>
        <asp:ListItem>Travail demi-journée</asp:ListItem>
        <asp:ListItem>Absence</asp:ListItem>
        <asp:ListItem>Absence demi-journée</asp:ListItem>
    </asp:DropDownList>
     </div>
    </form>
</body>
</html>
