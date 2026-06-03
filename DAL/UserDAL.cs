using System;
using System.Collections.Generic;
using System.Linq;
using CaseManagement.Data;
using CaseManagement.Models;

namespace CaseManagement.DAL
{
    public class UserDAL
    {
        private CaseManagementContext _context = new CaseManagementContext();

        public User GetUserByUsername(string username)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Username == username && u.IsActive);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user: " + ex.Message);
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user: " + ex.Message);
            }
        }

        public List<User> GetAllCheckers()
        {
            try
            {
                return _context.Users.Where(u => u.Role == "Checker" && u.IsActive).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving checkers: " + ex.Message);
            }
        }

        public List<User> GetAllMakers()
        {
            try
            {
                return _context.Users.Where(u => u.Role == "Maker" && u.IsActive).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving makers: " + ex.Message);
            }
        }

        public bool CreateUser(User user)
        {
            try
            {
                user.CreatedDate = DateTime.Now;
                user.IsActive = true;
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating user: " + ex.Message);
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
                if (existingUser == null)
                    return false;

                existingUser.FullName = user.FullName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Department = user.Department;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message);
            }
        }

        public bool UpdateLastLoginDate(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                    return false;

                user.LastLoginDate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating login date: " + ex.Message);
            }
        }
    }
}
