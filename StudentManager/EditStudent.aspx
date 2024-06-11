<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditStudent.aspx.vb" Inherits="EditStudent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
      <script type="text/javascript">
        function previewImage(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    imgAvatar.src = URL.createObjectURL(input.files[0])
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
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
        <label for="imgAvatar">Avatar:</label>
        <asp:Image ID="imgAvatar"   runat="server" style="width: 50px; height: 50px;"/>
        <label for="fileAvatar">Avatar:</label>
        <asp:FileUpload ID="fileAvatar" runat="server" CssClass="form-control" OnChange="previewImage(this)"></asp:FileUpload>
    </div>
    <div>
        <asp:Button ID="btnEditStudent" runat="server" Text="Save" CssClass="btn btn-primary btn-lg" OnClick="btnEditStudent_Click" />
    </div>
</form>
    </body>
</html>
