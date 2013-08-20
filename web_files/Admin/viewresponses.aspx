<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="viewresponses.aspx.cs" Inherits="Admin_viewresponses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="quizfield" runat="server" />
    <asp:Label ID="lblmessage" runat="server" ForeColor="#ff0000" Visible="false" /><br />
    <div id="responsesdiv" runat="server">
        <h2>Available Responses</h2>
        <br />
        <br />
        <asp:Repeater ID="responsesrpt" runat="server">
            <HeaderTemplate>
                <table style="width: 100%">
                    <tr style="background-color: Gray; color: White;">
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">Name</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">Email</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">Correct Answers</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">Wrong Answers</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="background-color: #ffffff;">
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:Label ID="lblsalonname" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name")%>' Font-Bold="true"></asp:Label>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:Label ID="lblemail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "email")%>' Font-Bold="true"></asp:Label>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:Label ID="lblcorrectanswers" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "correctanswers")%>' ForeColor="Green" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:Label ID="lblwronganswers" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "wronganswers")%>' ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>        
    </div>
    <div style="clear: both"></div>
    <div id="exportdiv" runat="server">
        <br />
        <hr />
        <br />
        <h4>Export the result</h4>
        <br />
        <b>Select Type</b>&nbsp;&nbsp;<asp:DropDownList ID="fileexporttype" runat="server">
            <asp:ListItem Selected="True">Excel</asp:ListItem>            
            <asp:ListItem>Word</asp:ListItem>
            <asp:ListItem>PDF</asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="exportbutton" runat="server" Text="Export" OnClick="exportbutton_click" Width="100px" Height="25px" CausesValidation="false" /><br />
        <br />
    </div>
</asp:Content>

