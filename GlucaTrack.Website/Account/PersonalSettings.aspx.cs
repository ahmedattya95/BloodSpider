﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GlucaTrack.Website.Account
{
    public partial class PersonalSettings : System.Web.UI.Page
    {
        Queries.sp_GetLoginRow LoginRow = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //redirect if not logged in
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                LoginRow = (Queries.sp_GetLoginRow)Session["LoggedInUser"];

                if (Session["LoggedInUserId"] != null)
                    Session["LoggedInUserId"] = LoginRow.user_id;
                else
                    Session.Add("LoggedInUserId", LoginRow.user_id);
            }

            SetResources();
        }

        private void SetResources()
        {
            this.lblLowNormal.Text = Resources.Account_Strings.Label_LowNormal;
            this.lblHighNormal.Text = Resources.Account_Strings.Label_HighNormal;
            this.btnSavePersonalSettings.Text = Resources.Account_Strings.Button_SavePersonalSettings;
        }

        protected void btnSavePersonalSettings_Click(object sender, EventArgs e)
        {
            using (QueriesTableAdapters.QueriesTableAdapter qta = new QueriesTableAdapters.QueriesTableAdapter())
            {
                qta.sp_UpdatePersonalSettings(LoginRow.user_id,
                                              Convert.ToByte(((TextBox)this.fvLowNormal.Row.FindControl("LowNormal")).Text), 
                                              Convert.ToByte(((TextBox)this.fvHighNormal.Row.FindControl("HighNormal")).Text));
            }
        }
    }
}