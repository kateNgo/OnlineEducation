using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Globalization;

namespace OnlineEducation.Models
{
    public class AccountDB
    {
        bool invalid = false;
        public SqlConnection SQLConnect()
        {
            string connString = WebConfigurationManager.ConnectionStrings["ConStringSqlServer"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            //conn.Open();
            return conn;
        }
        public Account Login(string email, string password)
        {
            Account theAccount = null;
            string query = "SELECT * FROM Account WHERE Email=@email AND Password=@password";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", EncodePasswordToBase64( password));
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        theAccount = new Account();
                        theAccount.Id = sdr.GetInt32(0);
                        theAccount.Email = sdr.GetString(1);
                        theAccount.Password = sdr.GetString(2);
                        theAccount.Name = sdr.GetString(3);
                    }
                    con.Close();
                }
            }

            return theAccount;

        }
        public Account getAccountByEmail(string email)
        {
            Account theAccount = null;
            string query = "SELECT * FROM Account WHERE Email=@email ";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        theAccount = new Account();
                        theAccount.Email = sdr.GetString(1);
                        theAccount.Password = sdr.GetString(2);
                        theAccount.Name = sdr.GetString(3);
                    }
                    con.Close();
                }
            }

            return theAccount;

        }

        public Account Register(string email, string password, string name)
        {
            Account theAccount = null;
            string query = "INSERT INTO Account (Email, Password, Name) VALUES (@email, @password, @name)";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    theAccount = new Account
                    {
                        Email = email,
                        Password = password,
                        Name = name
                    };
                }
            }
            return theAccount;
        }
        public bool UpdateAccount(Account acc, string name)
        {
            //Account theAccount = null;
            string query = "UPDATE Account SET name = @name WHERE Email = @email";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@email", acc.Email);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Connection = con;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    return (result == 1);
                }
            }

        }
        public bool ChangePassword(Account acc, string newPassword)
        {

            return ChangePassword(acc.Email,newPassword);

        }
        public bool ChangePassword(string email, string newPassword)
        {

            //Account theAccount = null;
            string query = "UPDATE Account SET Password = @password WHERE Email = @email";

            using (SqlConnection con = SQLConnect())
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", EncodePasswordToBase64(newPassword));
                    cmd.Connection = con;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    return (result == 1);
                }
            }

        }
        public static bool SendEmail(string email, string randomPassword)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email);
            mailMessage.From = new MailAddress("kate@asims.com.au");
            mailMessage.Subject = "Reset password for Help online admin web site ";
            mailMessage.Body = String.Format(@"Please use {0} as a password to login Help Online Education Center Admin", randomPassword);
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Timeout = 600000,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("kate-pc\\kate", "SupportId456")
            };

            smtpClient.Send(mailMessage);
            return true;

        }
        public bool SendEmailViaGoogle(string email, string randomPassword)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email);
            mailMessage.From = new MailAddress("kateAsims2018@gmail.com");
            mailMessage.Subject = "Reset password for Help online admin web site ";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = String.Format(@"Please use <b><i>{0}</i></b> as a password to login Online Education Center Admin", randomPassword);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("kateAsims2018@gmail.com", "SupportId456")
            };

            smtpClient.Send(mailMessage);
            return true;

        }
        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid email format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
        public  string EncodePasswordToBase64(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
        public string RandomPassword()
        {
            int size = 8;
            bool lowerCase = true;
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();

        }

    }
}