<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="editquestion.aspx.cs" Inherits="Admin_editquestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="questionfield" runat="server" /> 
    <div id="editquestiondiv" runat="server">
        <h2>Edit Question</h2><br />
        <div id="singleoptiondiv" runat="server" visible="false">
            <b>Question</b><br /><asp:TextBox ID="txtsingleoption" runat="server" Height="23px" width="400px" TextMode="MultiLine"/>&nbsp;<asp:RequiredFieldValidator ID="singlequestionvalidator" runat="server" ControlToValidate="txtsingleoption" Display="Dynamic" ErrorMessage="please enter Question" SetFocusOnError="true" ForeColor="Red" ValidationGroup="singleoptionvalidation" /><br /><br />
            <b>option 1</b>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtsingleoption1" runat="server" Height="23px" width="200px" OnTextChanged="txtsingleoption_TextChanged" AutoPostBack="true"/>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsingleoption1" Display="Dynamic" ErrorMessage="please enter option 1" SetFocusOnError="true" ForeColor="Red" ValidationGroup="singleoptionvalidation" /><asp:HiddenField ID="hfoption1" runat="server" Visible="false" /><br /><br />
            <b>option 2</b>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtsingleoption2" runat="server" Height="23px" width="200px" OnTextChanged="txtsingleoption_TextChanged" AutoPostBack="true"/>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtsingleoption2" Display="Dynamic" ErrorMessage="please enter option 2" SetFocusOnError="true" ForeColor="Red" ValidationGroup="singleoptionvalidation" /><asp:HiddenField ID="hfoption2" runat="server" Visible="false" /><br /><br />
            <b>option 3</b>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtsingleoption3" runat="server" Height="23px" width="200px" OnTextChanged="txtsingleoption_TextChanged" AutoPostBack="true"/>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtsingleoption3" Display="Dynamic" ErrorMessage="please enter option 3" SetFocusOnError="true" ForeColor="Red" ValidationGroup="singleoptionvalidation" /><asp:HiddenField ID="hfoption3" runat="server" Visible="false" /><br /><br />
            <b>option 4</b>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtsingleoption4" runat="server" Height="23px" width="200px" OnTextChanged="txtsingleoption_TextChanged" AutoPostBack="true"/>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtsingleoption4" Display="Dynamic" ErrorMessage="please enter option 4" SetFocusOnError="true" ForeColor="Red" ValidationGroup="singleoptionvalidation" /><asp:HiddenField ID="hfoption4" runat="server" Visible="false" /><br /><br />
            <asp:Label ID="lblanswer" runat="server" Font-Bold="true" ForeColor="Red" Visible="false">Answer</asp:Label>&nbsp;&nbsp;
            <asp:DropDownList ID="ddlsingleanswer" runat="server" AutoPostBack="false" DataTextField="questionoption" DataValueField="id">                
            </asp:DropDownList><br /><br /><br />
            <asp:Button ID="singleoptionsubmit" runat="server" OnClick="singleoptionsubmit_Click" Text="Submit" Height="23px" Width="100px" ValidationGroup="singleoptionvalidation" />
        </div>
        <div style="clear:both"></div>
        <div id="textoptiondiv" runat="server" visible="false">
            <b>Question</b><br /><asp:TextBox ID="txttextoption" runat="server" Height="23px" width="400px" TextMode="MultiLine"/>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txttextoption" Display="Dynamic" ErrorMessage="please enter Question" SetFocusOnError="true" ForeColor="Red" ValidationGroup="textoptionvalidation" /><br /><br />
            <b>Answer</b><br /><asp:TextBox ID="txttextoptionanswer" runat="server" Text="" Height="23px" width="200px"/>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txttextoptionanswer" Display="Dynamic" ErrorMessage="please enter Answer" SetFocusOnError="true" ForeColor="Red" ValidationGroup="textoptionvalidation" /><br /><br />
            <asp:Button ID="textoptionsubmit" runat="server" OnClick="textoptionsubmit_Click" Text="Submit" Height="23px" Width="100px" ValidationGroup="textoptionvalidation" />
        </div>
        <br /><asp:Label ID="lblmessage" runat="server" ForeColor="#ff0000" Visible="false" /><br /><br />
    </div>    
</asp:Content>

