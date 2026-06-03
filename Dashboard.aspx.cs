using System;
using System.Collections.Generic;
using System.Linq;
using CaseManagement.Business;
using CaseManagement.Helpers;
using CaseManagement.Models;

namespace CaseManagement
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private CaseBL _caseBL = new CaseBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            try
            {
                int userId = SecurityHelper.GetCurrentUserId();
                string userRole = SecurityHelper.GetCurrentUserRole();
                string username = SecurityHelper.GetCurrentUsername();

                litUsername.Text = username;

                // Load statistics
                List<Case> allCases = _caseBL.GetAllCases();
                litTotalCases.Text = allCases.Count.ToString();
                litPendingCases.Text = allCases.Where(c => c.Status == "Pending").Count().ToString();
                litOverdueCases.Text = allCases.Where(c => c.IsOverdue).Count().ToString();
                litApprovedCases.Text = allCases.Where(c => c.Status == "Approved").Count().ToString();

                // Show role-specific actions
                if (userRole == "Maker")
                {
                    phMakerActions.Visible = true;
                    LoadMakerCases(userId);
                }
                else if (userRole == "Checker")
                {
                    phCheckerActions.Visible = true;
                    LoadCheckerCases(userId);
                }
                else if (userRole == "Admin")
                {
                    phAdminActions.Visible = true;
                    LoadAllCases();
                }

                // Load recent cases
                LoadRecentCases();
            }
            catch (Exception ex)
            {
                // Log error
            }
        }

        private void LoadMakerCases(int userId)
        {
            // Load maker-specific cases
        }

        private void LoadCheckerCases(int userId)
        {
            // Load checker-specific cases
        }

        private void LoadAllCases()
        {
            // Load all cases for admin
        }

        private void LoadRecentCases()
        {
            try
            {
                List<Case> cases = _caseBL.GetAllCases().Take(5).ToList();
                gvRecentCases.DataSource = cases;
                gvRecentCases.DataBind();
            }
            catch (Exception ex)
            {
                // Log error
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            SecurityHelper.ClearAuthentication();
            Response.Redirect("~/Login.aspx");
        }
    }
}
