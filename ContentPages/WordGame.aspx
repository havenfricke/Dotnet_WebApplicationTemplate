<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="WordGame.aspx.cs" Inherits="Fricke_ITM_325_Assignment_4.ContentPages.WordGame" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Word Game</title>
    <link rel="stylesheet" href="/Styles/Wide.css?v=1.0.0"/>
    <link rel="stylesheet" href="/Styles/WordGame.css?v=1.0.0"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Word Game</h2>
    <h3 id="Word" runat="server"></h3>
    <p id="Message" runat="server"></p>
    <h4 id="Attempts">
        <u>Attempts</u>
        <ol id="Answers" runat="server"></ol>
    </h4>
    <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
    <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />
</asp:Content>
