using System;
using CaseManagement.Business;
using CaseManagement.Helpers;
using CaseManagement.Models;

namespace CaseManagement
{
    public partial class CreateCase : System.Web.UI.Page
    {
        private CaseBL _caseBL = new CaseBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!SecurityHelper.IsAuthorized("Maker"))
            {
                Response.Redirect("~/Unauthorized.aspx");
            }
        }

        protected void btnCreateCase_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                    return;

                // Create new case
                Case newCase = new Case
                {
                    CaseTitle = txtCaseTitle.Text.Trim(),
                    CaseDescription = txtCaseDescription.Text.Trim(),
                    DueDate = DateTime.Parse(txtDueDate.Text),
                    Priority = ddlPriority.SelectedValue,
                    MakerNotes = txtNotes.Text.Trim(),
                    MakerUserId = SecurityHelper.GetCurrentUserId()
                };

                // Handle file upload
                if (fuDocument.HasFile)
                {
                    string fileName = System.IO.Path.GetFileName(fuDocument.FileName);
                    string uploadPath = Server.MapPath("~/Uploads/");
                    
                    if (!System.IO.Directory.Exists(uploadPath))
                        System.IO.Directory.CreateDirectory(uploadPath);

                    string filePath = System.IO.Path.Combine(uploadPath, DateTime.Now.Ticks + "_" + fileName);
                    fuDocument.SaveAs(filePath);
                    newCase.DocumentPath = filePath.Replace(Server.MapPath("~"), "");
                }

                // Create case
                if (_caseBL.CreateCase(newCase))
                {
                    ShowSuccess("Case created successfully!");
                    ClearForm();
                    System.Threading.Thread.Sleep(2000);
                    Response.Redirect("~/Dashboard.aspx");
                }
                else
                {
                    ShowError("Failed to create case. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Error creating case: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtCaseTitle.Text = "";
            txtCaseDescription.Text = "";
            txtDueDate.Text = "";
            txtNotes.Text = "";
            ddlPriority.SelectedIndex = 1;
        }

        private void ShowError(string message)
        {
            litError.Text = message;
            phError.Visible = true;
            phSuccess.Visible = false;
        }

        private void ShowSuccess(string message)
        {
            litSuccess.Text = message;
            phSuccess.Visible = true;
            phError.Visible = false;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            SecurityHelper.ClearAuthentication();
            Response.Redirect("~/Login.aspx");
        }
    }
}
