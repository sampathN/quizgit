<%@ Page Title="Product quiz Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="headerContent" ContentPlaceHolderID="HeadContent" runat="server">  
</asp:Content>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Label ID="lblmessage" runat="server" ForeColor="#ff0000" Visible="false" /><br />
    <asp:HiddenField ID="quizfield" runat="server" />
    <!-- quiz details -->
    <div id="quizdetails" runat="server">
        <!-- quiz title -->
        <asp:Label ID="lblquizname" runat="server" CssClass="quizname" />
        <br />
        <br />

        <!-- description -->
        <asp:Label ID="lbldescription" runat="server" CssClass="quizdesc" />
        <br />
        <br />
    </div>
    <div style="clear: both"></div>

    <!-- quiz questions -->
    <div id="quiz">
        <asp:ValidationSummary ID="quizvalidationsummary" runat="server" ShowMessageBox="false" DisplayMode="BulletList" ShowSummary="true" HeaderText="<br />&nbsp;&nbsp;Please check the following:-" ForeColor="Red" ValidationGroup="quizvalidation" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" Width="280px" /><br />
        <!-- user details -->
        <div id="detailsdiv" runat="server">
            <fieldset>
                <legend>Please fill your details</legend>
                <ol>
                    <li>
                        <asp:Label ID="lblname" runat="server" AssociatedControlID="txtname">Name</asp:Label>
                        <asp:TextBox runat="server" ID="txtname" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtname" Display="Dynamic" CssClass="field-validation-error" ErrorMessage="name required" ValidationGroup="quizvalidation" Text="*" SetFocusOnError="true" />
                    </li>
                    <li>
                        <asp:Label ID="lblemail" runat="server" AssociatedControlID="txtemail">Email Address</asp:Label>
                        <asp:TextBox runat="server" ID="txtemail" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtemail" Display="Dynamic" CssClass="field-validation-error" ErrorMessage="email required" ValidationGroup="quizvalidation" Text="*" SetFocusOnError="true" />
                    </li>                    
                </ol>
            </fieldset>
        </div>
        <!-- questions -->
        <div id="questionsdiv" runat="server">
            <asp:Label ID="lblalert" runat="server" ForeColor="Red" Font-Size="15px" Visible="false" /><br />            
            <asp:Repeater ID="questionsrpt" runat="server" OnItemDataBound="questionsrpt_ItemDataBound">
                <ItemTemplate>
                    <asp:HiddenField ID="hfID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' Visible="false" />
                    <asp:RequiredFieldValidator ID="rfvquiz" runat="server" Display="Dynamic" ControlToValidate="rbloptions" ValidationGroup="quizvalidation" ForeColor="Red" Text="*" SetFocusOnError="true"/>&nbsp;<asp:Label ID="lblquestion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "title")%>' /><br />
                    <asp:RadioButtonList ID="rbloptions" runat="server" ValidationGroup="quizvalidation" />                    
                </ItemTemplate>
            </asp:Repeater>
            <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Submit" ValidationGroup="quizvalidation" />
        </div>
    </div>
</asp:Content>