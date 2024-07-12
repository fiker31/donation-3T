﻿<%@ Application Language="C#" %>
<%@ Import Namespace="Microsoft.Practices.EnterpriseLibrary.ExceptionHandling" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="BLL.Framework" %>
<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        Application["Period"] = null;
    }
    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }
    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        ExceptionPolicy.HandleException(Server.GetLastError(), "Global Policy");
        //Exception ex = new Exception() ;
        //Session["ErrMessage"] = ex.Message; 
    }
    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
    }
    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.


        //EntValidationErrors validationErrors2 = new EntValidationErrors();
        //UserAccountBO ua = new UserAccountBO();
        //ua.Load(Session["LoginId"].ToString());
        //ua.DBAction = BaseEO.DBActionEnum.Update;
        //if (!ua.Save(ref validationErrors2, Session["LoginId"].ToString()))
        //{
        //}
    }


    void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        // Extract the forms authentication cookie
        string cookieName = FormsAuthentication.FormsCookieName;
        HttpCookie authCookie = Context.Request.Cookies[cookieName];
        if (null == authCookie)
        {
            // There is no authentication cookie.
            return;
        }

        FormsAuthenticationTicket authTicket = null;
        try
        {
            authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        }
        catch (Exception)
        {
            // Log exception details (omitted for simplicity)
            return;
        }
        if (null == authTicket)
        {
            // Cookie failed to decrypt.
            return;
        }
        System.Security.Principal.GenericIdentity id = new System.Security.Principal.GenericIdentity(authTicket.Name, "LdapAuthentication");
        // This principal will flow throughout the request.
        System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, null);
        // Attach the new principal object to the current HttpContext object
        Context.User = principal;
    }

</script>
