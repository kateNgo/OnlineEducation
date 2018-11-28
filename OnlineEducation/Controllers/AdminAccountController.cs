using OnlineEducation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineEducation.Controllers
{
    public class AdminAccountController : Controller
    {
        /** 
         * Password of account is sent to DB with a raw value
         */
        AccountDB accountDB = new AccountDB();

        // GET: AdminAccount/Login
        public ActionResult Login()
        {
            return View();
        }
        // POST: AdminAccount/Login
        [HttpPost]
        public ActionResult Login(Account account)
        {
            if (ModelState.IsValid)
            {
                Account theAccount = accountDB.Login(account.Email,account.Password);
                if (theAccount == null)
                {
                    ViewBag.ErrorMessage = "Invalid username or password";
                    return View(account);
                }
                else
                {
                    SetSessionAfterLogin(theAccount);
                    return RedirectToAction("../HelpOnline/AdminLevel1/Index");
                }
            }
            return View();
        }
        // GET: AdminAccount/ResetPassword
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        // POST: AdminAccount/ResetPassword
        [HttpPost]
        public ActionResult ResetPassword(string id)
        {
            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(id))
                {
                    ViewBag.ErrorMessage = "Email is required";
                    return View();
                }
                Account theAccount = accountDB.getAccountByEmail(id);
                if (theAccount == null)
                {
                    ViewBag.ErrorMessage = "Email " + id + " has not been registered.";
                    return View();
                }
                else
                {
                    string newPassword = accountDB.RandomPassword();
                    theAccount.Password = newPassword;
                    accountDB.SendEmailViaGoogle(theAccount.Email, newPassword);
                    accountDB.ChangePassword(theAccount.Email, theAccount.Password);
                    ViewBag.Message = "New password has been sent to email " + id;
                    return RedirectToAction("ResetPasswordSuccess");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult ResetPasswordSuccess()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Login");
            }
            Account account = (Account)Session["user"];
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        public ActionResult Edit()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Login");
            }
           
            Account account = (Account)Session["user"];
            if (account == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(string name)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Login");
            }
            if (ModelState.IsValid)
            {
                Account account = (Account)Session["user"];
                accountDB.UpdateAccount(account, name);
                account.Name = name;
                Session["user"] = account;
                return RedirectToAction("Details");
            }
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Login");
            }
            Account account = (Account)Session["user"]; // accountDB.getAccountByEmail(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        [HttpPost]
        public ActionResult ChangePassword(Account account)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Login");
            }
            if (ModelState.IsValid)
            {
                accountDB.ChangePassword(account, account.NewPassword);

                return RedirectToAction("ChangePasswordSuccess");
            }
            return View();
        }
        [HttpGet]
        public ActionResult ChangePasswordSuccess()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(NewAccount newAccount)
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Login");
            }
            if (ModelState.IsValid)
            {
                // check email existed
                accountDB.Register(newAccount.Email, newAccount.NewPassword, newAccount.Name);
                return RedirectToAction("CreateAccountSuccess");
            }
            return View();
        }
        [HttpGet]
        public ActionResult CreateAccountSuccess()
        {
            if (!IsAdminLogin())
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        
        private void SetSessionAfterLogin(Account account)
        {
            Session["user"] = account;
            Session["email"] = account.Email;
            Session["IsAdmin"] = Convert.ToBoolean(true);
        }
        public bool IsAdminLogin()
        {
            if ((Session["user"] == null) || (Session["IsAdmin"] == null) || (Convert.ToBoolean(Session["IsAdmin"]) != true))
            {
                return false;
            }
            return true;
        }
    }
}
