using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;

public partial class Admin_editquestion : System.Web.UI.Page
{
    string quizdetailstable = "quizdetails";
    string quizquestionstable = "quiz_questions";
    string quizquestionoptionstable = "question_options";
    string quizquestionanswertable = "question_answer";
    string qstring = "0";
    int qId = 0;
    int tempval = 0;
    DateTime updatedate = new DateTime();

    string qtype = "";
    string questionstr = "";
    string answerstr = "";
    string textoptionstr = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        updatedate = DateTime.Now;

        if (!Page.IsPostBack)
        {
            //get the query string value
            if (Page.Request["q"] == null)
            {
                editquestiondiv.InnerHtml = "<br /><span style='color:#FF0000; font-size:15px;'>Please select a question first and try again.</span><br /><br />";
            }
            else
            {
                qstring = Page.Request["q"].ToString();
                qId = Convert.ToInt32(qstring);

                //strore the quiz id in hidden field
                questionfield.Value = qstring;

                bindquestion();
            }
        }
        else
        {
            if (int.TryParse(questionfield.Value, out tempval) == true)
            {
                qId = tempval;
            }
            else
            {
                editquestiondiv.InnerHtml = "<br /><span style='color:#FF0000; font-size:15px;'>Please select a question first and try again.</span><br /><br />";
            }
        }

        //add javascript event
        singleoptionsubmit.Attributes.Add("onclick", "javascript: if ( Page_ClientValidate() ){" + singleoptionsubmit.ClientID + ".disabled=true; " + singleoptionsubmit.ClientID + ".value='Wait...';}" + ClientScript.GetPostBackEventReference(singleoptionsubmit, ""));
        textoptionsubmit.Attributes.Add("onclick", "javascript: if ( Page_ClientValidate() ){" + textoptionsubmit.ClientID + ".disabled=true; " + textoptionsubmit.ClientID + ".value='Wait...';}" + ClientScript.GetPostBackEventReference(textoptionsubmit, ""));
    }

    protected void bindquestion()
    {
        SqlDataReader dreader;
        SqlCommand getquestioncmd = new SqlCommand("select id, quizid, type, title from " + quizquestionstable + " where id=@questionid");
        getquestioncmd.Parameters.AddWithValue("questionid", qId);

        db getquestion = new db();
        dreader = getquestion.returnDataReader(getquestioncmd);

        if (!dreader.HasRows)
        {
            editquestiondiv.InnerHtml = "<br /><span style='color:#FF0000; font-size:15px;'>Sorry! we could not complete your request at this time. please try again later.</span><br />";
        }
        else
        {
            while (dreader.Read())
            {
                //set the home link
                string quizidstr = dreader["quizid"].ToString();
                HyperLink homelink = (HyperLink)Master.FindControl("homelnk");
                if (homelink != null)
                {
                    homelink.NavigateUrl = "setquestions?q=" + quizidstr;
                }

                //detect question type and set the template
                qtype = dreader["type"].ToString();

                if (qtype == "single")
                {
                    singleoptiondiv.Visible = true;
                    textoptiondiv.Visible = false;

                    questionstr = dreader["title"].ToString();
                    txtsingleoption.Text = questionstr;

                    populateoptions(qId);
                }
                else if (qtype == "text")
                {
                    singleoptiondiv.Visible = false;
                    textoptiondiv.Visible = true;

                    questionstr = dreader["title"].ToString();

                    txttextoption.Text = questionstr;
                    txttextoptionanswer.Text = textoptionstr;
                }
                else
                {
                    //do nothing for time being
                }
            }
        }
    }

    protected void populateoptions(int QID)
    {
        string answeridstr = "";
        List<string[]> allRows = new List<string[]>();
        SqlDataReader dReader;
        SqlCommand getanswerscmd = new SqlCommand("select a.id, a.questionoption, b.optionid from " + quizquestionoptionstable + " as a inner join " + quizquestionanswertable + " as b on a.questionid=b.questionid where a.questionid=@questionid");
        getanswerscmd.Parameters.AddWithValue("questionid", QID);

        db getanswerslist = new db();
        dReader = getanswerslist.returnDataReader(getanswerscmd);

        if (dReader.HasRows)
        {
            while (dReader.Read())
            {
                allRows.Add(new string[] { dReader["id"].ToString(), dReader["questionoption"].ToString() });
                answeridstr = dReader["optionid"].ToString();
            }

            //populate the answer dropdown
            lblanswer.Visible = true;
            ddlsingleanswer.DataSource = from obj in allRows
                                         select new
                                         {
                                             id = obj[0],
                                             questionoption = obj[1]
                                         };
            ddlsingleanswer.DataBind();
            ddlsingleanswer.Items.FindByValue(answeridstr).Selected = true;

            //populate the options text boxes
            int itemcount = 0;
            string optionstr = "";
            string optionidstr = "";
            foreach (string[] arr in allRows)
            {
                itemcount++;
                optionidstr = arr[0].ToString();
                optionstr = arr[1].ToString();

                if (itemcount == 1)
                {
                    txtsingleoption1.Text = optionstr;
                    hfoption1.Value = optionidstr;
                }
                else if (itemcount == 2)
                {
                    txtsingleoption2.Text = optionstr;
                    hfoption2.Value = optionidstr;
                }
                else if (itemcount == 3)
                {
                    txtsingleoption3.Text = optionstr;
                    hfoption3.Value = optionidstr;
                }
                else if (itemcount == 4)
                {
                    txtsingleoption4.Text = optionstr;
                    hfoption4.Value = optionidstr;
                }
            }
        }
        else
        {
            txtsingleoption1.Text = null;
            txtsingleoption2.Text = null;
            txtsingleoption3.Text = null;
            txtsingleoption4.Text = null;

            lblanswer.Visible = false;
            ddlsingleanswer.Visible = false;
        }
    }

    protected void singleoptionsubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            questionstr = txtsingleoption.Text.Trim();
            answerstr = ddlsingleanswer.SelectedItem.Value;

            //update question
            SqlCommand updatequestioncmd = new SqlCommand("update " + quizquestionstable + " set title=@title, lastupdated=@lastupdated where id=@questionid");
            updatequestioncmd.Parameters.AddWithValue("questionid", qId);
            updatequestioncmd.Parameters.AddWithValue("title", questionstr);
            updatequestioncmd.Parameters.AddWithValue("lastupdated", updatedate);

            db updatequestion = new db();
            updatequestion.ExecuteQuery(updatequestioncmd);

            //update answer
            SqlCommand updateanswercmd = new SqlCommand("update " + quizquestionanswertable + " set optionid=@optionid, lastupdated=@lastupdated where questionid=@questionid");
            updateanswercmd.Parameters.AddWithValue("questionid", qId);
            updateanswercmd.Parameters.AddWithValue("optionid", answerstr);
            updateanswercmd.Parameters.AddWithValue("lastupdated", updatedate);

            db updateanswer = new db();
            updateanswer.ExecuteQuery(updateanswercmd);

            lblmessage.Visible = true;
            lblmessage.Text = "Question updated successfully!";

            bindquestion();
        }
        else
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Please enter the compulsory fields!";
        }
    }

    protected void textoptionsubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            questionstr = txttextoption.Text.Trim();
            answerstr = txttextoptionanswer.Text.Trim();

            SqlCommand updatequestioncmd = new SqlCommand("update " + quizquestionstable + " set title=@title, textoption=@textoption, answer=@answer, lastupdated=@lastupdated where id=@questionid");
            updatequestioncmd.Parameters.AddWithValue("questionid", qId);
            updatequestioncmd.Parameters.AddWithValue("title", questionstr);
            updatequestioncmd.Parameters.AddWithValue("textoption", answerstr);
            updatequestioncmd.Parameters.AddWithValue("answer", answerstr);
            updatequestioncmd.Parameters.AddWithValue("lastupdated", updatedate);

            db updatequestion = new db();
            updatequestion.ExecuteQuery(updatequestioncmd);

            lblmessage.Visible = true;
            lblmessage.Text = "Question added successfully!";

            bindquestion();
        }
        else
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Please enter the compulsory fields!";
        }
    }

    //when answers updated
    protected void txtsingleoption_TextChanged(object sender, EventArgs e)
    {
        int optionId = 0;
        Page.Validate();
        if (Page.IsValid)
        {
            string reqcontrol = getPostBackControlName();

            if (reqcontrol == "txtsingleoption1")
            {
                optionId = Convert.ToInt32(hfoption1.Value);
                answerstr = txtsingleoption1.Text.Trim();
            }
            else if (reqcontrol == "txtsingleoption2")
            {
                optionId = Convert.ToInt32(hfoption2.Value);
                answerstr = txtsingleoption2.Text.Trim();
            }
            else if (reqcontrol == "txtsingleoption3")
            {
                optionId = Convert.ToInt32(hfoption3.Value);
                answerstr = txtsingleoption3.Text.Trim();
            }
            else if (reqcontrol == "txtsingleoption4")
            {
                optionId = Convert.ToInt32(hfoption4.Value);
                answerstr = txtsingleoption4.Text.Trim();
            }

            if (String.IsNullOrEmpty(reqcontrol) == false && optionId > 0)
            {
                SqlCommand updateoptioncmd = new SqlCommand("update " + quizquestionoptionstable + " set questionoption=@questionoption, lastupdated=@lastupdated where id=@optionid and questionid=@questionid");
                updateoptioncmd.Parameters.AddWithValue("optionid", optionId);
                updateoptioncmd.Parameters.AddWithValue("questionid", qId);
                updateoptioncmd.Parameters.AddWithValue("questionoption", answerstr);
                updateoptioncmd.Parameters.AddWithValue("lastupdated", updatedate);

                db updatequestion = new db();
                updatequestion.ExecuteQuery(updateoptioncmd);

                lblmessage.Visible = true;
                lblmessage.Text = "Option updated successfully!";

                bindquestion();
            }
            else
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Sorry! we could not complete your request at this time. please try again later.";
            }
        }
        else
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Please enter the compulsory fields!";
        }
    }

    //get the control that cause postback
    private string getPostBackControlName()
    {
        string controlid = "";
        Control control = null;
        //first we will check the "__EVENTTARGET" because if post back made by the controls
        //which used "_doPostBack" function also available in Request.Form collection.

        string ctrlname = Page.Request.Params["__EVENTTARGET"];
        if (ctrlname != null && ctrlname != String.Empty)
        {
            control = Page.FindControl(ctrlname);
        }

        // if __EVENTTARGET is null, the control is a button type and we need to
        // iterate over the form collection to find it
        else
        {
            string ctrlStr = String.Empty;
            Control c = null;
            foreach (string ctl in Page.Request.Form)
            {
                //handle ImageButton they having an additional "quasi-property" in their Id which identifies
                //mouse x and y coordinates
                if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                {
                    ctrlStr = ctl.Substring(0, ctl.Length - 2);
                    c = Page.FindControl(ctrlStr);
                }
                else
                {
                    c = Page.FindControl(ctl);
                }
                if (c is System.Web.UI.WebControls.Button ||
                         c is System.Web.UI.WebControls.ImageButton)
                {
                    control = c;
                    break;
                }
            }
        }
        if (control == null)
        {
            controlid = "pageload";
        }
        else
        {
            controlid = control.ID.ToString();
        }
        return controlid;
    }
}