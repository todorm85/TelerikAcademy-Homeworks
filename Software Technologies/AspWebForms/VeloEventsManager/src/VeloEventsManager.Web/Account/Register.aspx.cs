﻿using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using VeloEventsManager.Data;
using VeloEventsManager.Models;

namespace VeloEventsManager.Web.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            var user = new User() { UserName = Username.Text };
            if (FileUploadControl.HasFile)
            {
                string fileName = user.UserName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".jpg";
                FileUploadControl.SaveAs(Server.MapPath("~/Uploaded_Files/") + fileName);
                user.Avatar = fileName;
            }

            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {

                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("~/");
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}