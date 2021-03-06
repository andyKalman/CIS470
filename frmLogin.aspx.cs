﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class frmLogin : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    
    protected void btnLogin_Click(object sender, EventArgs e)
    {


        dsUsers dsUserLogin;
        dsCustomers dsCustomerInfo;
        

        dsUserLogin = clsDataLayer.VerifyUser(Server.MapPath("SiteDB.accdb"), txtUserName.Text, txtPassword.Text);

        if(dsUserLogin.Users.Count < 1)
        {
            //If not valid login redirect back to login page.

            
            lblStatus.Text = ("Invalid Login!");

            
        }

        else //If login Correct, Redirect to proper security level enabled Web Page. 
        {
            int SecurityLevel = dsUserLogin.Users[0].UserSecLevel;
            
            switch (SecurityLevel)
            {

                case 1:
                    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                    Session["SecurityLevel"] = 1;
                    Session["UserNameID"] = txtUserName.Text;
                    dsCustomerInfo = clsDataLayer.GetAddressInfo(Server.MapPath("SiteDB.accdb"), txtUserName.Text);
                    int AddressID = dsCustomerInfo.Customer[0].AddressID;
                    Session["AddressID"] = AddressID;
                    dsCustomerInfo = clsDataLayer.GetAddressInfo(Server.MapPath("SiteDB.accdb"), txtUserName.Text);
                    int CustID = dsCustomerInfo.Customer[0].CustID;
                    Session["CustID"] = CustID;
                    Response.Redirect("public/frmCatalog.aspx");
                    break;
                case 2:
                    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                    Session["SecurityLevel"] = 2;
                    Session["UserNameID"] = txtUserName.Text;
                    Response.Redirect("frmViewOrders.aspx");
                    break;
                case 3:
                    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                    Session["SecurityLevel"] = 3;
                    Session["UserNameID"] = txtUserName.Text;
                    Response.Redirect("frmUpdateOrder.aspx");
                    break;
                    
            }








        }



    }



    /**
    public String State
    {
        get
        {
            object o = ViewState["text"];
            return (o == null) ? String.Empty : (string)o;
        }

        set
        {
            ViewState["Text"] = value;
        }
    }
     * 
     * */
}