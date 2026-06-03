using System;
using System.Collections.Generic;
using CaseManagement.DAL;
using CaseManagement.Models;

namespace CaseManagement.Business
{
    public class CaseBL
    {
        private CaseDAL _caseDAL = new CaseDAL();
        private UserDAL _userDAL = new UserDAL();
        private NotificationBL _notificationBL = new NotificationBL();

        public Case GetCaseById(int caseId)
        {
            try
            {
                return _caseDAL.GetCaseById(caseId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving case: " + ex.Message);
            }
        }

        public List<Case> GetAllCases()
        {
            try
            {
                return _caseDAL.GetAllCases();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving cases: " + ex.Message);
            }
        }

        public List<Case> GetCasesByMaker(int makerId)
        {
            try
            {
                return _caseDAL.GetCasesByMaker(makerId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving maker cases: " + ex.Message);
            }
        }

        public List<Case> GetCasesForChecker(int checkerId)
        {
            try
            {
                return _caseDAL.GetCasesForChecker(checkerId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving checker cases: " + ex.Message);
            }
        }

        public List<Case> GetOverdueCases()
        {
            try
            {
                return _caseDAL.GetOverdueCases();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving overdue cases: " + ex.Message);
            }
        }

        public bool CreateCase(Case caseModel)
        {
            try
            {
                if (string.IsNullOrEmpty(caseModel.CaseNumber))
                    caseModel.CaseNumber = "CASE-" + DateTime.Now.Ticks.ToString();

                return _caseDAL.CreateCase(caseModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating case: " + ex.Message);
            }
        }

        public bool ApproveCase(int caseId, int checkerId, string checkerNotes)
        {
            try
            {
                return _caseDAL.ApproveCase(caseId, checkerId, checkerNotes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error approving case: " + ex.Message);
            }
        }

        public bool RejectCase(int caseId, int checkerId, string checkerNotes)
        {
            try
            {
                return _caseDAL.RejectCase(caseId, checkerId, checkerNotes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error rejecting case: " + ex.Message);
            }
        }

        public void SendOverdueNotifications()
        {
            try
            {
                List<Case> overdueCases = _caseDAL.GetOverdueNotificationPendingCases();

                foreach (var caseModel in overdueCases)
                {
                    User maker = _userDAL.GetUserById(caseModel.MakerUserId);
                    if (maker != null)
                    {
                        string message = string.Format(
                            "REMINDER: Case {0} - '{1}' is overdue. Due date was {2}. Please take action immediately.",
                            caseModel.CaseNumber,
                            caseModel.CaseTitle,
                            caseModel.DueDate.ToString("dd/MM/yyyy")
                        );

                        _notificationBL.SendSMS(maker.PhoneNumber, message);
                        _caseDAL.MarkOverdueNotificationSent(caseModel.CaseId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending overdue notifications: " + ex.Message);
            }
        }
    }
}
