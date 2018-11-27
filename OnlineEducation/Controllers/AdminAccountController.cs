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
                Account theAccount = accountDB.Login(account.Email, account.Password);
                if (theAccount == null)
                {
                    ViewBag.ErrorMessage = "Invalid username or password";
                    return View(account);
                }
                else
                {
                    Session["Account"] = theAccount;
                    Session["IsAdmin"] = true;
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
                    theAccount.Password = accountDB.EncodePasswordToBase64(newPassword);
                    //accountDB.SendEmailViaGoogle(theAccount.Email, newPassword);
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
        public ActionResult Details(string id)
        {
            Account account = accountDB.getAccountByEmail(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = accountDB.getAccountByEmail(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                accountDB.UpdateAccount(account, account.Name);
                return RedirectToAction("Details?id=" + account.Email);
            }
            return View(account);
        }
        [HttpGet]
        public ActionResult ChangePassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = accountDB.getAccountByEmail(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        [HttpPost]
        public ActionResult ChangePassword(Account account)
        {
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
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                // check email existed
                accountDB.Register(account.Email, account.Password, account.Name);
                return RedirectToAction("CreateAccountSuccess");
            }
            return View();
        }
        [HttpGet]
        public ActionResult CreateAccountSuccess()
        {
            return View();
        }

    }
}
