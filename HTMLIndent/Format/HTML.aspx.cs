﻿using System;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Data;
using System.Net;
using System.Web;

public partial class _1 : System.Web.UI.Page
{
    public const string emailNotification = "goldwort@gmail.com";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            drpIndentSpaces.SelectedValue = "4";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        //util.DBNull(txtInput.Text,
        //    Request.UserHostAddress.ToString(),
        //    "HTML",
        //    chkFormatCss.Checked,
        //    chkFormatJavascript.Checked,
        //    chkCompress.Checked, Int32.Parse(drpIndentSpaces.SelectedValue)
        //    );

        var st = new Stopwatch();
        st.Start();

        //Indent.Code n = new Indent.Code();
        IndentLibrary.Indent n = new IndentLibrary.Indent();

        n.CompressFlag = chkCompress.Checked;
        n.FormatCSS = chkFormatCss.Checked;
        n.FormatJS = chkFormatJavascript.Checked;
        n.ReplaceASCII = chkAscii.Checked;
        n.Spaces = Int32.Parse(drpIndentSpaces.SelectedValue);

        txtInput.Text = n.Beautify(txtInput.Text);

        if (chkCompress.Checked) txtInput.Wrap = true;
        else txtInput.Wrap = false;

        lblSize.Text = "Code size : <font color='red'>" + n.CodeSize + " Kb</font>";
        Label3.Text = "Processing Time : <font color='red'>" + st.ElapsedMilliseconds + " ms</font>";
    }
}