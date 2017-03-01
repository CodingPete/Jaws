using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Dashboard.Security
{
    public class MyMembershipProvider : MembershipProvider
    {
        private DataContainer db = new DataContainer();
        
        public override bool EnablePasswordRetrieval { get;}

        public override bool EnablePasswordReset { get; }

        public override bool RequiresQuestionAndAnswer { get; }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts { get; }

        public override int PasswordAttemptWindow { get; }

        public override bool RequiresUniqueEmail { get; }

        public override MembershipPasswordFormat PasswordFormat { get; }

        public override int MinRequiredPasswordLength { get; }

        public override int MinRequiredNonAlphanumericCharacters { get; }

        public override string PasswordStrengthRegularExpression { get; }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            Personal person;
            try
            {
                person = db.PersonalSet.First((x) => x.email.Equals(email));
                return person.Vorname + " " + person.Name;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            Personal person;
            try
            {
                person = db.PersonalSet.First((x) => x.email.Equals(username) && x.passwort.Equals(password));
            }
            catch(Exception)
            {
                person = null;
            }
            return person != null;
            
        }
    }
}