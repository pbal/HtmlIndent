<%@ Page Title="HTML Indent and Formating Tool" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="About" %>

<%@ Register Assembly="FreshClickmedia.Web" Namespace="FreshClickMedia.Web.UI.WebControls"
    TagPrefix="fcm" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            display: inline-block;
            margin: 10px auto;
            border: 1px solid #cdcdcd;
            padding: 5px;
            background-color: #efefef;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        HTML Indenting Tool</h2>
    <div style="width: 700px">
        <p>
            This tool indents HTML source in a way which improves readability and understanding
            of tags. This helps in differentiating between deeply nested tags and identifying
            child tags in a parent tag.
        </p>
        <p>
            You can indent HTML source file containing JavaScript, Style tags and programming
            tags of php and asp. This application delivers neatly tabbed code with fastest performance
            compared some of other popular HTML formating tools. You can try cleaning up untidy
            HTML source of <a title="Click this link to open source in new tab" href="view-source:http://www.google.com"
                target="_blank">Google</a> and lengthy code such as of <a href="view-source:http://www.dailymail.co.uk"
                    target="_blank" title="Click this link to open source in new tab">Daily Mail</a>.
        </p>
        Some of the things which this application does pretty well:
        <ul>
            <li>Indents HTML tags</li>
            <li>Indents HTML files containing php and asp code</li>
            <li>Indents CSS</li>
            <li>Indents Javascript</li>
            <li>Replaces ASCII values for symbols.</li>
            <li>Creates text blocks for lengthy content</li>
        </ul>
        <br />
        <p class="style1  ">
            <img alt="html indented code vs untidy code" src="sample.jpg" /></p>
        <div class="clear">
        </div>
        <p>
            Feel free to <a href="Format/HTML.aspx">abuse this app</a> and share comments. Happy
            indenting!!</p>
        <div style="clear: both">
        </div>
        <div>
            <h3 style="background-color: #efefef; border-bottom: 1px solid #cdcdcd; margin: 20px 0">
                Comments
            </h3>
            <div style="clear: both">
            </div>
            <%--  <asp:UpdatePanel ID="MyUpdatePanel" runat="server">
                <ContentTemplate>--%>
            <%-- <asp:Panel ID="TimerPanel" runat="server">
                        <asp:Timer ID="PageLoadTimer" runat="server" Interval="1" OnTick="timerTick" />
                    </asp:Panel>
                    <asp:UpdateProgress ID="PageProgressIndicator" runat="server">
                        <ProgressTemplate>
                            <p>
                                Loading comments...</p>
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="left" style="width: 110px;">
                        <div class="inside">
                            <fcm:Gravatar ID="Gravatar1" runat="server" CssClass='avatar avatar-70 photo avatar-default'
                                Height='70' Width='70' Enabled="true" DefaultImage="http://purehtml.in/grvdefault.jpg"
                                EnableTheming="False" OutputGravatarSiteLink="False" Email='<%# DataBinder.Eval(((System.Web.UI.WebControls.RepeaterItem)(Container)).DataItem, "email") %>' />
                            <p>
                            </p>
                        </div>
                    </div>
                    <div class="left" style="width: 450px;">
                        <i>
                            <%# DataBinder.Eval(((System.Web.UI.WebControls.RepeaterItem)(Container)).DataItem, "Name").ToString().Trim() == "" ?
    DataBinder.Eval(((System.Web.UI.WebControls.RepeaterItem)(Container)).DataItem, "email").ToString().Trim()  :DataBinder.Eval(((System.Web.UI.WebControls.RepeaterItem)(Container)).DataItem, "Name").ToString().Trim() %>
                            wrote.. </i>
                        <p>
                            <%# DataBinder.Eval(((System.Web.UI.WebControls.RepeaterItem)(Container)).DataItem, "comments") %>
                        </p>
                        <p style="font-size: 10px">
                            <%# DataBinder.Eval(((System.Web.UI.WebControls.RepeaterItem)(Container)).DataItem, "dtdate", "{0:MMMM d, yyyy} at {0:hh:mm}") %></p>
                    </div>
                </ItemTemplate>
                <SeparatorTemplate>
                    <div style="clear: both">
                        <hr style="border: 1px solid #e4e4e4" />
                    </div>
                </SeparatorTemplate>
            </asp:Repeater>
            <%--    </ContentTemplate>
            </asp:UpdatePanel>--%>
            <div style="clear: both">
            </div>
            <table width="100%" id="review">
                <tr>
                    <td width="110px">
                        Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="181px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="110px">
                        Email
                    </td>
                    <td>
                        <asp:TextBox ID="txtcmtEmail" runat="server" Width="181px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter email"
                            ControlToValidate="txtcmtEmail">Please enter email</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Comments
                        <br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtreview" CssClass="zn" TextMode="MultiLine" runat="server" Width="500px"
                            Height="200px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=""
                            ControlToValidate="txtreview">Please enter comments</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                    </td>
                    <td>
                        <asp:LinkButton ID="btnSubmit" class="splash_button" runat="server" OnClick="btnSubmit_Click"><span class="splash_button_pad">Submit Comments</span>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
