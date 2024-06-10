<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Student manager</h1>
        <p class="lead">This is a student list</p>
        <p><a class="btn btn-primary btn-lg" href="/AddStudent.aspx">+Add</a></p>
    </div>

<asp:GridView ID="studentsTable" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="id">
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" />
        <asp:TemplateField HeaderText="Full Name">
                <ItemTemplate>
                    <asp:Label ID="lblFullName" runat="server" Text='<%# Bind("fullName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFullName" runat="server" Text='<%# Bind("fullName") %>'></asp:TextBox>
                </EditItemTemplate>
        </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                </EditItemTemplate>
        </asp:TemplateField>
                <asp:TemplateField HeaderText="Avatar">
                <ItemTemplate>
                    <asp:Label ID="lblAvatar" runat="server" Text='<%# Bind("avatar") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAvatar" runat="server" Text='<%# Bind("avatar") %>'></asp:TextBox>
                </EditItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
</asp:GridView>


</asp:Content>
