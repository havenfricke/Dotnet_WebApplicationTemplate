using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fricke_ITM_325_Assignment_4.ContentPages
{
    public partial class OrderForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // set once
            {
                OrderSummary.Text = "Please select a size and crust type.";
                OrderSummary.ForeColor = System.Drawing.Color.Red;
                OrderSummary.Font.Size = FontUnit.Small;
                PricingSummary.ForeColor = System.Drawing.Color.Green;
                PricingSummary.Font.Size = FontUnit.Small;
                Size.Font.Size = FontUnit.Small;
                Crust.Font.Size = FontUnit.Small;
                Toppings.Font.Size = FontUnit.Small;
            }
        }

        protected void UpdateOrder()
        {
            // Items
            string size = Size.SelectedItem != null ? Size.SelectedItem.Text : "";
            string crust = Crust.SelectedItem != null ? Crust.SelectedItem.Text : "";
            var toppings = new List<string>();

            // Prices
            double sizePrice = 0;
            double crustPrice = 0;
            double toppingsPrice = 0;

            // Size
            switch (Size.SelectedValue)  
            {
                case "pr": 
                    sizePrice = 7.95; 
                    break;
                case "sm": 
                    sizePrice = 9.95; 
                    break;
                case "md": 
                    sizePrice = 12.95; 
                    break;
                case "lg": 
                    sizePrice = 17.95;
                    break;
                case "fm": 
                    sizePrice = 22.95; 
                    break;
            }

            // Crust
            switch (Crust.SelectedValue) 
            {
                case "ct":
                case "th":
                case "rg": 
                    crustPrice = 0.00; 
                    break;
                case "dd": 
                    crustPrice = 2.95; 
                    break;
            }

            // Toppings
            foreach (ListItem t in Toppings.Items)
            {
                if (t.Selected)
                {
                    toppings.Add(t.Text);
                }
            }


            toppingsPrice = toppings.Count * 2.00;

            if (!string.IsNullOrEmpty(size) && !string.IsNullOrEmpty(crust))
            {
                var culture = new CultureInfo("en-US"); 

                // Base Order
                OrderSummary.ForeColor = System.Drawing.Color.Blue;
                OrderSummary.Text = "<div id=\"BasePizza\">";
                OrderSummary.Text += "<u>Base Pizza</u><br/>";
                OrderSummary.Text += $"Size: {size}<br/>";
                OrderSummary.Text += $"Crust: {crust}<br/>";
                OrderSummary.Text += $"<br/>";
                OrderSummary.Text += "<u>Toppings</u><br/>";
                OrderSummary.Text += toppings.Count > 0 ? string.Join("<br/>", toppings) : "None selected";
                OrderSummary.Text += "</div>";
                
                // Price Calc
                double subtotal = sizePrice + crustPrice + toppingsPrice;
                double tax = Math.Round(subtotal * 0.06, 2); // round 2 decimal places
                double total = Math.Round(subtotal + tax, 2); // same here

                // Receipt
                PricingSummary.Text = $"<br/>";
                PricingSummary.Text += "<div id=\"Pricing\">";
                PricingSummary.Text += "<u>Pricing</u><br/>";
                PricingSummary.Text += $"{(subtotal - tax).ToString("C", culture)} (Base + Toppings before tax)<br/>";
                PricingSummary.Text += $"{(sizePrice + crustPrice).ToString("C", culture)} (Base Pizza)<br/>";
                PricingSummary.Text += $"{toppingsPrice.ToString("C", culture)} ({toppings.Count} Topping(s))<br/>";
                PricingSummary.Text += $"<br/>";
                PricingSummary.Text += $"{subtotal.ToString("C", culture)} Subtotal<br/>";
                PricingSummary.Text += $"{tax.ToString("C", culture)} Sales Tax (6%)<br/>";
                PricingSummary.Text += $"<div id=\"BottomLine\"></div>";
                PricingSummary.Text += $"<strong>{total.ToString("C", culture)} Total</strong>";
                PricingSummary.Text += "</div>";
            }
        }

        // Events / Listeners
        protected void Size_IndexChange(object sender, EventArgs e)
        {
            UpdateOrder();
        }

        protected void Crust_IndexChange(object sender, EventArgs e)
        {
            UpdateOrder();
        }

        protected void Toppings_ArrayUpdate(object sender, EventArgs e)
        {
            UpdateOrder();
        }

        protected void CancelOrderButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ContentPages/OrderForm.aspx");
        }
    }
}