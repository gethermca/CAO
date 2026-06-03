using System;
using System.Security.Cryptography;
using System.Text;
using CaseManagement.DAL;
using CaseManagement.Models;

namespace CaseManagement.Business
{
    public class AuthenticationBL
    {
        private UserDAL _userDAL = new UserDAL();

        public User Login(string username, string password)
        {
            try
            {
                User user = _userDAL.GetUserByUsername(username);
                if (user == null)
                    return null;

                if (VerifyPassword(password, user.PasswordHash))
                {
                    _userDAL.UpdateLastLoginDate(user.UserId);
                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Login error: " + ex.Message);
            }
        }

        public bool Register(User user, string password)
        {
            try
            {
                if (_userDAL.GetUserByUsername(user.Username) != null)
                    return false; // User already exists

                user.PasswordHash = HashPassword(password);
                return _userDAL.CreateUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Registration error: " + ex.Message);
            }
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hash;
        }
    }
}
