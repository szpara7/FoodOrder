using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace FoodOrder.Infrastructure
{
    public static class EmailsBody
    {
        public static string ConfirmEmail(string userName,string userToken, string userEmail)
        {
            
            StringBuilder body = new StringBuilder();
            body.Append("Welcome " + userName + "!.");
            body.AppendLine();
            body.Append("Please confirm your e-mail.");
            body.AppendLine();
            body.Append("http://localhost:51899/Auth/ConfirmEmail?token=" + userToken + "&email=" + userEmail);

            return body.ToString();
        }

        public static string RecoverPassword(string userName, string userToken, string userEmail)
        {
            StringBuilder body = new StringBuilder();
            body.Append("Welcome " + userName + "!.");
            body.AppendLine();
            body.Append("Please click link and set new password.");
            body.AppendLine();
            body.Append("http://localhost:51899/AccountManage/SetNewPassword?token=" + userToken + "&email=" + userEmail);

            return body.ToString();
        }
    }
}