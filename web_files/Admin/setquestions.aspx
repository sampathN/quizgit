<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="setquestions.aspx.cs" Inherits="Admin_setquestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="questionsdiv" runat="server">
        <h2>Available Questions</h2><br />
        <asp:Repeater ID="currrepeater" runat="server" OnItemCommand="currrepeater_ItemCommand">
            <HeaderTemplate>
                <table style="width: 100%">
                    <tr style="background-color: Gray; color: White;">
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">Question</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">Type</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">&nbsp;</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">&nbsp;</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="background-color: #ffffff;">
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:Label ID="lblquizname" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "title")%>' Font-Bold="true"></asp:Label>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:Label ID="lblfromdate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "type")%>' ForeColor="Tomato" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") + "|" +  DataBinder.Eval(Container.DataItem, "type") %>' CausesValidation="false">Edit</asp:LinkButton>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' CausesValidation="false">Delete</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <br />
        <asp:HiddenField ID="quizfield" runat="server" /><hr />  
        <br /><h2>Available Options</h2><br />
        <p><a href="addquestion" id="addquestionlnk" runat="server">Add a new question</a></p> <br />
    </div>   
</asp:Content>

