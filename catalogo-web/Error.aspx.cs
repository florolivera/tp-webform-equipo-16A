using System;

namespace TPWebForm_equipo_16A
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = (Session["error"] as string) ?? "Ocurrió un error.";
                Session.Remove("error");
            }
        }
    }
}
