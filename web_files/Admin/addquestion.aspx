<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="addquestion.aspx.cs" Inherits="Admin_addquestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="quizfield" runat="server" />
    <asp:HiddenField ID="questionfield" runat="server" />    
    <div id="addquestiondiv" runat="server">
        <h2>Add a New Question</h2>
        <br />
        <div id="selecttypediv" runat="server">            
            <b>Select question type</b>&nbsp;&nbsp;
        <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="questiontypechanged">
            <asp:ListItem Text="Please select" Value="0" Selected="True" />
            <asp:ListItem Text="Single" Value="1" />
            <%--<asp:ListItem Text="Text" Value="2" />--%>
        </asp:DropDownList><br />
            <br />
        </div>
        <asp:Label ID="lblmessage" runat="server" ForeColor="#ff0000" Visible="false" /><br />
        <div style="clear: both"></div>
        <div id="singleoptiondiv" runat="server" visible="false">
            <b>Question</b><br />
            <asp:TextBox ID="txtsingleoption" runat="server" Height="23px" Width="400px" TextMode="MultiLine" />&nbsp;<asp:RequiredFieldValidator ID="singlequestionvalidator" runat="server" ControlToValidate="txtsingleoption" Display="Dynamic" ErrorMessage="please enter Question" SetFocusOnError="true" ForeColor="Red" ValidationGroup="singleoptionvalidation" /><br />
            <br />
            <b>option 1</b>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtsingleoption1" runat="server" Height="23px" Width="200px" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsingleoption1" Display="Dynamic" ErrorMessage="please enter option 1" SetFocusOnError="true" ForeColor="Red" ValidationGroup="singleoptionvalidation" /><br />
            <br />
            <b>option 2</b>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtsingleoption2" runat="server" Height="23px" Width="200px" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtsingleoption2" Display="Dynamic" ErrorMessage="please enter option 2" SetFocusOnError="true" ForeColor="Red" ValidationGroup="singleoptionvalidation" /><br />
            <br />
            <b>option 3</b>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtsingleoption3" runat="server" Height="23px" Width="200px" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtsingleoption3" Display="Dynamic" ErrorMessage="please enter option 3" SetFocusOnError="true" ForeColor="Red" ValidationGroup="singleoptionvalidation" /><br />
            <br />
            <b>option 4</b>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtsingleoption4" runat="server" Height="23px" Width="200px" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtsingleoption4" Display="Dynamic" ErrorMessage="please enter option 4" SetFocusOnError="true" ForeColor="Red" ValidationGroup="singleoptionvalidation" /><br />
            <br /><br />
            <asp:Button ID="singlequestionsubmit" runat="server" OnClick="singlequestionsubmit_Click" Text="Submit" Height="23px" Width="100px" ValidationGroup="singleoptionvalidation" /><br /><br />
            <asp:Label ID="lblanswer" runat="server" Font-Bold="true" ForeColor="Red" Visible="false">Please select the answer</asp:Label>&nbsp;&nbsp;
            <asp:DropDownList ID="ddlsingleanswer" runat="server" AutoPostBack="false" Visible="false">
                <asp:ListItem Text="option 1" Value="singleoption1" Selected="True" />
                <asp:ListItem Text="option 2" Value="singleoption2" />
                <asp:ListItem Text="option 3" Value="singleoption3" />
                <asp:ListItem Text="option 4" Value="singleoption4" />
            </asp:DropDownList><br /><br />
            <asp:Button ID="singleanswersubmit" runat="server" OnClick="singleanswersubmit_Click" Text="Submit" Height="23px" Width="100px" Visible="false" />
        </div>
        <div style="clear: both"></div>
        <div id="textoptiondiv" runat="server" visible="false">
            <b>Question</b><br />
            <asp:TextBox ID="txttextoption" runat="server" Height="23px" Width="400px" TextMode="MultiLine" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txttextoption" Display="Dynamic" ErrorMessage="please enter Question" SetFocusOnError="true" ForeColor="Red" ValidationGroup="textoptionvalidation" /><br />
            <br />
            <b>Answer</b><br />
            <asp:TextBox ID="txttextoptionanswer" runat="server" Text="" Height="23px" Width="200px" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txttextoptionanswer" Display="Dynamic" ErrorMessage="please enter Answer" SetFocusOnError="true" ForeColor="Red" ValidationGroup="textoptionvalidation" /><br />
            <br />
            <asp:Button ID="textoptionsubmit" runat="server" OnClick="textoptionsubmit_Click" Text="Submit" Height="23px" Width="100px" ValidationGroup="textoptionvalidation" />
        </div>
        <br /><br />        
    </div>
</asp:Content>

