<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Calculator.aspx.cs" Inherits="Fricke_ITM_325_Assignment_4.ContentPages.Calculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Calculator</title>
    <link rel="stylesheet" href="/Styles/Calculator.css?v=1.0.0" /> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div id="Calculator">
            <h2>Calculator</h2>

            <div class="CalculatorRow">
                <label for="FirstNumberTextBox">First Number:</label>
                <asp:TextBox ID="FirstNumberTextBox" runat="server" />
                <asp:RequiredFieldValidator 
                    ID="ReqVal1" 
                    runat="server"
                    ControlToValidate="FirstNumberTextBox"
                    ErrorMessage="First Number is required."
                    Text="*" 
                    ForeColor="Red" 
                    Font-Size="Large"
                    Display="Dynamic" 
                    ValidationGroup="calc" 
                    Type="Double"
                    Operator="DataTypeCheck"
                />
                <asp:CompareValidator
                    ID="CmpVal1" 
                    runat="server"
                    ControlToValidate="FirstNumberTextBox"
                    Operator="DataTypeCheck"
                    Type="Double"
                    ErrorMessage="First Number must be a valid number."
                    Text="*" 
                    ForeColor="Red" 
                    Font-Size="Large"
                    Display="Dynamic" 
                    ValidationGroup="calc" />
            </div>

            <div class="CalculatorRow">
                <label for="SecondNumberTextBox">Second Number:</label>
                <asp:TextBox ID="SecondNumberTextBox" runat="server" />
                <asp:RequiredFieldValidator 
                    ID="ReqVal2" 
                    runat="server"
                    ControlToValidate="SecondNumberTextBox"
                    ErrorMessage="Second Number is required."
                    Text="*" 
                    ForeColor="Red" 
                    Font-Size="Large"
                    Display="Dynamic" 
                    ValidationGroup="calc" 
                    Type="Double"
                    Operator="DataTypeCheck"
                 />
                <asp:CompareValidator
                    ID="CmpVal2" 
                    runat="server"
                    ControlToValidate="SecondNumberTextBox"
                    Operator="DataTypeCheck"
                    Type="Double"
                    ErrorMessage="Second Number must be a valid number."
                    Text="*" 
                    ForeColor="Red" 
                    Font-Size="Large"
                    Display="Dynamic" 
                    ValidationGroup="calc" />
            </div>

            <div class="CalculatorRow">
                <label>Operators:</label>
                <div id="Operator">
                    <asp:RadioButtonList ID="Operators" runat="server">
                        <asp:ListItem Text="Addition (+)" Value="Add" />
                        <asp:ListItem Text="Subtraction (-)" Value="Sub" />
                        <asp:ListItem Text="Multiplication (*)" Value="Mul" />
                        <asp:ListItem Text="Division (/)" Value="Div" />
                    </asp:RadioButtonList>
                </div>
                <asp:RequiredFieldValidator 
                    ID="ReqVal3" 
                    runat="server"
                    ControlToValidate="Operators"
                    InitialValue=""
                    ErrorMessage="Please select an operator."
                    Text="*" 
                    ForeColor="Red" 
                    Font-Size="Large"
                    Display="Dynamic" 
                    ValidationGroup="calc" 
                />
            </div>

            <div id="CalculatorOutputRow">
                <label for="OutputTextBox">Output:</label>
                <asp:TextBox ID="OutputTextBox" runat="server" ReadOnly="true" />
            </div>

            <asp:ValidationSummary ID="CalculatorValSum" runat="server" HeaderText="Error Messages" ForeColor="Red" DisplayMode="BulletList" ValidationGroup="calc" />

            <div id="Buttons">
                <asp:Button ID="CalculateButton" runat="server" Text="Calculate" CssClass="CalcButton" OnClick="CalculateButton_Click" ValidationGroup="calc" />
                <asp:Button ID="ClearButton" runat="server" Text="Clear" CssClass="CalcButton" CausesValidation="false" OnClick="ClearButton_Click" />
            </div>
        </div>

</asp:Content>
