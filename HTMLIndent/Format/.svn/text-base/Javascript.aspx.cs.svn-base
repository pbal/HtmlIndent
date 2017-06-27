using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

public partial class FormatJavascript : System.Web.UI.Page
{
    private int spaceCount;

    public int SpaceCount
    {
        get { return spaceCount; }
        set { spaceCount = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private static string AddTab(int indentLevel)
    {
        return util.AddSpace(4, indentLevel);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            util.DBNull(txtInput.Text, Request.UserHostAddress.ToString(), "JavaSCript",
              false, false, false, 0);

        }
        catch (Exception)
        { }

        var st = new Stopwatch();
        st.Start();

        var indentLevel = 0;
        var inputText = txtInput.Text.Trim();

        if (!chkExtract.Checked && !inputText.StartsWith("<script>", StringComparison.InvariantCultureIgnoreCase))
        {
            inputText = "<script>" + inputText + "</script>";
        }

        #region jugad
        var splitOpen = new ArrayList();
        var openScript = false;
        var scriptEnded = false;
        var tempInnermu = new StringBuilder();
        var totalLength = inputText.Length;

        for (var j = 0; j < totalLength; j++)
        {
            int i;
            var openTagInside = false;

            tempInnermu.Remove(0, tempInnermu.Length);
            if (scriptEnded) scriptEnded = false;

            for (i = j; i < totalLength; i++)
            {
                var c = inputText[i];
                tempInnermu.Append(c);
                //bool splFlag = (util.GetCapitalAlphabet().Contains(c) ||
                //        util.GetLowerAlphabet().Contains(c) || c == ' ' ? true : false);
                //TODO: space anchor
                if ((i + 1) >= totalLength) break;

                if (c == '>')
                {
                    //if (splFlag)continue;
                    if (inputText[i - 1] == '?') continue;
                    if (scriptEnded)
                    {
                        scriptEnded = false;
                        break;
                    }
                    if (!openTagInside) continue;
                    openTagInside = false;
                    break;
                }

                if (c == '<')
                {
                    if (openTagInside) continue;
                    if (openScript)
                    {
                        #region script
                        if (inputText[i + 1] == '/' &&
                            inputText[i + 2].ToString().ToLower() == "s" &&
                            inputText[i + 3].ToString().ToLower() == "c" &&
                            inputText[i + 4].ToString().ToLower() == "r" &&
                            inputText[i + 5].ToString().ToLower() == "i" &&
                            inputText[i + 6].ToString().ToLower() == "p" &&
                            inputText[i + 7].ToString().ToLower() == "t"
                            )
                        {
                            openScript = false;
                            scriptEnded = true;
                            continue;
                        }
                        openTagInside = true;
                        #endregion
                        continue;
                    }
                    openTagInside = true;
                    continue;
                }

                if (inputText[i + 1] == '<' && inputText[i + 2] != ' ' && inputText[i + 2] != '\r' && inputText[i + 2] != '?')
                {
                    if (openTagInside && scriptEnded) continue;
                    break;
                }
            }
            j = i;
            if (string.IsNullOrEmpty(tempInnermu.ToString().Trim())) continue;
            splitOpen.Add(tempInnermu.ToString());
            if (tempInnermu.ToString().ToLower().StartsWith("<script")) openScript = true;
        }
        #endregion

        var outp = new StringBuilder();
        var q = new Stack();
        var content = false;

        // false = close ; true = open
        var tagState = false;
        var scriptTagParsedJustBefore = false;
        for (var loopIndex = 0; loopIndex < splitOpen.Count; loopIndex++)
        {
            var unAltered = splitOpen[loopIndex].ToString();
            if (String.IsNullOrEmpty(unAltered.Trim())) continue;

            if (indentLevel < 0) indentLevel = 0;

            var r = new StringBuilder(unAltered.Replace("\r\n", ""));

            string parent;


            tagState = true;
            content = false;

            parent = util.GetTagName(r, false).ToLower();

            q.Push(string.Equals(parent, "ahref") ? "a" : parent);

            while (r.ToString().Contains("  ")) r = r.Replace("  ", " ");
            r = r.Replace(" = ", "=").Replace("= ", "=").Replace(" =", "=");
            r = r.Replace(" >", ">");

            if (parent != "script") scriptTagParsedJustBefore = false;

            switch (parent)
            {
                case "script":
                    int i = loopIndex++;

                    var scriptContent = new StringBuilder();

                    var endTagLang = EndTagLanguage(splitOpen, loopIndex, "script");

                    while (!splitOpen[loopIndex].ToString().ToLower().Contains(endTagLang))
                    {
                        scriptContent.Append(splitOpen[loopIndex]);
                        loopIndex++;

                    }
                    if (!string.IsNullOrEmpty(scriptContent.ToString().Trim()))
                    {
                        if (chkExtract.Checked)
                        {
                            outp.Append(((!scriptTagParsedJustBefore) ? "\r\n" : "")
                                               + "\r\n" + AddTab(indentLevel) + r);
                            indentLevel++;
                        }
                        int indentSc = indentLevel;
                        var scriptc = Regex.Split(scriptContent.ToString(), @"([;{}])", RegexOptions.IgnoreCase);

                        foreach (string d in from s in scriptc
                                             where !string.IsNullOrEmpty(s.Trim())
                                             select s.Replace("\t", "").Trim())
                        {
                            string s = d;

                            switch (s)
                            {
                                case "{":
                                    outp.Append("\r\n" + AddTab(indentSc++) + s);
                                    break;
                                case "}":
                                    outp.Append("\r\n" + AddTab(--indentSc) + s);
                                    break;
                                case ";":
                                    outp.Append(s);
                                    break;
                                case "(":
                                    outp.Append(s);
                                    break;
                                case ")":
                                    outp.Append(s);
                                    break;
                                default:
                                    var c = Regex.Split(s, @"(\r\n)", RegexOptions.IgnoreCase);
                                    foreach (var s1 in c)
                                    {
                                        if (string.IsNullOrEmpty(s1.Trim())) continue;
                                        outp.Append("\r\n" + AddTab(indentSc) + s1.Trim());
                                    }

                                    break;
                            }
                        }
                        if (chkExtract.Checked)
                        {
                            outp.AppendLine("\r\n" + AddTab(--indentLevel) + splitOpen[loopIndex]);
                        }
                        scriptTagParsedJustBefore = true;
                    }
                    else
                    {
                        outp.AppendLine("\r\n" + AddTab(indentLevel) + splitOpen[i] + splitOpen[i + 1]);
                    }

                    q.Pop();
                    tagState = false;
                    continue;

                default:
                    if (r.ToString().TrimEnd().EndsWith("/>"))
                    {
                        q.Pop();
                        outp.Append("\r\n" + AddTab(indentLevel) + r);
                        continue;
                    }
                    //outp.Append("\r\n" + AddTab(indentLevel++) + r);
                    break;

            }
        }
        txtInput.Text = outp.ToString();
    }
    private static string EndTagLanguage(ArrayList splitOpen, int loopIndex, string tag)
    {
        var endTagLang = String.Empty;
        while (!splitOpen[loopIndex++].ToString().ToLower().Contains(tag))
        {
            if (splitOpen[loopIndex].ToString().ToLower().Contains("</" + tag + ">"))
            {
                endTagLang = "</" + tag + ">";
                break;
            }
            else if (splitOpen[loopIndex].ToString().ToLower().Contains(tag + ">"))
            {
                endTagLang = tag + ">";
                break;
            }
            else if (splitOpen[loopIndex].ToString().ToLower().Contains(tag))
            {
                endTagLang = tag;
                break;
            }
        }
        return ((endTagLang != "") ? endTagLang : "</" + tag + ">");
    }
}
