using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_addquestion : System.Web.UI.Page
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
                addquestiondiv.InnerHtml = "<br /><span style='color:#FF0000; font-size:15px;'>Please select a quiz event first and try again.</span>";
            }
            else
            {
                qstring = Page.Request["q"].ToString();
                quizId = Convert.ToInt32(qstring);

                //strore the quiz id in hidden field
                quizfield.Value = qstring;
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
                addquestiondiv.InnerHtml = "<br /><span style='color:#FF0000; font-size:15px;'>Please select a quiz event first and try again.</span>";
            }
        }

        //set the home link
        HyperLink homelink = (HyperLink)Master.FindControl("homelnk");
        if (homelink != null)
        {
            homelink.NavigateUrl = "setquestions?q=" + quizId;
        }

        //add javascript event
        singlequestionsubmit.Attributes.Add("onclick", "javascript: if ( Page_ClientValidate() ){" + singlequestionsubmit.ClientID + ".disabled=true; " + singlequestionsubmit.ClientID + ".value='Wait...';}" + ClientScript.GetPostBackEventReference(singlequestionsubmit, ""));
        singleanswersubmit.Attributes.Add("onclick", "javascript: if ( Page_ClientValidate() ){" + singleanswersubmit.ClientID + ".disabled=true; " + singleanswersubmit.ClientID + ".value='Wait...';}" + ClientScript.GetPostBackEventReference(singleanswersubmit, ""));
        textoptionsubmit.Attributes.Add("onclick", "javascript: if ( Page_ClientValidate() ){" + textoptionsubmit.ClientID + ".disabled=true; " + textoptionsubmit.ClientID + ".value='Wait...';}" + ClientScript.GetPostBackEventReference(textoptionsubmit, ""));
    }

    //load template based on question type
    protected void questiontypechanged(object sender, EventArgs e)
    {
        qtype = ddltype.SelectedItem.Text.ToLower();

        if (qtype == "single")
        {
            lblmessage.Visible = false;
            singleoptiondiv.Visible = true;
            textoptiondiv.Visible = false;
        }
        else if (qtype == "text")
        {
            lblmessage.Visible = false;
            singleoptiondiv.Visible = false;
            textoptiondiv.Visible = true;
        }
        else
        {
            singleoptiondiv.Visible = false;
            textoptiondiv.Visible = false;
            lblmessage.Visible = true;
            lblmessage.Text = "Please select a question type!";
        }
    }

    //single answer question
    protected void singlequestionsubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (quizId > 0)
            {
                qorder = getquestionorder();
                qtype = ddltype.SelectedItem.Text.ToLower();

                string singleoptionquestion = txtsingleoption.Text.Trim();
                string option1str = txtsingleoption1.Text.Trim();
                string option2str = txtsingleoption2.Text.Trim();
                string option3str = txtsingleoption3.Text.Trim();
                string option4str = txtsingleoption4.Text.Trim();

                string singleoptionanswer = ddlsingleanswer.SelectedItem.Value;

                //insert the question
                SqlCommand insertnewquestioncmd = new SqlCommand("insert into " + quizquestionstable + " (quizid, questionorder, title, type, lastupdated) values (@quizid, @questionorder, @title, @type, @lastupdated);SELECT CAST(scope_identity() AS int)");
                insertnewquestioncmd.Parameters.AddWithValue("quizid", quizId);
                insertnewquestioncmd.Parameters.AddWithValue("questionorder", qorder);
                insertnewquestioncmd.Parameters.AddWithValue("title", singleoptionquestion);
                insertnewquestioncmd.Parameters.AddWithValue("type", qtype);
                insertnewquestioncmd.Parameters.AddWithValue("lastupdated", updatedate);

                db insertnewquestion = new db();
                questid = insertnewquestion.ReturnIDonExecuteQuery(insertnewquestioncmd);

                //if question created successfully
                if (questid > 0)
                {
                    //store the value in hidden field
                    questionfield.Value = questid.ToString();

                    //insert options
                    if (String.IsNullOrEmpty(option1str) == true && String.IsNullOrEmpty(option2str) == true && String.IsNullOrEmpty(option3str) == true && String.IsNullOrEmpty(option4str) == true)
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "Please enter the answers";
                    }
                    else
                    {
                        //insert option1
                        SqlCommand insertquestionoption1cmd = new SqlCommand("insert into " + quizquestionoptionstable + " (questionid, questionoption, lastupdated) values (@questionid, @questionoption, @lastupdated)");
                        insertquestionoption1cmd.Parameters.AddWithValue("questionid", questid);
                        insertquestionoption1cmd.Parameters.AddWithValue("questionoption", option1str);
                        insertquestionoption1cmd.Parameters.AddWithValue("lastupdated", updatedate);

                        db insertoption1 = new db();
                        insertoption1.ExecuteQuery(insertquestionoption1cmd);

                        //insert option2
                        SqlCommand insertquestionoption2cmd = new SqlCommand("insert into " + quizquestionoptionstable + " (questionid, questionoption, lastupdated) values (@questionid, @questionoption, @lastupdated)");
                        insertquestionoption2cmd.Parameters.AddWithValue("questionid", questid);
                        insertquestionoption2cmd.Parameters.AddWithValue("questionoption", option2str);
                        insertquestionoption2cmd.Parameters.AddWithValue("lastupdated", updatedate);

                        db insertoption2 = new db();
                        insertoption2.ExecuteQuery(insertquestionoption2cmd);

                        //insert option3
                        if (String.IsNullOrEmpty(option3str) == false)
                        {
                            SqlCommand insertquestionoption3cmd = new SqlCommand("insert into " + quizquestionoptionstable + " (questionid, questionoption, lastupdated) values (@questionid, @questionoption, @lastupdated)");
                            insertquestionoption3cmd.Parameters.AddWithValue("questionid", questid);
                            insertquestionoption3cmd.Parameters.AddWithValue("questionoption", option3str);
                            insertquestionoption3cmd.Parameters.AddWithValue("lastupdated", updatedate);

                            db insertoption3 = new db();
                            insertoption3.ExecuteQuery(insertquestionoption3cmd);
                        }

                        //insert option4
                        if (String.IsNullOrEmpty(option4str) == false)
                        {
                            SqlCommand insertquestionoption4cmd = new SqlCommand("insert into " + quizquestionoptionstable + " (questionid, questionoption, lastupdated) values (@questionid, @questionoption, @lastupdated)");
                            insertquestionoption4cmd.Parameters.AddWithValue("questionid", questid);
                            insertquestionoption4cmd.Parameters.AddWithValue("questionoption", option4str);
                            insertquestionoption4cmd.Parameters.AddWithValue("lastupdated", updatedate);

                            db insertoption4 = new db();
                            insertoption4.ExecuteQuery(insertquestionoption4cmd);
                        }
                    }

                    //insert answer
                    ddltype.Visible = false;
                    txtsingleoption.ReadOnly = true;
                    txtsingleoption1.ReadOnly = true;
                    txtsingleoption2.ReadOnly = true;
                    txtsingleoption3.ReadOnly = true;
                    txtsingleoption4.ReadOnly = true;
                    txtsingleoption.BackColor = System.Drawing.Color.LightBlue;
                    txtsingleoption1.BackColor = System.Drawing.Color.LightBlue;
                    txtsingleoption2.BackColor = System.Drawing.Color.LightBlue;
                    txtsingleoption3.BackColor = System.Drawing.Color.LightBlue;
                    txtsingleoption4.BackColor = System.Drawing.Color.LightBlue;
                    singlequestionsubmit.Visible = false;
                    singleanswersubmit.Visible = true;
                    lblanswer.Visible = true;
                    ddlsingleanswer.Visible = true;

                    //get the available options
                    populateanswers(questid);
                }
                else
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "Sorry! we could not process your request. Please try again";
                }
            }
            else
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Sorry! we could not process your request. Please try again";
            }
        }
        else
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Please enter the compulsory fields!";
        }
    }

    //single answer question
    protected void singleanswersubmit_Click(object sender, EventArgs e)
    {
        int answerId = Convert.ToInt32(ddlsingleanswer.SelectedItem.Value);
        int questionId = Convert.ToInt32(questionfield.Value);
        int insertid = 0;

        SqlCommand insertquestionanswercmd = new SqlCommand("insert into " + quizquestionanswertable + " (questionid, optionid, lastupdated) values (@questionid, @optionid, @lastupdated);SELECT CAST(scope_identity() AS int)");
        insertquestionanswercmd.Parameters.AddWithValue("questionid", questionId);
        insertquestionanswercmd.Parameters.AddWithValue("optionid", answerId);
        insertquestionanswercmd.Parameters.AddWithValue("lastupdated", updatedate);

        db insertanswer = new db();
        insertid = insertanswer.ReturnIDonExecuteQuery(insertquestionanswercmd);

        //insert successful
        if (insertid > 0)
        {
            txtsingleoption.Text = null;
            txtsingleoption1.Text = null;
            txtsingleoption2.Text = null;
            txtsingleoption3.Text = null;
            txtsingleoption4.Text = null;
            txtsingleoption.ReadOnly = false;
            txtsingleoption1.ReadOnly = false;
            txtsingleoption2.ReadOnly = false;
            txtsingleoption3.ReadOnly = false;
            txtsingleoption4.ReadOnly = false;
            txtsingleoption.BackColor = System.Drawing.Color.White;
            txtsingleoption1.BackColor = System.Drawing.Color.White;
            txtsingleoption2.BackColor = System.Drawing.Color.White;
            txtsingleoption3.BackColor = System.Drawing.Color.White;
            txtsingleoption4.BackColor = System.Drawing.Color.White;
            lblanswer.Visible = false;
            ddlsingleanswer.Visible = false;
            singleanswersubmit.Visible = false;
            singlequestionsubmit.Visible = true;

            lblmessage.Visible = true;
            lblmessage.Text = "Question added successfully!";

            ddltype.Visible = true;
            singleoptiondiv.Visible = false;
            textoptiondiv.Visible = false;
        }

    }

    //populate single question options
    protected void populateanswers(int qId)
    {
        DataTable qTable = new DataTable();
        SqlCommand getanswers = new SqlCommand("select id, questionoption from " + quizquestionoptionstable + " where questionid=@questionid");
        getanswers.Parameters.AddWithValue("questionid", qId);

        db getanswerslist = new db();
        qTable = getanswerslist.returnDataTable(getanswers);

        if (qTable.Rows.Count > 0)
        {
            ddlsingleanswer.DataSource = qTable;
            ddlsingleanswer.DataTextField = "questionoption";
            ddlsingleanswer.DataValueField = "id";
            ddlsingleanswer.DataBind();
        }
        else
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Nothing available at the moment";
        }
    }

    //get the order
    protected int getquestionorder()
    {
        int temporder = 0;
        SqlDataReader dreader;
        SqlCommand findorder = new SqlCommand("select Top 1* from " + quizquestionstable + " where quizid=@quizid order by 'questionorder' Desc");
        findorder.Parameters.AddWithValue("quizid", quizId);

        db getorder = new db();
        dreader = getorder.returnDataReader(findorder);

        if (!dreader.HasRows)
        {
            temporder = 1;
        }
        else
        {
            while (dreader.Read())
            {
                string temporderstr = dreader["questionorder"].ToString();
                int itempos = Convert.ToInt32(temporderstr);
                temporder = itempos + 1;
            }
        }
        return temporder;
    }

    protected void textoptionsubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            qorder = getquestionorder();
            qtype = ddltype.SelectedItem.Text.ToLower();

            string textoptionquestion = txttextoption.Text.Trim();
            string textoptionanswer = txttextoptionanswer.Text.Trim();

            SqlCommand insertnew = new SqlCommand("insert into " + quizquestionstable + " (quizid, questionorder, type, title, textoption, answer, lastupdated) values (@quizid, @questionorder, @type, @title, @textoption, @answer, @lastupdated)");
            insertnew.Parameters.AddWithValue("quizid", quizId);
            insertnew.Parameters.AddWithValue("questionorder", qorder);
            insertnew.Parameters.AddWithValue("type", qtype);
            insertnew.Parameters.AddWithValue("title", textoptionquestion);
            insertnew.Parameters.AddWithValue("textoption", textoptionanswer);
            insertnew.Parameters.AddWithValue("answer", textoptionanswer);
            insertnew.Parameters.AddWithValue("lastupdated", updatedate);

            db insertnewquestion = new db();
            insertnewquestion.ExecuteQuery(insertnew);

            txttextoption.Text = null;
            txttextoptionanswer.Text = null;

            lblmessage.Visible = true;
            lblmessage.Text = "Question added successfully!";
        }
        else
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Please enter the compulsory fields!";
        }
    }
}