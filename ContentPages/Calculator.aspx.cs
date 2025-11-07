using System;
using System.Web.UI.WebControls;

namespace Fricke_ITM_325_Assignment_4.ContentPages
{
    public partial class Calculator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // GPT reccomended I put this in my page load to
            // fix the 500 error I was getting related to validator display.
            // Here's what it said - This line tells ASP.NET “Use the (built-in)
            // validation rendering instead of jQuery-based unobtrusive mode.”
            // ASP.NET will automatically include the classic validation scripts
            // (WebUIValidation.js) so that your RequiredFieldValidator, ValidationSummary,
            // etc. work out of the box. I'm not sure if this was taught in class but if it was
            // not, it should have. The way you are asking us to handle the errors needs to be
            // specified. Without this specification, the entire system throws a 500 error.
            Page.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            CalculatorValSum.Font.Size = FontUnit.Large;
            CalculateButton.Font.Size = FontUnit.Point(12);
            ClearButton.Font.Size = FontUnit.Point(12);
        }

        protected void CalculateButton_Click(object sender, EventArgs e)
        {
            Page.Validate("calc");

            if (!Page.IsValid)
            {
                OutputTextBox.Text = string.Empty;
                return;
            }

            if (!float.TryParse(FirstNumberTextBox.Text, out float a) || !float.TryParse(SecondNumberTextBox.Text, out float b))
            {
                OutputTextBox.Text = string.Empty;
                return;
            }

            float result;
            string symbol;

            switch (Operators.SelectedValue)
            {
                case "Add":
                    result = a + b;
                    symbol = "+";
                    break;

                case "Sub":
                    result = a - b;
                    symbol = "-";
                    break;

                case "Mul":
                    result = a * b;
                    symbol = "*";
                    break;

                case "Div":
                    if (b == 0)
                    {
                        OutputTextBox.Text = string.Empty;
                        return;
                    }
                    result = a / b;
                    symbol = "/";
                    break;

                default:
                    OutputTextBox.Text = string.Empty;
                    return;
            }

            // Round for more presice calculation
            a = (float)Math.Round(a, 2);
            b = (float)Math.Round(b, 2);
            result = (float)Math.Round(result, 2);

            OutputTextBox.Text = $"{a} {symbol} {b} = {result}";
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            Operators.ClearSelection();
            Response.Redirect("/ContentPages/Calculator.aspx");
        }
    }
}
