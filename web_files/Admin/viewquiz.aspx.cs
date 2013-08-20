using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_viewquiz : System.Web.UI.Page
{
    string quizdetailstable = "quizdetails";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bindquiz();
        }

        //set the home link
        HyperLink homelink = (HyperLink)Master.FindControl("homelnk");
        if (homelink != null)
        {
            homelink.NavigateUrl = "viewquiz";
        }
    }

    //get the recent quiz
    protected void Bindquiz()
    {
        DataTable dTable = new DataTable();
        SqlCommand getquizes = new SqlCommand("select id, name, startdate, enddate from " + quizdetailstable);

        db getquizeslist = new db();
        dTable = getquizeslist.returnDataTable(getquizes);

        if (dTable.Rows.Count > 0)
        {
            currrepeater.Visible = true;
            lblmessage.Visible = false;

            currrepeater.DataSource = dTable;
            currrepeater.DataBind();
        }
        else
        {
            currrepeater.Visible = false;
            lblmessage.Visible = true;
            lblmessage.Text = "Nothing available at the moment" + "<br /><br />";
        }
    }

    //edit quiz
    protected void currrepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int quizid = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "edit")
        {
            Response.Redirect("editquiz?q=" + quizid, false);
        }
        else if (e.CommandName == "responses")
        {
            Response.Redirect("viewresponses?q=" + quizid, false);
        }
        else if (e.CommandName == "questions")
        {
            Response.Redirect("setquestions?q=" + quizid, false);
        }
        else
        {
        }
    }
}