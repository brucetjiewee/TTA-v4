using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for NotificationCenter
/// </summary>
public class NotificationCenter
{
    const string ALERTMESSAGE_HEADER = "alertMessage";
    public NotificationCenter()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void ShowNotification(Page form,  string message, string alertType)
    {
        ScriptManager.RegisterClientScriptBlock(form, form.GetType(), ALERTMESSAGE_HEADER, "notify('" + alertType + "', '" + message + "')", true);
    }
}

public struct AlertTypes
{
    public static readonly string error = "error";
    public static readonly string info = "info";
    public static readonly string success = "success";
}