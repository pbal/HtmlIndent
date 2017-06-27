using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace IndentLibrary
{
    public class Indent
    {
        public string CodeSize { get; set; }
        public bool CompressFlag { get; set; }
        public bool FormatCSS { get; set; }
        public bool FormatJS { get; set; }
        public bool ReplaceASCII { get; set; }
        public int Spaces { get; set; }

        private string AddTab(int indentLevel)
        {
            return AddSpace(Spaces, indentLevel);
        }

        public static string AddSpace(int spaceCount, int indentLevel)
        {
            var tabLength = string.Empty;
            for (int i = 0; i < spaceCount * indentLevel; i++)
            {
                tabLength += " ";
            }
            return tabLength;
        }
        private void BlockText(int indentLevel, StringBuilder outp, StringBuilder r, bool content)
        {
            var spli = r.ToString().Trim().Split(' ');
            var sb = new StringBuilder();
            //outp.Append(AddTab(indentLevel));
            outp.Append(((content) ? AddTab(indentLevel) : " "));

            for (int i = 0; i < spli.LongLength; i++)
            {
                var s = spli[i];
                if (string.IsNullOrEmpty(s.Trim())) continue;

                if ((sb.ToString().Trim().Length + s.Length < 100))
                {
                    sb.Append(s);
                    if (i + 1 < spli.LongLength)
                        sb.Append(" ");
                }
                else
                {
                    outp.Append(sb.Append("\r\n" + AddTab(indentLevel) + s + " "));
                    sb.Remove(0, sb.Length);
                }
            }
            outp.Append(sb);
            //return sb;
        }

        private string Compress(string input)
        {
            var output = new StringBuilder(input.Replace("\r\n", "").Replace("\t", ""));

            while (output.ToString().Contains("  "))
                output.Replace("  ", " ");

            return output.ToString();
        }
        public static string GetTagName(StringBuilder r, bool bClosingTag)
        {
            var ar = r.ToString().Split(' ', '=', '\r');
            return !bClosingTag ? ar[0].Trim('<', '>', ' ') : ar[ar.Length - 1].TrimEnd('>').TrimStart('<', '/', '\\');
        }
        public string Beautify(string txtInput)
        {
            Encoding encoding = Encoding.UTF8;
            if (CompressFlag)
            {
                string compressed = Compress(txtInput);
                CodeSize = (encoding.GetByteCount(compressed) / 1000).ToString("#,##0");
                return compressed;
            }

            var indentLevel = 0;

            #region jugad
            var splitOpen = new ArrayList();
            var openScript = false;
            var scriptEnded = false;
            var tempInnermu = new StringBuilder();
            var totalLength = txtInput.Length;

            for (var j = 0; j < totalLength; j++)
            {
                int i;
                var openTagInside = false;

                tempInnermu.Remove(0, tempInnermu.Length);
                if (scriptEnded) scriptEnded = false;

                for (i = j; i < totalLength; i++)
                {
                    var c = txtInput[i];
                    tempInnermu.Append(c);
                    //bool splFlag = (util.GetCapitalAlphabet().Contains(c) ||
                    //        util.GetLowerAlphabet().Contains(c) || c == ' ' ? true : false);
                    //TODO: space anchor
                    if ((i + 1) >= totalLength) break;

                    if (c == '>')
                    {
                        //if (splFlag)continue;
                        //if (inputText[i - 1] == '?') continue;
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
                            if (txtInput[i + 1] == '/' &&
                                txtInput[i + 2].ToString().ToLower() == "s" &&
                                txtInput[i + 3].ToString().ToLower() == "c" &&
                                txtInput[i + 4].ToString().ToLower() == "r" &&
                                txtInput[i + 5].ToString().ToLower() == "i" &&
                                txtInput[i + 6].ToString().ToLower() == "p" &&
                                txtInput[i + 7].ToString().ToLower() == "t"
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

                    if (txtInput[i + 1] == '<' && txtInput[i + 2] != ' ' && txtInput[i + 2] != '\r')// && inputText[i + 2] != '?')
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

                //tOdo : PHP
                var r = new StringBuilder(unAltered);//.Replace("\r\n", ""));

                string parent;
                if (r.ToString().StartsWith("</") || r.ToString().StartsWith("<\\/"))
                {
                    #region "</"

                    parent = GetTagName(r, true).ToLower();
                    Array arQueue = q.ToArray();
                    Array.Reverse(arQueue);
                    var temp = new Stack(arQueue);

                    var tempIndent = indentLevel;

                    if ((!content) && tagState)
                    {
                        tagState = false;
                        q.Pop();
                        --indentLevel;
                        outp.Append(r);
                        continue;
                    }

                    // pop all invalid and reduce tab level
                    if (q.Count > 0)
                        while (parent != q.Peek().ToString())
                        {
                            q.Pop();
                            --indentLevel;
                            if (q.Count == 0 || indentLevel < 0) break;
                        }

                    if (indentLevel <= 0)
                    {
                        indentLevel = tempIndent;
                        q = temp;
                        outp.Append("\r\n" + AddTab(indentLevel) + r);
                        continue;
                    }

                    if (q.Count > 0)
                        q.Pop();

                    outp.Append("\r\n" + AddTab(--indentLevel) + r);

                    #endregion
                }
                else if (r.ToString().StartsWith("<"))
                {
                    #region <

                    tagState = true;
                    content = false;
                    if (r.ToString().StartsWith("<!"))
                    {
                        outp.Append("\r\n" + AddTab(indentLevel) + r);
                        tagState = false;
                        continue;
                    }
                    parent = GetTagName(r, false).ToLower();

                    if (string.Equals(parent, "ahref", StringComparison.InvariantCultureIgnoreCase))
                        q.Push("a");
                    if (string.Equals(parent, "?php", StringComparison.InvariantCultureIgnoreCase))
                        q.Push("?");
                    else
                        q.Push(parent);

                    //while (r.ToString().Contains("  ")) r = r.Replace("  ", " ");
                    r = r.Replace(" = ", "=").Replace("= ", "=").Replace(" =", "=");
                    r = r.Replace(" >", ">");

                    if (parent != "script") scriptTagParsedJustBefore = false;

                    switch (parent)
                    {
                        case "body":
                            outp.Append("\r\n" + AddTab(indentLevel++) + r);
                            continue;
                        case "html":
                            outp.Append("\r\n" + r);
                            continue;
                        case "?php":
                            #region PHP
                            outp.Append("\r\n");//+ AddTab(indentLevel++));
                            int identPHP = indentLevel;

                            var phpString = Regex.Split(r.ToString().Trim().Replace("\t", ""), @"([;{}])\r\n");

                            do
                            {
                                foreach (var sd in phpString)
                                {
                                    string ss = sd;
                                    //ss = ss.Replace("\r\n", "");
                                    //while (ss.Contains("  ")) ss = ss.Replace("  ", " ");

                                    switch (ss.Trim())
                                    {
                                        //case "{":
                                        //    outp.AppendLine(" " + ss);
                                        //    identPHP++;
                                        //    break;
                                        //case "}":
                                        //    outp.AppendLine("\r\n" + AddTab(--identPHP) + ss.Trim());
                                        //    break;
                                        case ";":
                                            outp.AppendLine(ss.Trim());
                                            break;
                                        default:
                                            if (!string.IsNullOrEmpty(ss.Trim()))
                                                outp.Append(AddTab(identPHP) + ss.Trim().Replace("\r\n", "\r\n" + AddTab(identPHP)));
                                            break;
                                    }
                                }
                            }
                            while (!splitOpen[loopIndex++].ToString().Contains("?"));
                            //outp.Append(AddTab(--indentLevel) + splitOpen[loopIndex]);
                            q.Pop();
                            loopIndex--;
                            tagState = false;
                            #endregion

                            continue;
                        case "style":
                            #region Style
                            if (!FormatCSS)
                            {
                                outp.Append("\r\n" + AddTab(indentLevel) + r);

                                continue;
                            }

                            outp.AppendLine("\r\n" + AddTab(indentLevel++) + r);
                            int styleIndent = indentLevel;

                            var endTagLangStyle = EndTagLanguage(splitOpen, loopIndex + 1, "style");

                            while (!splitOpen[++loopIndex].ToString().ToLower().Contains(endTagLangStyle))
                            {
                                var s = splitOpen[loopIndex].ToString();

                                if (string.IsNullOrEmpty(s.Trim()))
                                    continue;

                                var styleString = Regex.Split(s, @"([;{}])");
                                foreach (var sd in styleString)
                                {
                                    string ss = sd;
                                    ss = ss.Replace("\r\n", "");
                                    while (ss.Contains("  ")) ss = ss.Replace("  ", " ");
                                    ss = ss.Replace(":", ": ");
                                    ss = ss.Replace(",", ", ");
                                    switch (ss.Trim())
                                    {
                                        case "{":
                                            outp.AppendLine(" " + ss);
                                            styleIndent++;
                                            break;
                                        case "}":
                                            outp.AppendLine("\r\n" + AddTab(--styleIndent) + ss.Trim());
                                            break;
                                        case ";":
                                            outp.AppendLine(ss.Trim());
                                            break;
                                        default:
                                            if (!string.IsNullOrEmpty(ss.Trim()))
                                                outp.Append(AddTab(styleIndent) + ss.Trim());
                                            break;
                                    }
                                }
                            }
                            outp.Append(AddTab(--indentLevel) + splitOpen[loopIndex]);
                            q.Pop();
                            tagState = false;
                            #endregion
                            continue;

                        case "script":
                            #region "JavaSscript"

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
                                if (!FormatJS)
                                {
                                    scriptContent.Append(splitOpen[loopIndex]);
                                    outp.Append("\r\n" + AddTab(indentLevel) + r + scriptContent.ToString() + "\r\n");
                                    q.Pop();
                                    continue;
                                }

                                outp.Append(((!scriptTagParsedJustBefore) ? "\r\n" : "")
                                    + "\r\n" + AddTab(indentLevel) + r);
                                indentLevel++;
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
                                            //if (paranthesisOpen != 0)
                                            //    outp.Append(s);
                                            //else
                                            outp.Append("\r\n" + AddTab(indentSc++) + s);
                                            break;
                                        case "}":
                                            //if (paranthesisOpen != 0)
                                            //    outp.Append(s);
                                            //else
                                            outp.Append("\r\n" + AddTab(--indentSc) + s);
                                            break;
                                        case ";":
                                            outp.Append(s);
                                            break;
                                        case "(":
                                            outp.Append(s);
                                            //paranthesisOpen++;
                                            break;
                                        case ")":
                                            outp.Append(s);
                                            //paranthesisOpen--;
                                            break;
                                        default:
                                            //if (paranthesisOpen != 0)
                                            //{
                                            //    outp.Append(s.Trim());
                                            //    //paranthesisOpen = false;
                                            //    break;
                                            //}

                                            var c = Regex.Split(s, @"(\r\n)", RegexOptions.IgnoreCase);
                                            //if (s[0] == '.' || s[0] == ':')
                                            //    outp.Append(s.Trim());
                                            //else
                                            foreach (var s1 in c)
                                            {
                                                if (string.IsNullOrEmpty(s1.Trim())) continue;
                                                outp.Append("\r\n" + AddTab(indentSc) + s1.Trim());
                                            }

                                            break;
                                    }
                                }
                                outp.AppendLine("\r\n" + AddTab(--indentLevel) + splitOpen[loopIndex]);
                                scriptTagParsedJustBefore = true;
                            }
                            else
                            {
                                outp.AppendLine("\r\n" + AddTab(indentLevel) + splitOpen[i] + splitOpen[i + 1]);
                            }

                            q.Pop();
                            tagState = false;

                            #endregion
                            continue;

                        case "input":
                        case "!doctype":
                        case "param":
                        case "area":
                        case "layer":
                        case "hr":
                        case "br":
                        case "meta":
                        case "link":
                        case "img":
                            content = true;
                            q.Pop();
                            outp.Append("\r\n" + AddTab(indentLevel) + r);
                            continue;

                        default:
                            if (r.ToString().TrimEnd().EndsWith("/>"))
                            {
                                q.Pop();
                                outp.Append("\r\n" + AddTab(indentLevel) + r);
                                continue;
                            }
                            outp.Append("\r\n" + AddTab(indentLevel++) + r);
                            break;
                    }
                    #endregion
                }
                else
                {
                    #region Text Block
                    tagState = true;

                    if (ReplaceASCII)
                        r = new StringBuilder(System.Web.HttpUtility.HtmlEncode(r.ToString()).Replace("&amp;", "&"));

                    if (q.Count != 0)
                    {
                        switch (q.Peek().ToString().ToLower())
                        {
                            case "title":
                            case "meta":
                            case "label":
                            //case "a":
                            case "b":
                            case "strong":
                            case "option":
                                content = false;
                                break;
                            default:
                                content = true;
                                break;
                        }
                    }

                    outp.Append(((content) ? "\r\n" : ""));
                    BlockText(indentLevel, outp, r, content);

                    #endregion
                }
            }

            CodeSize = (encoding.GetByteCount(outp.ToString()) / 1000).ToString("#,##0");
            return outp.ToString();
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
}
