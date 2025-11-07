using System;
using System.Collections.Generic;
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
            
        }

        protected void UpdateOrder(string orderValue)
        {
            string size = Size.SelectedItem != null ? Size.SelectedItem.Text : "";
            string crust = Crust.SelectedItem != null ? Crust.SelectedItem.Text : "";
            
            List<string> toppings = new List<string>();

            if (Toppings.Items.Count > 0)
            {
                foreach (ListItem t in Toppings.Items)
                {
                    toppings.Add(t.Text);
                }
            }

            if (size != "" && crust != "")
            {
                OrderSummary.Text = $"<u>Base Pizza</u><br/>\r\n"   +
                                    $"Size: {size}<br/>\r\n"        +
                                    $"Crust: {crust}<br/>\r\n"      +
                                    $"<u>Toppings</u><br/>\r\n";
                OrderSummary.Text += toppings.Count > 0 ? string.Join("<br/>\r\n", toppings) : "None selected";
            }

            if (size != "" && crust != "" && toppings.Count > 0)
            {
                OrderSummary.Text = $"<u>Base Pizza</u><br/>\r\n"   +
                                    $"Size: {size}<br/>\r\n"        +
                                    $"Crust: {crust}<br/>\r\n"      +
                                    $"<u>Toppings</u><br/>\r\n";
                OrderSummary.Text += toppings.Count > 0 ? string.Join("<br/>\r\n", toppings) : "None selected";
            }

            switch (orderValue)
            {
                //Size
                case "pr":               
                case "sm":                  
                case "md":    
                case "lg":
                    // pricing logic
                    break;
                //Crust
                case "ct":
                case "th":
                case "rg":
                case "dd":
                    // pricing logic
                    break;
                //Toppings
                case "xc":
                case "pi":
                case "is":
                case "hm":
                case "ov":
                case "gp":
                case "rp":
                case "on":
                case "mh":
                    // pricing logic
                    break;
                case "xc-r":
                case "pi-r":
                case "is-r":        
                case "hm-r":
                case "ov-r":
                case "gp-r":
                case "rp-r":
                case "on-r":
                case "mh-r":
                    // pricing logic
                    break;
                default:
                    size = null;
                    crust = null;
                    toppings = null;
                    return;
            }




            
        }

        protected void Size_IndexChange(object sender, EventArgs e)
        {
            UpdateOrder(Size.SelectedValue);
        }

        protected void Crust_IndexChange(object sender, EventArgs e)
        {
            UpdateOrder(Crust.SelectedValue);
        }

        protected void Toppings_ArrayUpdate(object sender, EventArgs e)
        {
            for (int i = 0; i < Toppings.Items.Count; i++)
            {
                if (Toppings.Items[i].Selected)
                {
                    UpdateOrder(Toppings.Items[i].Value);
                }
                else if (!Toppings.Items[i].Selected)
                {
                    UpdateOrder(Toppings.Items[i].Value + "-r");
                }
            }
        }

        protected void CancelOrderButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ContentPages/OrderForm.aspx");
        }
    }
}