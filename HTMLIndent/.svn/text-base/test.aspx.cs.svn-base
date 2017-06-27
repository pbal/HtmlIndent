using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using Indent;

public partial class test : System.Web.UI.Page
{
    WebRequest mywebReq;
    WebResponse mywebResp;
    StreamReader sr;
    string strHTML;
    StreamWriter sw;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here
        mywebReq = WebRequest.Create("http://www.purehtml.in/index.aspx");
        mywebResp = mywebReq.GetResponse();
        
        sr = new StreamReader(mywebResp.GetResponseStream());
        strHTML = sr.ReadToEnd();

        Code c = new Code();
        c.FormatCSS = true;
        c.FormatJS = true;
        c.ReplaceASCII = false;
        c.CompressFlag = false;
        c.Spaces = 4;
        
        string outp = c.Beautify(strHTML);
        outp += "\r\n<!-- Served by PureHtml logic. For more info visit www.puerhtml.in -->";

        if (File.Exists(Server.MapPath("temp.html")))
            File.Delete(Server.MapPath("temp.html"));

        sw = File.CreateText(Server.MapPath("temp.html"));
        sw.WriteLine(outp);
        sw.Close();
        //Response.WriteFile(Server.MapPath("temp.html"));
        Server.Transfer("temp.html");
    }

}