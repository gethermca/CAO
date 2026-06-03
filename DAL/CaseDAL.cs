using System;
using System.Collections.Generic;
using System.Linq;
using CaseManagement.Data;
using CaseManagement.Models;

namespace CaseManagement.DAL
{
    public class CaseDAL
    {
        private CaseManagementContext _context = new CaseManagementContext();

        public Case GetCaseById(int caseId)
        {
            try
            {
                return _context.Cases.FirstOrDefault(c => c.CaseId == caseId);
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
                return _context.Cases.OrderByDescending(c => c.CreatedDate).ToList();
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
                return _context.Cases.Where(c => c.MakerUserId == makerId)
                    .OrderByDescending(c => c.CreatedDate).ToList();
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
                return _context.Cases.Where(c => c.Status == "Pending" && c.CheckerUserId == null)
                    .OrderByDescending(c => c.DueDate).ToList();
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
                return _context.Cases.Where(c => c.DueDate < DateTime.Now && c.Status != "Approved" && c.Status != "Rejected")
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving overdue cases: " + ex.Message);
            }
        }

        public List<Case> GetOverdueNotificationPendingCases()
        {
            try
            {
                return _context.Cases.Where(c => c.DueDate < DateTime.Now && !c.OverdueNotificationSent && c.Status != "Approved" && c.Status != "Rejected")
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving pending notification cases: " + ex.Message);
            }
        }

        public bool CreateCase(Case caseModel)
        {
            try
            {
                caseModel.CreatedDate = DateTime.Now;
                caseModel.Status = "Pending";
                caseModel.OverdueNotificationSent = false;
                _context.Cases.Add(caseModel);
                _context.SaveChanges();
                return true;
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
                var caseModel = _context.Cases.FirstOrDefault(c => c.CaseId == caseId);
                if (caseModel == null)
                    return false;

                caseModel.Status = "Approved";
                caseModel.CheckerUserId = checkerId;
                caseModel.CheckerNotes = checkerNotes;
                caseModel.CheckedDate = DateTime.Now;
                _context.SaveChanges();
                return true;
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
                var caseModel = _context.Cases.FirstOrDefault(c => c.CaseId == caseId);
                if (caseModel == null)
                    return false;

                caseModel.Status = "Rejected";
                caseModel.CheckerUserId = checkerId;
                caseModel.CheckerNotes = checkerNotes;
                caseModel.CheckedDate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error rejecting case: " + ex.Message);
            }
        }

        public bool MarkOverdueNotificationSent(int caseId)
        {
            try
            {
                var caseModel = _context.Cases.FirstOrDefault(c => c.CaseId == caseId);
                if (caseModel == null)
                    return false;

                caseModel.OverdueNotificationSent = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error marking notification sent: " + ex.Message);
            }
        }
    }
}
