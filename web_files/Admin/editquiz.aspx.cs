using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class Admin_editquiz : System.Web.UI.Page
{
    string quizdetailstable = "quizdetails";
    DateTime updatedate = new DateTime();
    string qstring = "0";
    int quizId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        updatedate = DateTime.Now;
        if (!Page.IsPostBack)
        {
            //get the query string value
            if (Page.Request["q"] == null)
            {
                editquiz.Visible = false;
                lblalert.Visible = false;
                lblalert.Text = "<br />Sorry! we could not process your request at this time. please try again later.";
            }
            else
            {
                editquiz.Visible = true;
                qstring = Page.Request["q"].ToString();
                quizId = Convert.ToInt32(qstring);

                //strore the quiz id in hidden field
                quizfield.Value = qstring;

                bindquizdetails();
            }
        }
        else
        {
            quizId = Convert.ToInt32(quizfield.Value);
        }

        //add javascript event
        submitquiz.Attributes.Add("onclick", "javascript: if ( Page_ClientValidate() ){" + submitquiz.ClientID + ".disabled=true; " + submitquiz.ClientID + ".value='Wait...';}" + ClientScript.GetPostBackEventReference(submitquiz, ""));
    }

    //bind quiz details
    protected void bindquizdetails()
    {
        SqlDataReader dReader;
        SqlCommand getquizcmd = new SqlCommand("select id, name, description, completiondescription, startdate, enddate, termsandconditions from " + quizdetailstable + " where id=@quizid");
        getquizcmd.Parameters.AddWithValue("quizid", quizId);

        db getquizinfo = new db();
        dReader = getquizinfo.returnDataReader(getquizcmd);

        if (!dReader.HasRows)
        {
            editquiz.Visible = false;
            lblalert.Visible = true;
            lblalert.Text = "<br />Sorry! we could not process your request at this time. please try again later.";
        }
        else
        {
            while (dReader.Read())
            {
                editquiz.Visible = true;
                lblalert.Visible = false;
                string name = dReader["name"].ToString();
                string description = dReader["description"].ToString();
                string completiondescription = dReader["completiondescription"].ToString();
                string tandcstr = dReader["termsandconditions"].ToString();
                string startdatestr = dReader["startdate"].ToString();
                string enddatestr = dReader["enddate"].ToString();

                txtname.Text = name;
                txtdescription.Text = description;
                txtcompletiondescription.Text = completiondescription;
                txtterms.Text = tandcstr;
                txtstartdate.Text = startdatestr.Substring(0, 10);
                txtenddate.Text = enddatestr.Substring(0, 10);
            }
        }
    }

    //edit quiz
    protected void submitquiz_click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string name = txtname.Text.Trim();
            string description = txtdescription.Text.Trim();
            string completiondesc = txtcompletiondescription.Text.Trim();
            string start = txtstartdate.Text;
            string end = txtenddate.Text;
            DateTime startdate = new DateTime();
            startdate = Convert.ToDateTime(start);
            DateTime enddate = new DateTime();
            enddate = Convert.ToDateTime(end);
            enddate = enddate.AddHours(23).AddMinutes(59).AddSeconds(59);
            string quizterms = txtterms.Text;

            if (startdate > enddate)
            {
                lblalert.Visible = true;
                lblalert.Text = "Please check Start date and End date!";
            }
            else
            {
                SqlCommand updatequizcmd = new SqlCommand("update " + quizdetailstable + " set name=@name, description=@description, completiondescription=@completiondescription, startdate=@startdate, enddate=@enddate, termsandconditions=@termsandconditions, lastupdated=@lastupdated where id=@quizid");
                updatequizcmd.Parameters.AddWithValue("quizid", quizId);
                updatequizcmd.Parameters.AddWithValue("name", name);
                updatequizcmd.Parameters.AddWithValue("description", description);
                updatequizcmd.Parameters.AddWithValue("completiondescription", completiondesc);
                updatequizcmd.Parameters.AddWithValue("startdate", startdate);
                updatequizcmd.Parameters.AddWithValue("enddate", enddate);
                updatequizcmd.Parameters.AddWithValue("termsandconditions", quizterms);
                updatequizcmd.Parameters.AddWithValue("lastupdated", updatedate);

                db updatequiz = new db();
                updatequiz.ExecuteQuery(updatequizcmd);

                Response.Redirect("viewquiz", false);
            }
        }
        else
        {
            lblalert.Visible = true;
            lblalert.Text = "Please enter the complusory fields and try again!";
        }
    }
}