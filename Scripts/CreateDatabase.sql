-- Create Database for CAO Case Management System
CREATE DATABASE CAOCaseManagement;
GO

USE CAOCaseManagement;
GO

-- Create Users Table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(15) NOT NULL,
    Role NVARCHAR(20) NOT NULL, -- Admin, Maker, Checker
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastLoginDate DATETIME NULL,
    Department NVARCHAR(500) NULL
);
GO

-- Create Cases Table
CREATE TABLE Cases (
    CaseId INT PRIMARY KEY IDENTITY(1,1),
    CaseNumber NVARCHAR(50) NOT NULL UNIQUE,
    CaseTitle NVARCHAR(500) NOT NULL,
    CaseDescription NVARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    DueDate DATETIME NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending', -- Pending, InReview, Approved, Rejected, Overdue
    MakerUserId INT NOT NULL,
    CheckerUserId INT NULL,
    MakerNotes NVARCHAR(500) NULL,
    CheckerNotes NVARCHAR(500) NULL,
    CheckedDate DATETIME NULL,
    Priority NVARCHAR(50) NULL, -- Low, Medium, High, Urgent
    DocumentPath NVARCHAR(500) NULL,
    OverdueNotificationSent BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (MakerUserId) REFERENCES Users(UserId),
    FOREIGN KEY (CheckerUserId) REFERENCES Users(UserId)
);
GO

-- Create Indexes
CREATE INDEX IX_Cases_MakerUserId ON Cases(MakerUserId);
CREATE INDEX IX_Cases_CheckerUserId ON Cases(CheckerUserId);
CREATE INDEX IX_Cases_Status ON Cases(Status);
CREATE INDEX IX_Cases_DueDate ON Cases(DueDate);
CREATE INDEX IX_Users_Role ON Users(Role);
GO

-- Insert Default Users
INSERT INTO Users (Username, PasswordHash, FullName, Email, PhoneNumber, Role, Department)
VALUES 
('admin', 'R9lw7agoQ/Ks8rxHeuLvBA2oO84ZYeH4zlSduGmQere=', 'Administrator', 'admin@cao.gov.in', '9876543210', 'Admin', 'Administration'),
('maker1', 'R9lw7agoQ/Ks8rxHeuLvBA2oO84ZYeH4zlSduGmQere=', 'Case Maker 1', 'maker1@cao.gov.in', '9876543211', 'Maker', 'Case Processing'),
('checker1', 'R9lw7agoQ/Ks8rxHeuLvBA2oO84ZYeH4zlSduGmQere=', 'Case Checker 1', 'checker1@cao.gov.in', '9876543212', 'Checker', 'Case Verification');
GO

PRINT 'Database created successfully!';
