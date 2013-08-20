using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_setquestions : System.Web.UI.Page
{
    string quizdetailstable = "quizdetails";
    string quizquestionstable = "quiz_questions";
    string quizquestionoptionstable = "question_options";
    string quizquestionanswertable = "question_answer";
    string qstring = "0";
    string qtype = "";
    int quizId = 0;
    int tempval = 0;
    int qorder = 0;
    int questid = 0;
    DateTime updatedate = new DateTime();

    protected void Page_Load(object sender, EventArgs e)
    {
        updatedate = DateTime.Now;

        if (!Page.IsPostBack)
        {
            //get the query string value
            if (Page.Request["q"] == null)
            {
                questionsdiv.InnerHtml = "<br /><span style='color:#FF0000; font-size:15px;'>Please select a quiz event first and try again.</span>";
            }
            else
            {
                qstring = Page.Request["q"].ToString();
                quizId = Convert.ToInt32(qstring);

                //strore the quiz id in hidden field
                quizfield.Value = qstring;
                bindquestions();
            }
        }
        else
        {
            if (int.TryParse(quizfield.Value, out tempval) == true)
            {
                quizId = tempval;
            }
            else
            {
                questionsdiv.InnerHtml = "<br /><span style='color:#FF0000; font-size:15px;'>Please select a quiz event first and try again.</span>";
            }
        }

        //set the new question link
        addquestionlnk.HRef = "addquestion?q=" + quizId;
    }

    protected void bindquestions()
    {
        DataTable dTable = new DataTable();
        SqlCommand getquestions = new SqlCommand("select id, title, type from " + quizquestionstable + " where quizid=@quizid order by questionorder ASC");
        getquestions.Parameters.AddWithValue("quizid", quizId);

        db getquestionslist = new db();
        dTable = getquestionslist.returnDataTable(getquestions);

        if (dTable.Rows.Count > 0)
        {
            questionsdiv.Visible = true;

            currrepeater.DataSource = dTable;
            currrepeater.DataBind();
        }
        else
        {
            questionsdiv.Visible = true;
            questionsdiv.InnerHtml = "<span style='color:#FF0000; font-size:15px;'>Nothing available at the moment</span>";
        }
    }

    //edit quiz
    protected void currrepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string allKeys = Convert.ToString(e.CommandArgument);

        string[] arrKeys = new string[1];
        char[] splitter = { '|' };
        arrKeys = allKeys.Split(splitter);

        if (e.CommandName == "edit")
        {
            Response.Redirect("editquestion?q=" + arrKeys[0], false);
        }
        else if (e.CommandName == "delete")
        {
            SqlCommand deletequestion = new SqlCommand("delete from " + quizquestionstable + " where id=@questionid");
            deletequestion.Parameters.AddWithValue("questionid", arrKeys[0]);

            db deleterequest = new db();
            deleterequest.ExecuteQuery(deletequestion);

            bindquestions();
        }
    }
}