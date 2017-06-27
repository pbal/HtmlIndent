<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="HTML.aspx.cs"
    Inherits="_1" ValidateRequest="false" Title="HTML Indent and Formating Tool" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        HTML Formating Tool</h2>
    <br />
    <div id="Div1" style="x-overflow: scroll;">
        <div style="float: right">
            <asp:Literal ID="lblSize" runat="server"></asp:Literal>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:Literal
                ID="Label3" runat="server"></asp:Literal>
        </div>
        <br />
        <br />
        <div class="clear">
        </div>
        <asp:TextBox ID="txtInput" runat="server" TextMode="MultiLine" Style="width: 100%;
            height: 300px; overflow: scroll" Wrap="False"></asp:TextBox>
        <div class="left" style="margin-top: 10px">
            Example source code: <a title="Click to open source code in new tab" href="view-source:http://www.google.com"
                target="_blank">Google</a>, <a href="view-source:http://www.yahoo.com" target="_blank"
                    title="Click to open source code in new tab">Yahoo</a>, <a href="view-source:http://www.dailymail.co.uk"
                        target="_blank" title="Click to open source code in new tab">Daily Mail</a>.
        </div>
        <div class="right">
            <asp:LinkButton ID="Button1" class="splash_button" runat="server" OnClick="Button1_Click"><span class="splash_button_pad">Format my code</span>
            </asp:LinkButton>
        </div>
        <div class="clear">
        </div>
        <div class="clear">
        </div>
        <h3>
            Options</h3>
        <table class="options">
            <tr>
                <td width="600px">
                    <asp:CheckBox ID="chkFormatCss" runat="server" Checked="true" Text="Format CSS" ValidationGroup="1" />
                    &nbsp;&nbsp; <span class="link"><a href="javascript: void(0)">[?] <span><b>Format CSS
                    </b>
                        <br>
                        <br>
                        You can choose to maintain the original format of CSS by unchecking this option.
                    </span></a></span>
                </td>
                <td>
                    <asp:CheckBox ID="chkCompress" runat="server" ValidationGroup="2" />
                    <label for="ctl00_MainContent_chkCompress">
                        &nbsp;Compress code &nbsp;&nbsp; <span class="link"><a href="javascript: void(0)">
                            [?] <span><b>Why compress?</b><br>
                                <br>
                                Removing white spaces from your code reduces the size of your html file, therefore
                                reducing the time it takes to load your website. </span></a></span>
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkFormatJavascript" runat="server" Checked="true" Text="Format Javascript"
                        ValidationGroup="1"></asp:CheckBox>&nbsp;&nbsp; <span class="link"><a href="javascript: void(0)">
                            [?] <span><b>Format JavaScript </b>
                                <br>
                                <br>
                                Unchecking this option will maintain the original struture of your Javascript.
                            </span></a></span>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkAscii" runat="server" Text="Replace symbols with ASCII characters"
                        ValidationGroup="1"></asp:CheckBox>&nbsp;&nbsp; <span class="link"><a href="javascript: void(0)">
                            [?] <span><b>Replace Symbols</b><br>
                                <br>
                                Check this option to replace symbols in your code with respective ASCII values.
                                For example: <b>&copy;</b> will be replaced by <b>&amp;copy;</b> </span></a>
                        </span>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="drpIndentSpaces" runat="server" ValidationGroup="1">
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    <label for="ctl00_MainContent_drpIndentSpaces">
                        &nbsp;Number of spaces in an indent</label>
                    &nbsp;&nbsp; <span class="link"><a href="javascript: void(0)">[?] <span><b>Indent spaces</b><br>
                        <br>
                        Choose number of spaces you want to express in an indent. Larger the number of white
                        spaces in a code, greater will be its size.</span></a> </span>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div class="push">
    </div>
    <div class="push">
    </div>
    <script type="text/javascript">
        window.___gcfg = {
            lang: 'en-US'
        };

        (function () {
            var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
            po.src = 'https://apis.google.com/js/plusone.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
        })();
    </script>
</asp:Content>
