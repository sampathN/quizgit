using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public partial class _Default : Page
{
    string quizdetailstable = "quizdetails";
    string quizquestionstable = "quiz_questions";
    string quizresponsestable = "quiz_responses";
    string quizquestionoptionstable = "question_options";
    string quizquestionanswertable = "question_answer";
    string quizuserreponsetable = "question_responses";
    string quizname = "";
    string description = "";
    DateTime updatedate = new DateTime();
    DateTime start = new DateTime();
    DateTime end = new DateTime();
    string terms = "";
    string lbr = "<br /><br />";
    int quizId = 0;
    int tempval = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        updatedate = DateTime.Now;

        if (!Page.IsPostBack)
        {
            //show quiz details
            Bindquizes();

            //show questions
            BindQuestions();
        }
        else
        {
            if (int.TryParse(quizfield.Value, out tempval) == true)
            {
                quizId = tempval;
            }
        }

        //add javascript event
        this.Form.DefaultButton = this.btnsubmit.UniqueID;
        btnsubmit.Attributes.Add("onclick", "javascript: if ( Page_ClientValidate() ){" + btnsubmit.ClientID + ".disabled=true; " + btnsubmit.ClientID + ".value='Wait...';}" + ClientScript.GetPostBackEventReference(btnsubmit, ""));
    }

    //get the recent quizes
    protected void Bindquizes()
    {
        SqlDataReader dReader;
        SqlCommand getquizescmd = new SqlCommand("select id, name, description, startdate, enddate, termsandconditions from " + quizdetailstable);

        db getquizeslist = new db();
        dReader = getquizeslist.returnDataReader(getquizescmd);

        if (!dReader.HasRows)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Nothing available at the moment" + "<br /><br />";
        }
        else
        {
            while (dReader.Read())
            {
                //get and store quizid
                quizfield.Value = dReader["id"].ToString();
                quizId = Convert.ToInt32(quizfield.Value);

                //quiz details
                quizname = dReader["name"].ToString();
                description = dReader["description"].ToString();
                start = Convert.ToDateTime(dReader["startdate"].ToString());
                end = Convert.ToDateTime(dReader["enddate"].ToString());
                terms = dReader["termsandconditions"].ToString();

                //show quiz if start/end date is valid
                if ((updatedate < start) && (updatedate > end))
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "Sorry! We could not process your request. please try later.";
                }
                else
                {
                    lblquizname.Text = quizname;
                    lbldescription.Text = description;                    
                }
            }
        }
    }

    //get questions
    protected void BindQuestions()
    {
        DataTable dTable = new DataTable();
        SqlCommand getquestions = new SqlCommand("select id, title from " + quizquestionstable + " where quizid=@quizid order by questionorder ASC");
        getquestions.Parameters.AddWithValue("quizid", quizId);

        db getquestionslist = new db();
        dTable = getquestionslist.returnDataTable(getquestions);

        if (dTable.Rows.Count > 0)
        {
            questionsrpt.DataSource = dTable;
            questionsrpt.DataBind();
        }
        else
        {
            questionsdiv.InnerHtml = "<span style='color:#FF0000; font-size:15px;'>Nothing available at the moment</span>";
        }
    }

    //quiz answers submitted
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SqlDataReader dReader;
        string email = "";
        string name = "";
        string selectedanswer = "";
        string correctanswer = "";
        int questionId = 0;
        int questionscount = 0;
        int correctcount = 0;
        int wrongcount = 0;
        int success = 0;
        ArrayList answersList = new ArrayList();

        Page.Validate();
        if (Page.IsValid)
        {
            email = txtemail.Text.Trim();
            name = txtname.Text.Trim();

            //check if this account has already taken the quiz.
            DataTable accounts = new DataTable();
            SqlCommand checkentrycmd = new SqlCommand("select * from " + quizresponsestable + " where quizid=@quizid and email=@email");
            checkentrycmd.Parameters.AddWithValue("quizid", quizId);
            checkentrycmd.Parameters.AddWithValue("email", email);

            db checkentry = new db();
            accounts = checkentry.returnDataTable(checkentrycmd);

            if (accounts.Rows.Count > 0)
            {
                quizdetails.Visible = false;
                detailsdiv.Visible = false;
                questionsdiv.Visible = false;
                lblmessage.Visible = true;
                lblmessage.Text = "You have already taken this quiz!";
            }
            else
            {
                foreach (RepeaterItem item in questionsrpt.Items)
                {
                    // Checking the item is a data item
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        //get the questionid
                        var hfId = item.FindControl("hfID") as HiddenField;
                        questionId = Convert.ToInt32(hfId.Value);

                        //get the submitted answer
                        var rdbList = item.FindControl("rbloptions") as RadioButtonList;
                        selectedanswer = rdbList.SelectedValue;
                        //disable to prevent submitting again
                        rdbList.Enabled = false;

                        //get the correct answer
                        SqlCommand getanswercmd = new SqlCommand("select optionid from " + quizquestionanswertable + " where questionid=@questionid");
                        getanswercmd.Parameters.AddWithValue("questionid", questionId);

                        db getanswer = new db();
                        dReader = getanswer.returnDataReader(getanswercmd);

                        if (!dReader.HasRows)
                        {
                            //can't find this question/answer
                        }
                        else
                        {
                            while (dReader.Read())
                            {
                                correctanswer = dReader["optionid"].ToString();
                            }
                        }

                        //compare both answers
                        if (selectedanswer == correctanswer)
                        {
                            correctcount++;
                            rdbList.SelectedItem.Attributes.Add("style", "color: #006400");
                        }
                        else
                        {
                            wrongcount++;
                            rdbList.SelectedItem.Attributes.Add("style", "color: #ff0000");
                            rdbList.Items.FindByValue(correctanswer).Attributes.Add("style", "color: #006400");
                        }

                        //add the submitted answer to list
                        answersList.Add(new string[] { questionId.ToString(), selectedanswer });
                    }
                }

                //create entry
                SqlCommand insertentrycmd = new SqlCommand("insert into " + quizresponsestable + " (quizid, email, name, correctanswers, wronganswers, lastupdated) values (@quizid, @email, @name, @correctanswers, @wronganswers, @lastupdated);SELECT CAST(scope_identity() AS int)");
                insertentrycmd.Parameters.AddWithValue("quizid", quizId);
                insertentrycmd.Parameters.AddWithValue("email", email);
                insertentrycmd.Parameters.AddWithValue("name", name);
                insertentrycmd.Parameters.AddWithValue("correctanswers", correctcount);
                insertentrycmd.Parameters.AddWithValue("wronganswers", wrongcount);
                insertentrycmd.Parameters.AddWithValue("lastupdated", updatedate);

                db insertentry = new db();
                success = insertentry.ReturnIDonExecuteQuery(insertentrycmd);

                //display the result on screen
                if (success > 0)
                {
                    foreach (string[] arr in answersList)
                    {
                        SqlCommand insertresponsecmd = new SqlCommand("insert into " + quizuserreponsetable + " (responseid, questionid, optionid, lastupdated) values (@responseid, @questionid, @optionid, @lastupdated)");
                        insertresponsecmd.Parameters.Clear();
                        insertresponsecmd.Parameters.AddWithValue("responseid", success);
                        insertresponsecmd.Parameters.AddWithValue("questionid", arr[0].ToString());
                        insertresponsecmd.Parameters.AddWithValue("optionid", arr[1].ToString());
                        insertresponsecmd.Parameters.AddWithValue("lastupdated", updatedate);

                        db insertresponses = new db();
                        insertresponses.ExecuteQuery(insertresponsecmd);
                    }

                    detailsdiv.Visible = false;
                    questionscount = correctcount + wrongcount;
                    lblalert.Visible = true;
                    
                    //get the completion description from database table
                    SqlDataReader Treader;
                    SqlCommand getcompletiondesc = new SqlCommand("select completiondescription from " + quizdetailstable + " where id=@quizid");
                    getcompletiondesc.Parameters.AddWithValue("quizid", quizId);

                    db getdesc = new db();
                    Treader = getdesc.returnDataReader(getcompletiondesc);

                    if (!Treader.HasRows)
                    {
                        lblalert.Text = "Thanks for taking the Quiz." + "<br />" + "You have answered <span style='color:#006400;'>" + correctcount + "</span> questions correctly out of " + questionscount + "<br />";
                    }
                    else
                    {
                        while (Treader.Read())
                        {
                            string completiondesc = Treader["completiondescription"].ToString();
                            lblalert.Text = completiondesc + "<br />" + "You have answered <span style='color:#006400;'>" + correctcount + "</span> questions correctly out of " + questionscount + "<br />";
                        }
                    }

                    
                    btnsubmit.Visible = false;
                }
                else
                {
                    lblalert.Visible = true;
                    lblalert.Text = "Sorry! we could not process your request. Please try again.";
                }
            }
        }
        else
        {
            lblalert.Visible = true;
            lblalert.Text = "Please answer all the questions!";
        }
    }

    //populate the options for each question
    protected void questionsrpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int qid = 0;

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //get the questionid
            HiddenField hfl = (HiddenField)e.Item.FindControl("hfID");
            qid = Convert.ToInt32(hfl.Value);

            //get the options for this questionid
            RadioButtonList rbl = (RadioButtonList)e.Item.FindControl("rbloptions");
            DataTable qTable = new DataTable();
            SqlCommand getoptions = new SqlCommand("select id, questionoption from " + quizquestionoptionstable + " where questionid=@questionid");
            getoptions.Parameters.AddWithValue("questionid", qid);

            db getoptionslist = new db();
            qTable = getoptionslist.returnDataTable(getoptions);

            if (qTable.Rows.Count > 0)
            {
                rbl.DataSource = qTable;
                rbl.DataTextField = "questionoption";
                rbl.DataValueField = "id";
                rbl.DataBind();
            }
        }
    }
}