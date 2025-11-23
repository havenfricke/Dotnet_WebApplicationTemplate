using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;


public class ThemedApp : MasterPage
{
    protected HttpCookie preferredTheme;

    protected static string cookieName = "Theme";
    protected string theme;
    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        // This class is here to demonstrate extensibility in an ASP.NET web app 
        // Modify ALL of your content pages so that they correctly switch between
        // themes based on which drop down option is selected. This will require
        // that you modify the class for each content page so that it extends your
        // ThemedPage class.

        // "When you use a master page in ASP.NET, the relationship between the
        // master page and the content page is not one of class inheritance in the
        // traditional sense. Instead, it is a compositional relationship where the
        // master page acts as a template for the content page."

        // Instead of templating each one of the content pages, the master page
        // was templatized because it houses each content page. 

        // This completely removes the need to extend each content page from a specific class.
        // This class is here for demo purposes. 
    }

}

