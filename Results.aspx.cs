using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Results : System.Web.UI.Page
{

    private static string[] RainbowColour = { "Red", "Orange", "Yellow", "Lime", "SkyBlue", "Violet", "red", "Pink", "Brown", "Teal", "Turquiose", "MediumSeaGreen", "Cyan", "Navy", "Olive" };
    private int outcomeIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        { 
            int testingvariable = ((Globals)Session["Globals"]).PossibleOutComes.Count;//if this session is empty then close straight away
            if(testingvariable == 0)//if there is no results that can be found, also leave
                Response.Redirect("~/Default.aspx");
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }

        Random rnd = new Random();
        string[] randomColours = RainbowColour.OrderBy(x => rnd.Next()).ToArray();

        if (!IsPostBack)
        {
            Globals gb = (Globals)Session["Globals"]; //pull GB

            int count = -1;
            foreach (Modules mod in gb.UserModules)
            {
                count++;
                mod.BkColour = Color.FromName(randomColours[count]);
                if (mod.BkColour == Color.Navy || mod.BkColour == Color.Red || mod.BkColour == Color.Brown || mod.BkColour == Color.Olive || mod.BkColour == Color.Purple)
                {
                    mod.FgColour = Color.White;
                }
                else
                {
                mod.FgColour = Color.Black;
                }


            }

            int numberOfOutcomes = gb.PossibleOutComes.Count();
            lblOutcomes.Text = "Solution [ 1 of " + numberOfOutcomes + " ]";

            #region test
            //generate(ref gb);
            #endregion

            gb.DisplayOutcome(0);
            UpdateTable(ref gb.TimeTable);
            gb.outcomecounter = 0;
            int outcomeIndex = 0;
            Session["OutcomeIndex"] = outcomeIndex;

            if (Session["Clash"] != null)
            {
                //Response.Write("window.alert('" + Session["Clash"].ToString() + "');");
                string test = "alert('" + Session["Clash"].ToString() + "')";
                //string input = "alert('I could not find a perfect solution for you so I generated a TimeTable without: INF 315 (P) Please remember to add this module in manually')";//Session["Clash"].ToString();


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", test, true);
            }

        }


    }




    protected void btnTime_Click(object sender, EventArgs e)
    {

    }

    protected void btnEmail_Click(object sender, EventArgs e)
    {

    }

    protected void btnSaveImg_Click(object sender, EventArgs e)
    {
        #region [ATTEMPTED PDF EXPORT]
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //pnlTimeTable.RenderControl(hw);
        //StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End(); 
        #endregion

        #region [ATTEMPTED PNG EXPORT]
        //string base64 = Request.Form[hfImageStream.UniqueID].Split(',')[1];
        //byte[] bytes = Convert.FromBase64String(base64);
        //Response.Clear();
        //Response.ContentType = "image/png";
        ////string HeaderToAdd = "attachment; filename=TimeTable - " + System.DateTime.Now.ToShortDateString() + " - " + System.DateTime.Now.ToShortTimeString() + ".png";
        //Response.AddHeader("Content-Disposition", "attachment; filename=TimeTable.png");
        //Response.Buffer = true;
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.BinaryWrite(bytes);
        //Response.End(); 
        #endregion
    }

    /// <summary>
    /// Clears the table, Redisplays the table from the internal class
    /// </summary>
    /// <param name="Grid1">datagridview to implement onto</param>
    public void UpdateTable(ref Table TimeTable)
    {

        //var frm = Application.OpenForms["Main"];
        for (int j = 1; j < TimeTable.GetxLen(); j++)
        {
            for (int k = 0; k < TimeTable.GetyLen() - 2; k++)
            {
                try
                {
                    string[] sline = TimeTable.sTable[j, k].sTable.Split('#');
                    tblOutput.Rows[k + 1].Cells[j].Text = sline[0] + "<br/>" + sline[1];
                }
                catch
                {  //some objects will be empty so you have to first test to see if its actually compatitble to be split or not
                    tblOutput.Rows[k + 1].Cells[j].Text = TimeTable.sTable[j, k].sTable;
                }

                // tblOutput.
                tblOutput.Rows[k + 1].Cells[j].BackColor = TimeTable.sTable[j, k].BkColour;
                tblOutput.Rows[k + 1].Cells[j].ForeColor = TimeTable.sTable[j, k].FgColour;
                


            }

        }
    }

    protected void btnPreviousOutcome_Click(object sender, EventArgs e)
    {
        int index = (int)Session["OutcomeIndex"];
        Globals gb = (Globals)Session["Globals"];
        index--;
        if (index >= 0)
        {
            gb.DisplayOutcome(index);//tested displaying an outcome, doesnt work
            UpdateTable(ref gb.TimeTable);
            int numberOfOutcomes = gb.PossibleOutComes.Count();
            lblOutcomes.Text = "[ " + (index + 1) + " of " + numberOfOutcomes + " ]";
        }
        else
        {
            index = gb.PossibleOutComes.Count() - 1;
            gb.DisplayOutcome(index);//tested displaying an outcome, doesnt work
            UpdateTable(ref gb.TimeTable);
            int numberOfOutcomes = gb.PossibleOutComes.Count();
            lblOutcomes.Text = "[ " + (index + 1) + " of " + numberOfOutcomes + " ]";
            //alert
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No previous outcomes to display!')", true);
        }
        Session["OutcomeIndex"] = index;
    }

    protected void btnNextOutcome_Click(object sender, EventArgs e)
    {
        int index = (int)Session["OutcomeIndex"];
        Globals gb = (Globals)Session["Globals"];
        index++;
        if (index < gb.PossibleOutComes.Count)
        {
            gb.DisplayOutcome(index);//tested displaying an outcome, doesnt work
            UpdateTable(ref gb.TimeTable);
            int numberOfOutcomes = gb.PossibleOutComes.Count();
            lblOutcomes.Text = "[ " + (index + 1) + " of " + numberOfOutcomes + " ]";
        }
        else
        {
            index = 0;
            gb.DisplayOutcome(index);//tested displaying an outcome, doesnt work
            UpdateTable(ref gb.TimeTable);
            int numberOfOutcomes = gb.PossibleOutComes.Count();
            lblOutcomes.Text = "[ " + (index + 1) + " of " + numberOfOutcomes + " ]";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No more outcomes to display!')", true);
            //alert
        }
        Session["OutcomeIndex"] = index;
    }

    protected void lblOutcomes_Load(object sender, EventArgs e)
    {

    }

    protected void btnSaveImg_Click1(object sender, EventArgs e)
    {

    }



    protected void btnPreviewImg_Click(object sender, EventArgs e)
    {
        string jsScriptName = "PreviewImg()";
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "uniqueKey", jsScriptName, true);
       // Response.Redirect("Results.aspx#divPreviewImage");
    }
}

public static class MessageBox
{
    public static void Show(this Page Page, String Message)
    {
        Page.ClientScript.RegisterStartupScript(
           Page.GetType(),
           "MessageBox",
           "<script language='javascript'>alert('" + Message + "');</script>"
        );
    }
}