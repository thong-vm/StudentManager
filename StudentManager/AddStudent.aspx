<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddStudent.aspx.vb" Inherits="AddStudent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <head>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
</head>
<body>
<form id="formAddStudent" runat="server" class="container mt-4">
    <div class="form-group">
        <label for="txtName">Full Name:</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="txtEmail">Email:</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="txtAvatar">Avatar:</label>
        <asp:TextBox ID="txtAvatar" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="btnAddStudent" runat="server" Text="Add Student" CssClass="btn btn-primary" OnClick="btnAddStudent_Click" />
    </div>
</form>

</body>
</html>


