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

    private static string[] RainbowColour = { "#C62828", "#F57C00", "#FFEA00", "#76FF03", "#00B0FF", "#651FFF", "#D500F9", "#F50057", "#4E342E", "#00E5FF" };
    private int outcomeIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int testingvariable = ((Globals)Session[Constants.GLOBALS_SESSION]).PossibleOutComes.Count;//if this session is empty then close straight away
            if (testingvariable == 0)//if there is no results that can be found, also leave
                Response.Redirect("~/Default.aspx");
        }
        catch
        {
            Response.Redirect("~/Default.aspx");
        }

        if (!IsPostBack)
        {
            Globals gb = (Globals)Session[Constants.GLOBALS_SESSION]; //pull GB

            int count = -1;
            foreach (Modules mod in gb.UserModules)
            {
                count++;
                mod.BkColour = Color.FromName(RainbowColour[count]);
            }

            int numberOfOutcomes = gb.PossibleOutComes.Count();
            lblOutcomes.Text = "Solution [ 1 of " + numberOfOutcomes + " ]";

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


                NotificationCenter.ShowNotification(this, test);
            }

        }


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
        Globals gb = (Globals)Session[Constants.GLOBALS_SESSION];
        index--;
        if (index >= 0)
        {
            gb.DisplayOutcome(index);//tested displaying an outcome, doesnt work
            UpdateTable(ref gb.TimeTable);
            int numberOfOutcomes = gb.PossibleOutComes.Count();
            lblOutcomes.Text = "Solution [ " + (index + 1) + " of " + numberOfOutcomes + " ]";
        }
        else
        {
            index = gb.PossibleOutComes.Count() - 1;
            gb.DisplayOutcome(index);//tested displaying an outcome, doesnt work
            UpdateTable(ref gb.TimeTable);
            int numberOfOutcomes = gb.PossibleOutComes.Count();
            lblOutcomes.Text = "Solution [ " + (index + 1) + " of " + numberOfOutcomes + " ]";
            //alert
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No previous outcomes to display!')", true);
        }
        Session["OutcomeIndex"] = index;
    }

    protected void btnNextOutcome_Click(object sender, EventArgs e)
    {
        int index = (int)Session["OutcomeIndex"];
        Globals gb = (Globals)Session[Constants.GLOBALS_SESSION];
        index++;
        if (index < gb.PossibleOutComes.Count)
        {
            gb.DisplayOutcome(index);//tested displaying an outcome, doesnt work
            UpdateTable(ref gb.TimeTable);
            int numberOfOutcomes = gb.PossibleOutComes.Count();
            lblOutcomes.Text = "Solution [ " + (index + 1) + " of " + numberOfOutcomes + " ]";
        }
        else
        {
            index = 0;
            gb.DisplayOutcome(index);//tested displaying an outcome, doesnt work
            UpdateTable(ref gb.TimeTable);
            int numberOfOutcomes = gb.PossibleOutComes.Count();
            lblOutcomes.Text = "Solution [ " + (index + 1) + " of " + numberOfOutcomes + " ]";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No more outcomes to display!')", true);
            //alert
        }
        Session["OutcomeIndex"] = index;
    }

    protected void lblOutcomes_Load(object sender, EventArgs e)
    {

    }

}