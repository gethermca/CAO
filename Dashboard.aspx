<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CaseManagement.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Dashboard - CAO Case Management</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f5f5f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        .navbar {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .navbar-brand {
            font-weight: 600;
            font-size: 20px;
        }
        .dashboard-container {
            padding: 30px 0;
        }
        .card {
            border: none;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.08);
            margin-bottom: 20px;
            transition: transform 0.3s, box-shadow 0.3s;
        }
        .card:hover {
            transform: translateY(-5px);
            box-shadow: 0 5px 20px rgba(0,0,0,0.12);
        }
        .card-header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            border: none;
            font-weight: 600;
        }
        .stat-card .card-body {
            text-align: center;
            padding: 30px;
        }
        .stat-number {
            font-size: 36px;
            font-weight: 700;
            color: #667eea;
        }
        .stat-label {
            font-size: 14px;
            color: #666;
            margin-top: 10px;
        }
        .btn-custom {
            border-radius: 5px;
            padding: 10px 20px;
            font-weight: 500;
        }
        .btn-primary-custom {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            border: none;
            color: white;
        }
        .btn-primary-custom:hover {
            color: white;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(102, 126, 234, 0.4);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation -->
        <nav class="navbar navbar-expand-lg navbar-dark">
            <div class="container-fluid">
                <a class="navbar-brand" href="#">CAO Case Management System</a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <span class="navbar-text mr-3">Welcome, <asp:Literal ID="litUsername" runat="server" /></span>
                        </li>
                        <li class="nav-item">
                            <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-outline-light btn-sm" OnClick="btnLogout_Click" />
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <!-- Main Content -->
        <div class="dashboard-container">
            <div class="container-fluid">
                <!-- Header -->
                <div class="row mb-30">
                    <div class="col-md-12">
                        <h1>Dashboard</h1>
                        <p class="text-muted">Welcome to CAO Case Management System (GIGW Compliant)</p>
                    </div>
                </div>

                <!-- Statistics Row -->
                <div class="row mb-30">
                    <div class="col-md-3">
                        <div class="card stat-card">
                            <div class="card-body">
                                <div class="stat-number">
                                    <asp:Literal ID="litTotalCases" runat="server" Text="0" />
                                </div>
                                <div class="stat-label">Total Cases</div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="card stat-card">
                            <div class="card-body">
                                <div class="stat-number">
                                    <asp:Literal ID="litPendingCases" runat="server" Text="0" />
                                </div>
                                <div class="stat-label">Pending Cases</div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="card stat-card">
                            <div class="card-body">
                                <div class="stat-number">
                                    <asp:Literal ID="litOverdueCases" runat="server" Text="0" />
                                </div>
                                <div class="stat-label">Overdue Cases</div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="card stat-card">
                            <div class="card-body">
                                <div class="stat-number">
                                    <asp:Literal ID="litApprovedCases" runat="server" Text="0" />
                                </div>
                                <div class="stat-label">Approved Cases</div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Actions Row -->
                <div class="row mb-30">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header">Quick Actions</div>
                            <div class="card-body">
                                <asp:PlaceHolder ID="phMakerActions" runat="server" Visible="false">
                                    <asp:HyperLink ID="lnkCreateCase" runat="server" NavigateUrl="~/CreateCase.aspx" CssClass="btn btn-primary-custom btn-custom">Create New Case</asp:HyperLink>
                                    <asp:HyperLink ID="lnkMyCases" runat="server" NavigateUrl="~/MyCases.aspx" CssClass="btn btn-info btn-custom">View My Cases</asp:HyperLink>
                                </asp:PlaceHolder>

                                <asp:PlaceHolder ID="phCheckerActions" runat="server" Visible="false">
                                    <asp:HyperLink ID="lnkPendingCases" runat="server" NavigateUrl="~/CheckerCases.aspx" CssClass="btn btn-warning btn-custom">Review Pending Cases</asp:HyperLink>
                                </asp:PlaceHolder>

                                <asp:PlaceHolder ID="phAdminActions" runat="server" Visible="false">
                                    <asp:HyperLink ID="lnkAllCases" runat="server" NavigateUrl="~/AllCases.aspx" CssClass="btn btn-primary-custom btn-custom">All Cases</asp:HyperLink>
                                    <asp:HyperLink ID="lnkUsers" runat="server" NavigateUrl="~/Users.aspx" CssClass="btn btn-success btn-custom">Manage Users</asp:HyperLink>
                                    <asp:HyperLink ID="lnkReports" runat="server" NavigateUrl="~/Reports.aspx" CssClass="btn btn-danger btn-custom">Reports</asp:HyperLink>
                                </asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Recent Cases -->
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header">Recent Cases</div>
                            <div class="card-body">
                                <asp:GridView ID="gvRecentCases" runat="server" CssClass="table table-hover" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="CaseNumber" HeaderText="Case Number" />
                                        <asp:BoundField DataField="CaseTitle" HeaderText="Title" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkView" runat="server" NavigateUrl='<%# "ViewCase.aspx?id=" + Eval("CaseId") %>' CssClass="btn btn-sm btn-outline-primary">View</asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
