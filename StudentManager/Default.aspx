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
        <asp:BoundField DataField="fullName" HeaderText="Full Name" />
        <asp:BoundField DataField="email" HeaderText="Email" />
                     <asp:TemplateField HeaderText="Avatar">
                    <ItemTemplate>
                        <img src='<%# ResolveUrl(Eval("avatar").ToString()) %>' alt="Avatar" style="width: 50px; height: 50px;" />
                    </ItemTemplate>
                </asp:TemplateField>
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
</asp:GridView>


</asp:Content>
