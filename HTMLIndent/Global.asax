﻿<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        //Exception exc = Server.GetLastError();
        //Mail.SendMe("goldwort@gmail.com", "", "", "Error | HTML Formatter", exc.Message + "<br><hr><br>" + exc.StackTrace);
    }
    void Application_BeginRequest(object sender, EventArgs e)
    {
        //string s = Request.Url.LocalPath;
        //if (!(s.EndsWith(".aspx") || s.EndsWith(".htm") || s.EndsWith(".html")))
        //{
        //    return;
        //}

        //s = Request.Url.AbsoluteUri;

        //System.Net.WebRequest mywebReq = System.Net.WebRequest.Create("http://www.purehtml.in");
        //System.Net.WebResponse mywebResp = mywebReq.GetResponse();

        //System.IO.StreamReader sr = new System.IO.StreamReader(mywebResp.GetResponseStream());
        //string strHTML = sr.ReadToEnd();

        //var c = new Indent.Code();
        //c.FormatCSS = true;
        //c.FormatJS = true;
        //c.ReplaceASCII = false;
        //c.CompressFlag = false;
        //c.Spaces = 4;

        //string outp = c.Beautify(strHTML);
        //outp += "\r\n<!-- Served by PureHtml. For more info visit www.puerhtml.in -->";

        //if (System.IO.File.Exists(Server.MapPath("temp.html")))
        //    System.IO.File.Delete(Server.MapPath("temp.html"));

        //System.IO.StreamWriter sw = System.IO.File.CreateText(Server.MapPath("temp.html"));
        //sw.WriteLine(outp);
        //sw.Close();
        //Server.Transfer("temp.html");

    }

    private void Application_EndRequest(Object source, EventArgs e)
    {
        //HttpApplication application = (HttpApplication)source;
        //HttpContext context = application.Context;
        //string filePath = context.Request.FilePath;
        //string fileExtension =
        //    VirtualPathUtility.GetExtension(filePath);
        //if (fileExtension.Equals(".aspx"))
        //{
        //    context.Response.Write("<hr><h1><font color=red>" +
        //        "HelloWorldModule: End of Request</font></h1>");
        //}
        //if (!(fileExtension.Equals(".aspx") || fileExtension.Equals(".htm") || fileExtension.Equals(".html")))
        //{
        //    return;
        //}
        //var c = new Indent.Code();
        //c.FormatCSS = true;
        //c.FormatJS = true;
        //c.ReplaceASCII = false;
        //c.CompressFlag = false;
        //c.Spaces = 4;
        //System.IO.StreamReader sr = new System.IO.StreamReader(context.Response.OutputStream, System.Text.Encoding.UTF32);

        //string s = c.Beautify(sr.ReadToEnd());
        //context.Response.ClearContent();

        //context.Response.Write(s);
    }
    void Session_Start(object sender, EventArgs e)
    {

        util.DBNull(Request.Browser.Browser, Request.UserHostAddress.ToString(), "Start",
            false, false, false, 0);

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>

