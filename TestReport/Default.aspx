<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TestReport._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        AUTOMATION TEST REPORT</h2>
    <p>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </p>
<p>
        &nbsp;</p>
    </asp:Content>
