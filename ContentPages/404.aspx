<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="Fricke_ITM_325_Assignment_4.ContentPages._404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Page Not Found (404)</title>
<meta http-equiv="refresh" content="5;url=/ContentPages/Home.aspx" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>404 — Page Not Found</h1>
    <h2>Sorry, we couldn’t find that page.</h2>
    <p>You’ll be redirected to the home page in 5 seconds.</p>
    <p><a href="/ContentPages/Home.aspx">Go now</a></p>
</asp:Content>
