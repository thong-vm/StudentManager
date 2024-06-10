<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddStudent.aspx.vb" Inherits="AddStudent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="formAddStudent" runat="server">
    <div>
        <label for="txtName">Full Name:</label>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
    </div>
    <div>
        <label for="txtEmail">Email:</label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    </div>
    <div>
        <label for="txtPhoneNumber">Phone Number:</label>
        <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="btnAddStudent" runat="server" Text="Add Student" OnClick="btnAddStudent_Click" />
    </div>
</form>

</body>
</html>
