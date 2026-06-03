using System;
using CaseManagement.Business;
using CaseManagement.Helpers;
using CaseManagement.Models;

namespace CaseManagement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if already logged in
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Dashboard.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                AuthenticationBL authBL = new AuthenticationBL();
                User user = authBL.Login(username, password);

                if (user != null)
                {
                    SecurityHelper.SetAuthenticationCookie(user);
                    Response.Redirect("~/Dashboard.aspx");
                }
                else
                {
                    ShowError("Invalid username or password. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ShowError("An error occurred during login: " + ex.Message);
            }
        }

        private void ShowError(string message)
        {
            litError.Text = message;
            phError.Visible = true;
        }
    }
}
