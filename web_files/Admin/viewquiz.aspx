<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="viewquiz.aspx.cs" Inherits="Admin_viewquiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="availablecompetitions">
        <h2>Recent quizes</h2>
        <br />
        <asp:Label ID="lblmessage" runat="server" ForeColor="#ff0000" Visible="false" />
        <asp:Repeater ID="currrepeater" runat="server" Visible="false" OnItemCommand="currrepeater_ItemCommand">
            <HeaderTemplate>
                <table style="width: 100%">
                    <tr style="background-color: Gray; color: White;">
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">Name</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">Start Date</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">End Date</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">&nbsp;</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">&nbsp;</td>
                        <td style="height: 25px; padding-left: 10px; font-weight: bold;">&nbsp;</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="background-color: #ffffff;">
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:Label ID="lblquizname" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name")%>' Font-Bold="true"></asp:Label>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:Label ID="lblfromdate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "startdate", "{0:dd/MM/yyyy}")%>' ForeColor="Green" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:Label ID="lbltodate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "enddate", "{0:dd/MM/yyyy}")%>' ForeColor="Red" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:LinkButton ID="lnkquestions" runat="server" CommandName="questions" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' CausesValidation="false">Questions</asp:LinkButton>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:LinkButton ID="lnkView" runat="server" CommandName="responses" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' CausesValidation="false">Responses</asp:LinkButton>
                    </td>
                    <td style="height: 25px; padding-left: 10px;">
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' CausesValidation="false">Edit</asp:LinkButton>
                    </td>

                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <br />
    <hr />
    <br />
    <h2>Available Options</h2>
    <br />
    <div id="quizsetup">
        <p><a href="newquiz">Start a new quiz</a></p>
    </div>
    <br />
</asp:Content>

