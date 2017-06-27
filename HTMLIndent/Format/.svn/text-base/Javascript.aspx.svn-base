<%@ Page Title="Format Javascript" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Javascript.aspx.cs" Inherits="FormatJavascript" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Format Javascript</h2>
    <br />
    <div style="float: right">
        <asp:Label ID="lblTime" runat="server" Text=""></asp:Label></div>
    <asp:TextBox ID="txtInput" runat="server" TextMode="MultiLine" Style="width: 100%;
        height: 300px; overflow: scroll" Wrap="False"></asp:TextBox>
    <div class="right">
        <asp:LinkButton ID="LinkButton1" class="splash_button" runat="server" OnClick="Button1_Click"><span class="splash_button_pad">Format my code</span>
        </asp:LinkButton></div>
    <div class="push">
    </div>
    <div>
        <asp:CheckBox ID="chkExtract" runat="server" ValidationGroup="2" />
        <label for="ctl00_MainContent_chkExtract">
            &nbsp;Extract JavaScript &nbsp;&nbsp; <span class="link"><a href="javascript: void(0)">
                ?? <span><b>Extract Javascript </b>
                    <br>
                    <br>
                    Check this option if you need to note all the javascript code refered in a html
                    file.
                    <br />
                    <br />
                    You can copy paste HTML source and output will display formatted Javascipt.</span></a></span>
        </label>
    </div>
</asp:Content>
