using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wizard : System.Web.UI.Page
{
    #region [PAGE VARIABLES & OBJECTS]
    int iCheckCount = 0; // Counter variable for checking number of campuses.
    //string sPath1 = Constants.EMPTY_STRING; // Blank string for primary campus path capture.
    //string sPath2 = Constants.EMPTY_STRING; // Blank string for secondary campus path capture.
    string path = TUKSDOMAIN + "hatfield_TimeTable.csv";
    const string TUKSDOMAIN = "http://upnet.up.ac.za/tt/myTimeTable/";

    List<string> allMods = new List<string>();
    List<string> filterMods = new List<string>();
    const string COLLAPSE_ACTIVE_KEYWORD = "in";

    const string CSV_EXTENTION = ".csv";

    const string HATFIELD = "hatfield_TimeTable";
    const string ENG = "eng_TimeTable";
    const string GROENKLOOF = "groenkloof_TimeTable";
    const string MAMELODI = "mamelodi_TimeTable";
    const string THEOLOGY = "theology_TimeTable";


    const string PANEL1_SESSION = "ActiveOne";
    const string PANEL2_SESSION = "ActiveTwo";
    const string PANEL3_SESSION = "ActiveThree";
    const string MODULES_SESSION = "Modules";
    const string PATH_SESSION = "Paths";
    private const string CLASH_SESSION = "Clash";
    private const string INPUTS_SESSION = "Inputs";

    #endregion

    /// <summary>
    /// changes the keyword input from the user to the specified one in the csv. Simply adds a space by the third character if there isn't already one
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    private static string fixKeywordInput(string keyword)
    {
        if (keyword.Length > 3 && keyword[3] != ' ')
        {
            keyword = keyword.Insert(3, " ");
        }
        keyword = keyword.Trim().ToLower();//change input to lower case for comparison purposes
        return keyword;
    }

    private static string temp;
    private static string getModuleName(string sline, string ModuleName)
    {
        temp = "";
        try
        {
            temp = sline.Split(',')[1].Split('/')[1];
        }
        catch
        {
            try
            {
                temp = sline.Split(',')[1].Split(' ')[1] + " " + sline.Split(',')[1].Split(' ')[2];
            }
            catch { }
        }

        return temp;
    }

    enum pullType
    {
        ModuleName,
        FullString
    }

    /// <summary>
    /// downloads the data from the csv files and saves them into session.
    /// First attempts to read from session data. If nothing found, pull from csv
    /// </summary>
    /// <param name="url">the csv url</param>
    /// <param name="keyword">the module code entered by the user</param>
    /// <param name="PullType">enum indicating full string or just the module name</param>
    /// <returns></returns>
    private List<string> pullFromCsv(string url, string keyword, pullType PullType)
    {
        string campusName;
        if (PullType == pullType.FullString)
        {
            keyword = keyword.ToLower();
            campusName = url.Substring(TUKSDOMAIN.Length) + "full";
        }
        else
        {
            campusName = url.Substring(TUKSDOMAIN.Length);
            keyword = fixKeywordInput(keyword);
        }
        
        List<string> returnList = new List<string>();

        if (((List<string>)Session[campusName]) != null)
        {
            //first pull from existing session
            returnList = ((List<string>)Session[campusName]).Where(o => o.ToLower().Contains(keyword)).ToList();
        }

        if (returnList == null || returnList.Count() == 0)
        {
            #region pull from csv
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader csvReader = new StreamReader(resp.GetResponseStream(), true))
            {
                string sline = Constants.EMPTY_STRING;
                string ModuleName = Constants.EMPTY_STRING;
                while (!string.IsNullOrWhiteSpace(sline = csvReader.ReadLine()))//read till end of csv
                {
                    if (sline.ToLower().Contains(keyword))//if the csv line contains the user entered module code
                    {
                        if (PullType == pullType.FullString)
                        {
                            returnList.Add(sline);//save full line
                        }
                        else
                        {
                            ModuleName = getModuleName(sline, ModuleName);

                            if (ModuleName.ToLower() != "module"
                                && !returnList.Any(o => o == ModuleName))
                            {
                                returnList.Add(ModuleName);
                            }
                        }
                    }

                    sline = Constants.EMPTY_STRING;

                }
                Session[campusName] = returnList;
            }
            #endregion
        }

        return returnList;
    }

    public List<string> GetStrings(List<string> campuses, string search)
    {
        List<string> tempreturn = new List<string>();
        foreach (string campus in campuses)
        {
            tempreturn.AddRange(pullFromCsv(TUKSDOMAIN + campus + CSV_EXTENTION, search, pullType.FullString));
        }

        return tempreturn.Distinct().OrderBy(o => o).ToList();
    }

    public List<string> GetModules(List<string> campuses, string search)
    {
        List<string> tempreturn = new List<string>();
        foreach (string campus in campuses)
        {
            tempreturn.AddRange(pullFromCsv(TUKSDOMAIN + campus + CSV_EXTENTION, search, pullType.ModuleName));
        }

        return tempreturn.Distinct().OrderBy(o => o).ToList();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetActivePanel(1);
        }

        if (IsPostBack)
        {
            string script = "$(document).ready(function () { $('[id*=btnSearch]').click(); });";
            ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            //ClientScript.RegisterStartupScript(this.GetType(), "load", "$('#loadmodal').hide()", true);
        }

        try
        {
            List<string> testUrls = new List<string>();

            testUrls.Add(TUKSDOMAIN + HATFIELD + CSV_EXTENTION);
            testUrls.Add(TUKSDOMAIN + ENG + CSV_EXTENTION);
            testUrls.Add(TUKSDOMAIN + GROENKLOOF + CSV_EXTENTION);
            testUrls.Add(TUKSDOMAIN + MAMELODI + CSV_EXTENTION);
            testUrls.Add(TUKSDOMAIN + THEOLOGY + CSV_EXTENTION);

            string test = Constants.EMPTY_STRING;
            foreach (string url in testUrls)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            }

        }
        catch (Exception)
        {
            NotificationCenter.ShowNotification(this, "We are experiencing difficulty contacting UP servers. Please bear with us while we attempt to resolve this. Certain funtionality may be limited. If the problem persists please contact us via the contact page.");
        }

    }

    List<string> paths = new List<string>();
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        paths = new List<string>();

        SetActivePanel(2);
        try
        {
            if (cbxEngineering.Checked == true)
            {
                paths.Add(ENG);
            }
            if (cbxGroenkloof.Checked == true)
            {
                paths.Add(GROENKLOOF);
            }
            if (cbxHatfield.Checked == true)
            {
                paths.Add(HATFIELD);
            }
            if (cbxMamelodi.Checked == true)
            {
                paths.Add(MAMELODI);
            }
            if (cbxTheology.Checked == true)
            {
                paths.Add(THEOLOGY);
            }
            if (paths.Count < 1)
            {
                NotificationCenter.ShowNotification(this, "Please be sure to select at least one campus!");
                SetActivePanel(1);
                return;
            }

            Session[MODULES_SESSION] = GetModules(paths, txtInput.Text);
            Session[PATH_SESSION] = paths;
            lbxSource.DataSource = (List<string>)Session[MODULES_SESSION];
            lbxSource.DataBind();
            lbxSource.SelectedIndex = 0;
            lbxSource.Text = lbxSource.SelectedValue;



            if (lbxSource.Items.Count < 1)
            {
                NotificationCenter.ShowNotification(this, "We couldn't find a module with that code. Please try again.");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnCampus_Click(object sender, EventArgs e)
    {
        foreach (CheckBox checkbox in this.Controls) // Finds all checkboxes and increments a counter variable if the box is checked
        {
            if (checkbox.Checked)
                iCheckCount++;
        }

        if (iCheckCount == 0) // Catches unchecked checkboxes
        {
            NotificationCenter.ShowNotification(this, "Please select a campus first.");
        }
        else if (iCheckCount > 2) // Catches checks exceeding 2 checkboxes
        {
            NotificationCenter.ShowNotification(this, "Cannot select more than 2 campuses. Ensure that a maximum of 2 campuses are selected.");
        }
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        SetActivePanel(2);
        try
        {
            if (lbxSource.Items.Count > 1 && lbxSource.SelectedIndex < 0)
            {
                NotificationCenter.ShowNotification(this, "Please select at least one module!");
                return;
            }
            if (lbxSource.Items.Count == 1 && lbxSource.SelectedIndex != 0)
            {
                TransferModuleToMyList(0);
            }
            else
            {
                if (lbxSource.GetSelectedIndices().Length > 1)
                {
                    foreach (int index in lbxSource.GetSelectedIndices())
                    {
                        TransferModuleToMyList(index);
                    }
                }
                else
                {
                    TransferModuleToMyList(lbxSource.SelectedIndex);
                }
            }
        }
        catch (Exception)
        {

        }
        RefreshLbx();
    }

    private void TransferModuleToMyList(int index)
    {
        List<string> sourceItems = (List<string>)Session[MODULES_SESSION];
        lbxDestination.Items.Add(sourceItems[index]);
        lbxDestination.SelectedIndex = index;
        lbxDestination.Text = lbxSource.SelectedValue;
        txtInput.Text = Constants.EMPTY_STRING;
        txtInput.Focus();
    }

    protected void btnTime_Click(object sender, EventArgs e)
    {
        try
        {
            Session[CLASH_SESSION] = null;
            if (!cbxEngineering.Checked && !cbxGroenkloof.Checked && !cbxHatfield.Checked && !cbxMamelodi.Checked && !cbxTheology.Checked)
            {
                //throw error message. Have fun with it later.
                NotificationCenter.ShowNotification(this, "Please select at least one campus or faculty!");
                SetActivePanel(1);
                return;
            }

            List<string> UserModules = new List<string>();
            foreach (var module in lbxDestination.Items)
            {
                UserModules.Add(module.ToString());
            }

            List<string> UserCampuses = new List<string>();
            addToUserCampuses(UserCampuses);

            UserInputs inputs = new UserInputs(UserModules, UserCampuses, ddlPeriod.SelectedValue.ToString(), ddlLanguage.SelectedValue.ToString());
            Session[INPUTS_SESSION] = inputs;

            Globals gb = new Globals();
            List<Module> mods = new List<Module>();
            paths = (List<String>)Session[PATH_SESSION];
            if (paths.Count < 1)
            {
                //throw error message. Have fun with it later.
                NotificationCenter.ShowNotification(this, "Session Timeout. Please reselect at least one campus or faculty!");
                SetActivePanel(1);
                return;
            }
            Module addMod;
            Module temp;
            Lecture inLect;
            GetStrings(paths, Constants.EMPTY_STRING);//first store everything into session once
            foreach (string modName in inputs.Modules)
            {
                addMod = new Module();
                foreach (string fullstring in GetStrings(paths, modName))
                {
                    string[] tempString = fullstring.Split(',');
                    string[] group = tempString[1].Split('/');

                    if (group.Count() > 1)
                    {
                        inLect = new Lecture(fullstring);
                        addMod.Lectures.Add(inLect);
                    }
                }

                temp = new Module();
                temp = addMod;
                mods.Add(temp);
                addMod = null;
            }

            foreach (Module mod in mods)
            {
                gb.AddNewModule(Constants.EMPTY_STRING, mod); // add module method (adapted) adding each module's lectures
            }
            //gb.TimeTable.Clear();
            //gb.FixedBoolTable(ref gb.bTable, gb.FixedModList);
            //gb.ModulesToBeUsed = gb.Get2UseModules(gb.UserModules, inputs.Time, inputs.Language);
            //gb.PossibleOutComes = new List<Globals.PossibleOutcome>();
            //gb.outcomecounter = 0;
            //gb.OneOutcome = new int[0];
            generate(ref gb);

            // gb.Generator(gb.ModulesToBeUsed); // generate from added modules? 

            Session[Constants.GLOBALS_SESSION] = gb; //store GB as is

            if (gb.PossibleOutComes.Count > 0)
            {
                Response.Redirect("~/Results.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                //alert
                NotificationCenter.ShowNotification(this, "We couldn't find a match for you. Please confirm that all provided information is accurate.");
            }
        }
        catch (Exception x)
        {
            NotificationCenter.ShowNotification(this, "" + x.Message + "')");
        }



    }

    private void addToUserCampuses(List<string> UserCampuses)
    {
        if (cbxEngineering.Checked == true)
        {
            UserCampuses.Add("Engineering");
        }
        if (cbxGroenkloof.Checked == true)
        {
            UserCampuses.Add("Groenkloof");
        }
        if (cbxHatfield.Checked == true)
        {
            UserCampuses.Add("Hatfield");
        }
        if (cbxMamelodi.Checked == true)
        {
            UserCampuses.Add("Mamelodi");
        }
        if (cbxTheology.Checked == true)
        {
            UserCampuses.Add("Theology");
        }
    }

    public void generate(ref Globals gb)
    {


        //Globals.Memento Undo = new Globals.Memento(gb.DeepCopyTable(gb.TimeTable), gb.DeepCopyModules(gb.UserModules));
        #region Collect criteria

        gb.sLanguage = ddlLanguage.SelectedItem.ToString();
        gb.PeriodOfPres = ddlPeriod.SelectedItem.ToString();

        #endregion

        Array.Resize(ref gb.TypeCntrPerMod, gb.UserModules.Length);
        for (int i = 0; i < gb.TypeCntrPerMod.Length; i++)
        {
            gb.TypeCntrPerMod[i] = 0;
        }

        gb.TimeTable.Clear();
        gb.FixedBoolTable(ref gb.bTable, gb.FixedModList);
        gb.ModulesToBeUsed = gb.Get2UseModules(gb.UserModules, gb.PeriodOfPres, gb.sLanguage);//collecting the modules that need to be used

        if (gb.ModulesToBeUsed.Length == 0)
        {
            // KryptonMessageBox.Show("I don't think the criterias you selected fits your modules\nWill it be ok if you double check your language and period of presentation?", "Criteria Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            NotificationCenter.ShowNotification(this, "Could not find a result for the criteria selected. Please double check your input.");
        }
        else
        {
            string sTempModList = Constants.EMPTY_STRING;
            for (int sListLoop = 0; sListLoop < gb.ModulesToBeUsed.Length; sListLoop++)
            {
                sTempModList += gb.ModulesToBeUsed[sListLoop].Name + " (" + gb.ModulesToBeUsed[sListLoop].Group[0].Sets[0].type.Substring(0, 1) + ")\n";
            }

            //KryptonMessageBox.Show("The system will proceed with the following modules from your selected criterias:\n" + sTempModList, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);// == System.Windows.Forms.DialogResult.Yes)

            gb.PossibleOutComes = new List<Globals.PossibleOutcome>();
            gb.OutComeCounter = 0;
            gb.OneOutcome = new int[0];
            gb.Generator(gb.ModulesToBeUsed);


            #region try alternatives

            if (gb.PossibleOutComes.Count == 0 && (gb.ModulesToBeUsed.Length - 1 > 0))
            {

                //prepare try alternative
                Globals.SidMod tempSideMod = new Globals.SidMod();
                tempSideMod.ModIndex = 0;
                tempSideMod.sideModule = gb.ModulesToBeUsed[gb.ModulesToBeUsed.Length - 1];
                tempSideMod.sideModuleReference = gb.RefUseModIndex[gb.ModulesToBeUsed.Length - 1];
                tempSideMod.ModIndex = gb.ModulesToBeUsed.Length - 1;

                Array.Resize(ref gb.ModulesToBeUsed, gb.ModulesToBeUsed.Length - 1);
                Array.Resize(ref gb.RefUseModIndex, gb.ModulesToBeUsed.Length);


                //loop through all modules and do the same to see if there is a possible outcome
                // (KryptonMessageBox.Show("Your selected modules do not have any perfect matches\nWould you like me to remove " + tempSideMod.sideModule.Name +
                //        " (" + gb.ModulesToBeUsed[gb.ModulesToBeUsed.Length - 1].Group[0].Sets[0].type.Substring(0, 1) + ") module and generate again?", "Let me try and figure something out for you", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                while ((gb.PossibleOutComes.Count == 0) && (tempSideMod.ModIndex > 0))
                {

                    gb.bTable = new bool[gb.TimeTable.GetxLen(), gb.TimeTable.GetyLen()];
                    gb.FixedBoolTable(ref gb.bTable, gb.FixedModList);

                    gb.PossibleOutComes = new List<Globals.PossibleOutcome>();
                    gb.OutComeCounter = 0;
                    gb.OneOutcome = new int[0];
                    gb.Generator(gb.ModulesToBeUsed);

                    if (gb.PossibleOutComes.Count == 0)
                    {
                        tempSideMod.ModIndex--;
                        tempSideMod.SwopMod(ref gb.ModulesToBeUsed[tempSideMod.ModIndex], ref gb.RefUseModIndex[tempSideMod.ModIndex]);

                    }

                }

                //if successful then
                if (gb.PossibleOutComes.Count > 0)
                {
                    string clashString = "I could not find a perfect solution for you so I generated a TimeTable without: " + tempSideMod.sideModule.Name + " (" + gb.ModulesToBeUsed[gb.ModulesToBeUsed.Length - 1].Group[0].Sets[0].type.Substring(0, 1) + "). Please remember to add this module in manually";
                    Session[CLASH_SESSION] = clashString;

                }
            }
            #endregion
        }



        //UpdateTable();
        //frmMain.UpdateTable(ref frmMain.dgvMain);//update GUI


        //if ((EndFlag == true) && (PossibleOutcomes.Count > 0))
        if (gb.PossibleOutComes.Count > 0)
        {

            //KryptonMessageBox.Show("Looks like we found TimeTable solutions for you! ^_^\nClick left and right to shift between various possibilities", "Yay! =D", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //KryptonMessageBox.Show(sline, "Yay! =D", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //frmMain.NewState();//create new state
            //this.Close();//close the form after completion
            //pass focus back to the main form
            // var formMain = new Main();
            // Application.OpenForms[formMain.Name].Focus();

            // this.Visible = true;
            // AlignForm();
            //displaying the first resultasdsa
            // gb.DisplayOutcome(0);
            //gb.OutComeCounter = 0;
            //  label4.Text = (gb.OutComeCounter + 1) + " out of " + gb.PossibleOutComes.Count.ToString();

        }
        else
        {
            #region Failed


            //KryptonMessageBox.Show("Generator could not find a solution for you", "Generator was exhausted =(", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



            #region Roll back previous state
            //using the memnto design pattern to bring back the previous state that was on the table
            //this is done for user friendly GUI
            //gb.SHBox.Undo(Undo);
            //gb.TimeTable = null;
            //gb.UserModules = null;
            //gb.TimeTable = gb.DeepCopyTable(gb.SHBox.GetStateTable());
            //gb.UserModules = gb.DeepCopyModules(gb.SHBox.GetStateMod());
            //gb.UpdateTable();/////////////////////////////////////////////////////////////////
            //frmMain.UpdateTable(ref frmMain.dgvMain);

            #endregion

            #endregion
        }

        #region initialising items back to basics
        gb.bTable = new bool[gb.TimeTable.GetxLen(), gb.TimeTable.GetyLen()];

        gb.ModuleCounter = 0;
        gb.EndFlag = false;
        #endregion

    }

    public void RefreshLbx()
    {
        List<string> temp = new List<string>();
        foreach (var module in lbxDestination.Items)
        {
            temp.Add(module.ToString());
        }
        temp = temp.Distinct().ToList();
        temp.Sort();
        lbxDestination.Items.Clear();
        foreach (string distinctMod in temp)
        {
            lbxDestination.Items.Add(distinctMod);
        }
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        SetActivePanel(2);

        try
        {
            if (lbxDestination.Items.Count > 1 && lbxDestination.SelectedIndex < 0)
            {
                NotificationCenter.ShowNotification(this, "Please select at least one module to remove!");
                return;
            }

            if (lbxDestination.GetSelectedIndices().Length > 1)
            {
                List<int> selectedindices = new List<int>();
                foreach (var index in lbxDestination.GetSelectedIndices().Reverse())
                {
                    selectedindices.Add(index);
                }
                foreach (int index in selectedindices)
                {
                    lbxDestination.Items[index].Selected = false;
                    lbxDestination.Items.RemoveAt(index);

                    txtInput.Text = Constants.EMPTY_STRING;
                    txtInput.Focus();
                }
            }
            else
            {
                if (lbxDestination.Items.Count == 1 && lbxDestination.SelectedIndex != 0)
                {

                    lbxDestination.Items.RemoveAt(0);

                    txtInput.Text = Constants.EMPTY_STRING;
                    txtInput.Focus();


                }
                lbxDestination.Items.RemoveAt(lbxDestination.SelectedIndex);
            }
            RefreshLbx();
        }
        catch (Exception)
        {

        }
    }

    /// <summary>
    /// allows the page to be active on a certain panel on the accordian. 
    /// </summary>
    /// <param name="activePanelNumber">There is three panels to choose from. Enter a number between 1 and 3</param>
    private void SetActivePanel(int activePanelNumber)
    {
        switch (activePanelNumber)
        {
            case 1:
                Session[PANEL1_SESSION] = COLLAPSE_ACTIVE_KEYWORD;
                Session[PANEL2_SESSION] = Constants.EMPTY_STRING;
                Session[PANEL3_SESSION] = Constants.EMPTY_STRING;
                break;
            case 2:
                Session[PANEL1_SESSION] = Constants.EMPTY_STRING;
                Session[PANEL2_SESSION] = COLLAPSE_ACTIVE_KEYWORD;
                Session[PANEL3_SESSION] = Constants.EMPTY_STRING;
                break;
            case 3:
                Session[PANEL1_SESSION] = Constants.EMPTY_STRING;
                Session[PANEL2_SESSION] = Constants.EMPTY_STRING;
                Session[PANEL3_SESSION] = COLLAPSE_ACTIVE_KEYWORD;
                break;
            default:
                Session[PANEL1_SESSION] = COLLAPSE_ACTIVE_KEYWORD;
                Session[PANEL2_SESSION] = Constants.EMPTY_STRING;
                Session[PANEL3_SESSION] = Constants.EMPTY_STRING;
                break;

        }

    }

    protected void btnCampusNext_Click(object sender, EventArgs e)
    {
        SetActivePanel(2);
        try
        {
            if (cbxEngineering.Checked == true)
            {
                paths.Add(ENG);
            }
            if (cbxGroenkloof.Checked == true)
            {
                paths.Add(GROENKLOOF);
            }
            if (cbxHatfield.Checked == true)
            {
                paths.Add(HATFIELD);
            }
            if (cbxMamelodi.Checked == true)
            {
                paths.Add(MAMELODI);
            }
            if (cbxTheology.Checked == true)
            {
                paths.Add(THEOLOGY);
            }
            if (paths.Count < 1)
            {
                NotificationCenter.ShowNotification(this, "Please be sure to select at least one campus!");
                SetActivePanel(1);
                return;
            }

            Session[MODULES_SESSION] = GetModules(paths, "");
            Session[PATH_SESSION] = paths;
            lbxSource.DataSource = (List<string>)Session[MODULES_SESSION];

            if (lbxSource.Items.Count < 1)
            {
                NotificationCenter.ShowNotification(this, "We couldn't find a module with that code. Please try again.");
            }
        }
        catch (Exception ex)
        {

        }
    }
}