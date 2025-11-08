<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="OrderForm.aspx.cs" Inherits="Fricke_ITM_325_Assignment_4.ContentPages.OrderForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>order form</title>
    <link rel="stylesheet" href="/Styles/Wide.css?v=1.0.0" /> 
    <link rel="stylesheet" href="/Styles/OrderMenu.css?v=1.0.0" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="OrderMenu">
            <form id="form1" runat="server">
                <div id="OrderForm">
                    <h2>Pizza Order Form</h2>
                        <div id="OrderFormColumns">
                            <div class="OrderFormColumn">
                                <label for="Order">Order</label>
                                <!--order populates here-->
                                <asp:Label ID="OrderSummary" runat="server" />
                                <asp:Label ID="PricingSummary" runat="server" />
                            </div>
                            <div class="OrderFormColumn">
                                <label for="Size" >Size:</label>
                                <asp:RadioButtonList ID="Size" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Size_IndexChange">
                                    <asp:ListItem Text="Personal" Value="pr" />
                                    <asp:ListItem Text="Small" Value="sm" />
                                    <asp:ListItem Text="Medium" Value="md" />
                                    <asp:ListItem Text="Large" Value="lg" />
                                    <asp:ListItem Text="Family" Value="fm" />
                                </asp:RadioButtonList>
                            </div>
                            <div class="OrderFormColumn">
                                <label for="Crust">Crust:</label>
                                <asp:RadioButtonList ID="Crust" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Crust_IndexChange">
                                    <asp:ListItem Text="Crispy Thin" Value="ct" />
                                    <asp:ListItem Text="Thin" Value="th" />
                                    <asp:ListItem Text="Regular" Value="rg" />
                                    <asp:ListItem Text="Deep Dish (+2.95)" Value="dd" />
                                </asp:RadioButtonList>
                            </div>
                            <div class="OrderFormColumn">
                                <label for="Toppings">Toppings (2$ each)</label>
                                <asp:CheckBoxList ID="Toppings" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Toppings_ArrayUpdate">
                                    <asp:ListItem Text="2x Cheese" Value="xc"></asp:ListItem>
                                    <asp:ListItem Text="Pepperoni" Value="pi"></asp:ListItem>
                                    <asp:ListItem Text="Italian Sausage" Value="is"></asp:ListItem>
                                    <asp:ListItem Text="Ham" Value="hm"></asp:ListItem>
                                    <asp:ListItem Text="Olives" Value="ov"></asp:ListItem>
                                    <asp:ListItem Text="Green Pepper" Value="gp"></asp:ListItem>
                                    <asp:ListItem Text="Red Pepper" Value="rp"></asp:ListItem>
                                    <asp:ListItem Text="Onion" Value="on"></asp:ListItem>
                                    <asp:ListItem Text="Mushroom" Value="mh"></asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </div> 
                    <asp:Button ID="CancelOrderButton" runat="server" CssClass="CancelOrderButton" CausesValidation="false" Text="Cancel Order" OnClick="CancelOrderButton_Click" />
                </div>
           </form>
      </div>
</asp:Content>
