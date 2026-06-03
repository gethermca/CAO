<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateCase.aspx.cs" Inherits="CaseManagement.CreateCase" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Create New Case - CAO Case Management</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f5f5f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        .navbar {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        }
        .form-container {
            background: white;
            border-radius: 10px;
            padding: 30px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.08);
            margin-top: 30px;
            margin-bottom: 30px;
        }
        .form-group label {
            font-weight: 600;
            color: #333;
            margin-bottom: 10px;
        }
        .form-control {
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 12px;
        }
        .form-control:focus {
            border-color: #667eea;
            box-shadow: 0 0 0 0.2rem rgba(102, 126, 234, 0.25);
        }
        .btn-submit {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            border: none;
            padding: 12px 30px;
            font-weight: 600;
            margin-right: 10px;
        }
        .btn-submit:hover {
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
                <a class="navbar-brand" href="Dashboard.aspx">CAO Case Management System</a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <asp:HyperLink ID="lnkDashboard" runat="server" NavigateUrl="~/Dashboard.aspx" CssClass="nav-link">Dashboard</asp:HyperLink>
                        </li>
                        <li class="nav-item">
                            <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-outline-light btn-sm" OnClick="btnLogout_Click" />
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <!-- Main Content -->
        <div class="container">
            <div class="row">
                <div class="col-md-8 offset-md-2">
                    <div class="form-container">
                        <h2>Create New Case</h2>
                        <hr />

                        <asp:PlaceHolder ID="phError" runat="server" Visible="false">
                            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                                <asp:Literal ID="litError" runat="server"></asp:Literal>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </asp:PlaceHolder>

                        <asp:PlaceHolder ID="phSuccess" runat="server" Visible="false">
                            <div class="alert alert-success alert-dismissible fade show" role="alert">
                                <asp:Literal ID="litSuccess" runat="server"></asp:Literal>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </asp:PlaceHolder>

                        <div class="form-group">
                            <label for="txtCaseTitle">Case Title *</label>
                            <asp:TextBox ID="txtCaseTitle" runat="server" CssClass="form-control" MaxLength="500" Placeholder="Enter case title" />
                            <asp:RequiredFieldValidator ID="rfvCaseTitle" runat="server" ControlToValidate="txtCaseTitle" ErrorMessage="Case Title is required" ValidationGroup="CreateCase" Display="Dynamic" />
                        </div>

                        <div class="form-group">
                            <label for="txtCaseDescription">Case Description *</label>
                            <asp:TextBox ID="txtCaseDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Placeholder="Enter case description" />
                            <asp:RequiredFieldValidator ID="rfvCaseDescription" runat="server" ControlToValidate="txtCaseDescription" ErrorMessage="Case Description is required" ValidationGroup="CreateCase" Display="Dynamic" />
                        </div>

                        <div class="form-group">
                            <label for="txtDueDate">Due Date *</label>
                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" TextMode="Date" />
                            <asp:RequiredFieldValidator ID="rfvDueDate" runat="server" ControlToValidate="txtDueDate" ErrorMessage="Due Date is required" ValidationGroup="CreateCase" Display="Dynamic" />
                        </div>

                        <div class="form-group">
                            <label for="ddlPriority">Priority</label>
                            <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control">
                                <asp:ListItem Value="Low">Low</asp:ListItem>
                                <asp:ListItem Value="Medium" Selected="True">Medium</asp:ListItem>
                                <asp:ListItem Value="High">High</asp:ListItem>
                                <asp:ListItem Value="Urgent">Urgent</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label for="txtNotes">Maker Notes</label>
                            <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Placeholder="Enter additional notes" />
                        </div>

                        <div class="form-group">
                            <label for="fuDocument">Upload Document</label>
                            <asp:FileUpload ID="fuDocument" runat="server" CssClass="form-control" accept=".pdf,.doc,.docx,.xls,.xlsx" />
                        </div>

                        <div class="form-group">
                            <asp:Button ID="btnCreateCase" runat="server" Text="Create Case" CssClass="btn btn-submit" OnClick="btnCreateCase_Click" ValidationGroup="CreateCase" />
                            <asp:HyperLink ID="lnkCancel" runat="server" NavigateUrl="~/Dashboard.aspx" CssClass="btn btn-secondary">Cancel</asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
