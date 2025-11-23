using System;
using System.Web;

namespace Fricke_ITM_325_Assignment_4.MasterPages
{
    public partial class Main : ThemedApp
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            preferredTheme = Request.Cookies[cookieName];

            if (preferredTheme == null || string.IsNullOrEmpty(preferredTheme.Value))
            {
                Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
                theme = "Default";
            }
            else
            {
                theme = preferredTheme.Value;
            }

            AddThemeCss(theme);
        }

        protected void AddThemeCss(string themeName)
        {   
            string cssPath = ResolveUrl("~/Styles/Main_" + themeName + ".css?v=1.0.0");
            ThemeCss.Href = cssPath;
        }

        protected void ThemeLink_Click(object sender, EventArgs e)
        {
            string selectedTheme;

            if (sender == DropdownLinkButton_Default)
                selectedTheme = "Default";
            else if (sender == DropdownLinkButton_Barbie)
                selectedTheme = "Barbie";
            else if (sender == DropdownLinkButton_WhiteMonster)
                selectedTheme = "WhiteMonster";
            else if (sender == DropdownLinkButton_LimeScooter)
                selectedTheme = "LimeScooter";
            else if (sender == DropdownLinkButton_Takis)
                selectedTheme = "Takis";
            else if (sender == DropdownLinkButton_USA)
                selectedTheme = "USA";
            else
                selectedTheme = "Default";

            if (selectedTheme == "Default")
            {
                // Overwrite cookie with an expired one to delete it client-side
                var deleteCookie = new HttpCookie(cookieName);
                deleteCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(deleteCookie);
            }
            else
            {
                var themeCookie = new HttpCookie(cookieName, selectedTheme);
                themeCookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(themeCookie);
            }

            Response.Redirect(Request.RawUrl);
        }
    }
}
