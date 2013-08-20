using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

public partial class Admin_viewresponses : System.Web.UI.Page
{
    string quizdetailstable = "quizdetails";
    string quizquestionstable = "quiz_questions";
    string quizquestionoptionstable = "question_options";
    string quizquestionanswertable = "question_answer";
    string quizresponsestable = "quiz_responses";
    string quizuserreponsetable = "question_responses";
    DateTime updatedate = new DateTime();
    string qstring = "";
    int quizId = 0;
    int tempval = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        updatedate = DateTime.Now;

        if (!Page.IsPostBack)
        {
            //get the query string value
            if (Page.Request["q"] == null)
            {
                responsesdiv.InnerHtml = "<br /><span style='color:#FF0000; font-size:15px;'>Please select a quiz event first and try again.</span>";
            }
            else
            {
                qstring = Page.Request["q"].ToString();
                quizId = Convert.ToInt32(qstring);

                //strore the quiz id in hidden field
                quizfield.Value = qstring;
                bindentries();
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
                responsesdiv.InnerHtml = "<br /><span style='color:#FF0000; font-size:15px;'>Please select a quiz event first and try again.</span>";
            }
        }
    }

    protected void bindentries()
    {
        DataTable dTable = new DataTable();
        SqlCommand getresponses = new SqlCommand("select id, email, name, correctanswers, wronganswers from " + quizresponsestable + " where quizid=@quizid");
        getresponses.Parameters.AddWithValue("quizid", quizId);

        db getresponseslist = new db();
        dTable = getresponseslist.returnDataTable(getresponses);

        if (dTable.Rows.Count > 0)
        {
            responsesdiv.Visible = true;
            responsesrpt.Visible = true;
            exportdiv.Visible = true;
            lblmessage.Visible = false;

            responsesrpt.DataSource = dTable;
            responsesrpt.DataBind();

            lblmessage.Visible = true;
            lblmessage.Text = "Total Entries: " + dTable.Rows.Count.ToString() + "<br />";
        }
        else
        {
            responsesdiv.Visible = false;
            responsesrpt.Visible = false;
            exportdiv.Visible = false;
            lblmessage.Visible = true;
            lblmessage.Text = "Nothing available at the moment" + "<br /><br />";
        }
    }

    //file exports
    protected void exportbutton_click(object sender, EventArgs e)
    {
        //get the selected file type
        if (fileexporttype.SelectedValue == "Excel")
        {
            createexcelfile();
        }
        else if (fileexporttype.SelectedValue == "Word")
        {
            createwordfile();
        }
        else if (fileexporttype.SelectedValue == "PDF")
        {
            createpdffile();
        }
    }

    //create excel file
    protected void createexcelfile()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=reponses.xls");
        Response.Charset = "";

        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        responsesrpt.RenderControl(hw);        
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    //create word file
    protected void createwordfile()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=reponses.doc");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-word";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        responsesrpt.RenderControl(hw);        
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    //create pdf file
    protected void createpdffile()
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=reponses.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        responsesrpt.RenderControl(hw);        
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
}