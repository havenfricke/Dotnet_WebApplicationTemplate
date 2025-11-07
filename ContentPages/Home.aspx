<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Fricke_ITM_325_Assignment_4.ContentPages.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Home</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <article>
    <h2><a href="/ContentPages/Article1.aspx">Post One</a></h2>
    <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec
        condimentum, urna a tristique imperdiet, arcu nisl ullamcorper
        lorem, vitae placerat justo augue sed nisi. Curabitur ut lectus
        vitae nisl gravida luctus.
    </p>
    </article>

    <hr />

    <article>
        <h2><a href="/ContentPages/Article2.aspx">Post Two</a></h2>
        <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer
            tempus, tortor sit amet finibus cursus, velit neque volutpat
            libero, a ultricies nulla odio nec sem. Vivamus ac nibh eget
            metus congue pulvinar.
        </p>
    </article>

    <hr />

    <article>
        <h2><a href="/ContentPages/Article3.aspx">Post Three</a></h2>
        <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed
            aliquam, turpis in dapibus accumsan, est augue commodo libero,
            vitae dictum eros magna eu nisi. Aenean fermentum elit sed
            placerat rutrum.
        </p>
    </article>
</asp:Content>
