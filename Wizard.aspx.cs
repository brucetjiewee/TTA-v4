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
    //string sPath1 = EMPTY_STRING; // Blank string for primary campus path capture.
    //string sPath2 = EMPTY_STRING; // Blank string for secondary campus path capture.
    string path = TUKSDOMAIN + "hatfield_TimeTable.csv";
    const string TUKSDOMAIN = "http://upnet.up.ac.za/tt/myTimeTable/";

    List<string> allMods = new List<string>();
    List<string> filterMods = new List<string>();
    const string COLLAPSE_ACTIVE_KEYWORD = "in";
    const string EMPTY_STRING = "";
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

    #endregion

    List<string> getAllModules(string url, string keyword)
    {
        string campusName = url.Substring(TUKSDOMAIN.Length);
        if (keyword.Length > 3 && keyword[3] != ' ')
        {
            keyword = keyword.Insert(3, " ");
        }
        keyword = keyword.Trim();
        List<string> modules = null;

        if (((List<string>)Session[campusName]) != null)
        {
            //first pull from existing session
            modules = ((List<string>)Session[campusName]).Where(o => o.ToLower().Contains(keyword.ToLower())).ToList();
        }

        //if nothing came from the session then collect from remote
        if (modules == null)
        {
            modules = new List<string>();
            #region pull from csv
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader csvReader = new StreamReader(resp.GetResponseStream(), true))
            {
                string sline = EMPTY_STRING;
                string ModuleName = EMPTY_STRING;
                while (!string.IsNullOrWhiteSpace(sline = csvReader.ReadLine()))
                {
                    try
                    {
                        ModuleName = sline.Split(',')[1].Split('/')[1];
                    }
                    catch
                    {
                        try
                        {
                            ModuleName = sline.Split(',')[1].Split(' ')[1] + " " + sline.Split(',')[1].Split(' ')[2];
                        }
                        catch
                        {

                        }
                    }

                    if (ModuleName.ToLower().Contains(keyword.ToLower()) && ModuleName.ToLower() != "module")
                    {

                        modules.Add(ModuleName);
                    }
                }
                sline = EMPTY_STRING;
                modules.Distinct().ToList().Sort();
                Session[campusName] = modules;
            }
            #endregion
        }

        return modules;
    }

    List<string> getFullStrings(string url, string keyword)
    {
        string campusName = url.Substring(TUKSDOMAIN.Length) + "full";
        List<string> fullStrings = new List<string>();

        if (Session[campusName] == null)
        {
            #region pull from csv
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader csvReader = new StreamReader(resp.GetResponseStream(), true))
            {
                string sline = EMPTY_STRING;
                string ModuleName = EMPTY_STRING;
                while (!string.IsNullOrWhiteSpace(sline = csvReader.ReadLine()))//read till end of csv
                {
                    try
                    {
                        ModuleName = sline.Split(',')[1].Split('/')[1];
                    }
                    catch
                    {
                        try
                        {
                            ModuleName = sline.Split(',')[1].Split(' ')[1] + " " + sline.Split(',')[1].Split(' ')[2];
                        }
                        catch
                        {

                        }
                    }

                    if (ModuleName.ToLower().Contains(keyword.ToLower()))
                    {

                        fullStrings.Add(sline);
                    }

                    sline = EMPTY_STRING;

                }
                Session[campusName] = fullStrings;
            }
            #endregion
        }
        else
        {
            fullStrings = ((List<string>)Session[campusName]).Where(o => o.ToLower().Contains(keyword.ToLower())).ToList();
        }
        return fullStrings;
    }

    public List<string> GetStrings(List<string> campuses, string search)
    {
        List<string> tempreturn = new List<string>();
        foreach (string campus in campuses)
        {
            tempreturn.AddRange(getFullStrings(TUKSDOMAIN + campus + CSV_EXTENTION, search));
        }

        return tempreturn.Distinct().OrderBy(o => o).ToList();
    }

    public List<string> GetModules(List<string> campuses, string search)
    {
        List<string> tempreturn = new List<string>();
        foreach (string campus in campuses)
        {
            tempreturn.AddRange(getAllModules(TUKSDOMAIN + campus + CSV_EXTENTION, search));
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

            string test = EMPTY_STRING;
            foreach (string url in testUrls)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            }

        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('We are experiencing difficulty contacting UP servers. Please bear with us while we attempt to resolve this. Certain funtionality may be limited. If the problem persists please contact us via the contact page.')", true);
        }

    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

        /*

         foreach (var mod in allMods)
         {
             if (mod.ToLower().Contains(txtInput.Text.ToLower()))
             {
          //lbxSource.DataSource = filterMods;
        // lbxSource.DataBind();        
         */
    }

    protected void btnOfDeath_Click(object sender, EventArgs e)
    {
        /*    List<string> temp = new List<string>();
            List<string> selected = new List<string>();

            temp = (List<string>)Session["FilterModules"];

            selected.Add(temp[lbxSource.SelectedIndex]);
            Session["SelectedModules"] = selected;

            temp.RemoveAt(lbxSource.SelectedIndex);

            lbxDestination.DataSource = (List<string>)Session["SelectedModules"];
            lbxDestination.DataBind();
            lbxSource.DataSource = Session["FilterModules"];
            lbxSource.DataBind();*/
    }
    List<string> paths = new List<string>();
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetActivePanel(2);
        try
        {


            if (cbxEngineering.Checked == true)
            {
                paths.Add("eng_TimeTable");
            }
            if (cbxGroenkloof.Checked == true)
            {
                paths.Add("groenkloof_TimeTable");
            }
            if (cbxHatfield.Checked == true)
            {
                paths.Add("hatfield_TimeTable");
            }
            if (cbxMamelodi.Checked == true)
            {
                paths.Add("mamelodi_TimeTable");
            }
            if (cbxTheology.Checked == true)
            {
                paths.Add("theology_TimeTable");
            }
            if (paths.Count < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please be sure to select at least one campus!')", true);
                SetActivePanel(1);
                return;
            }

            Session[MODULES_SESSION] = GetModules(paths, txtInput.Text);
            Session["Paths"] = paths;
            lbxSource.DataSource = (List<string>)Session[MODULES_SESSION];
            lbxSource.DataBind();
            lbxSource.SelectedIndex = 0;
            lbxSource.Text = lbxSource.SelectedValue;



            if (lbxSource.Items.Count < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('We couldn't find a module with that code. Please try again.')", true);
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select a campus first.')", true);
        }
        else if (iCheckCount > 2) // Catches checks exceeding 2 checkboxes
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cannot select more than 2 campuses. Ensure that a maximum of 2 campuses are selected.')", true);
        }
        else // Proceed if a maximum of 2 checkboxes are checked
        {
            // Evaluation of state of campus checkboxes & assignment of paths:
            //#region [URLs]
            //if (cbxTheology.Checked)
            //{
            //    if (sPath1 == "")
            //        sPath1 = "http://upnet.up.ac.za/tt/myTimeTable/theology_TimeTable.csv";
            //    else
            //        sPath2 = "http://upnet.up.ac.za/tt/myTimeTable/theology_TimeTable.csv";
            //}
            //if (cbxHatfield.Checked)
            //{
            //    if (sPath1 == "")
            //        sPath1 = "http://upnet.up.ac.za/tt/myTimeTable/hatfield_TimeTable.csv";
            //    else
            //        sPath2 = "http://upnet.up.ac.za/tt/myTimeTable/hatfield_TimeTable.csv";
            //}
            //if (cbxGroenkloof.Checked)
            //{
            //    if (sPath1 == "")
            //        sPath1 = "http://upnet.up.ac.za/tt/myTimeTable/groenkloof_TimeTable.csv";
            //    else
            //        sPath2 = "http://upnet.up.ac.za/tt/myTimeTable/groenkloof_TimeTable.csv";
            //}
            //if (cbxEngineering.Checked)
            //{
            //    if (sPath1 == "")
            //        sPath1 = "http://upnet.up.ac.za/tt/myTimeTable/eng_TimeTable.csv";
            //    else
            //        sPath2 = "http://upnet.up.ac.za/tt/myTimeTable/eng_TimeTable.csv";
            //}
            //if (cbxMamelodi.Checked)
            //{
            //    if (sPath1 == "")
            //        sPath1 = "http://upnet.up.ac.za/tt/myTimeTable/mamelodi_TimeTable.csv";
            //    else
            //        sPath2 = "http://upnet.up.ac.za/tt/myTimeTable/mamelodi_TimeTable.csv";
            //} 
            //#endregion
        }
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        SetActivePanel(2);
        try
        {


            if (lbxSource.Items.Count > 1 && lbxSource.SelectedIndex < 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select at least one module!')", true);
                return;
            }
            if (lbxSource.Items.Count == 1 && lbxSource.SelectedIndex != 0)
            {
                List<string> sourceItems = (List<string>)Session[MODULES_SESSION];
                lbxDestination.Items.Add(sourceItems[0]);
                lbxDestination.SelectedIndex = 0;
                lbxDestination.Text = lbxSource.SelectedValue;
                //sourceItems.Clear();
                //lbxSource.Items.Clear();
                txtInput.Text = EMPTY_STRING;
                txtInput.Focus();
            }
            else
            {
                if (lbxSource.GetSelectedIndices().Length > 1)
                {
                    foreach (int index in lbxSource.GetSelectedIndices())
                    {

                        List<string> sourceItems = (List<string>)Session[MODULES_SESSION];
                        lbxDestination.Items.Add(sourceItems[index]);
                        lbxDestination.SelectedIndex = index;
                        lbxDestination.Text = lbxSource.SelectedValue;
                        //sourceItems.Clear();
                        //lbxSource.Items.Clear();
                        txtInput.Text = EMPTY_STRING;
                        txtInput.Focus();
                    }
                }
                else
                {
                    List<string> sourceItems = (List<string>)Session[MODULES_SESSION];
                    lbxDestination.Items.Add(sourceItems[lbxSource.SelectedIndex]);
                    //sourceItems.Clear();
                    //lbxSource.Items.Clear();
                    txtInput.Text = EMPTY_STRING;
                    txtInput.Focus();
                }

            }
        }
        catch (Exception)
        {

        }

        RefreshLbx();

    }

    protected void btnTime_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Clash"] = null;
            if (!cbxEngineering.Checked && !cbxGroenkloof.Checked && !cbxHatfield.Checked && !cbxMamelodi.Checked && !cbxTheology.Checked)
            {
                //throw error message. Have fun with it later.
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select at least one campus or faculty!')", true);
                SetActivePanel(1);
                return;
            }



            List<string> UserModules = new List<string>();
            foreach (var module in lbxDestination.Items)
            {
                UserModules.Add(module.ToString());
            }
            List<string> UserCampuses = new List<string>();
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

            UserInputs inputs = new UserInputs(UserModules, UserCampuses, ddlPeriod.SelectedValue.ToString(), ddlLanguage.SelectedValue.ToString());
            Session["Inputs"] = inputs;

            Globals gb = new Globals();
            List<Module> mods = new List<Module>();
            paths = (List<String>)Session["Paths"];
            if (paths.Count < 1)
            {
                //throw error message. Have fun with it later.
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Session Timeout. Please reselect at least one campus or faculty!')", true);
                SetActivePanel(1);
                return;
            }
            Module addMod;
            Module temp;
            Lecture inLect;
            GetStrings(paths, "");//first store everything into session once
            foreach (string modName in inputs.Modules)
            {
                addMod = new Module();
                foreach (string fullstring in GetStrings(paths, modName))
                {
                    inLect = new Lecture(fullstring);
                    addMod.Lectures.Add(inLect);
                }

                temp = new Module();
                temp = addMod;
                mods.Add(temp);
                addMod = null;
            }

            foreach (Module mod in mods)
            {

                gb.AddNewModule(EMPTY_STRING, mod); // add module method (adapted) adding each module's lectures

            }
            //gb.TimeTable.Clear();
            //gb.FixedBoolTable(ref gb.bTable, gb.FixedModList);
            //gb.ModulesToBeUsed = gb.Get2UseModules(gb.UserModules, inputs.Time, inputs.Language);
            //gb.PossibleOutComes = new List<Globals.PossibleOutcome>();
            //gb.outcomecounter = 0;
            //gb.OneOutcome = new int[0];
            generate(ref gb);

            // gb.Generator(gb.ModulesToBeUsed); // generate from added modules? 


            Session["Globals"] = gb; //store GB as is

            if (gb.PossibleOutComes.Count > 0)
            {

                Response.Redirect("~/Results.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                //alert
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('We couldn't find a match for you. Please confirm that all provided information is accurate.')", true);
            }
        }
        catch (Exception x)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + x.Message + "')", true);
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Could not find a result for the criteria selected. Please double check your input.')", true);
        }
        else
        {
            string sTempModList = EMPTY_STRING;
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
                    Session["Clash"] = clashString;

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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select at least one module to remove!')", true);
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

                    txtInput.Text = EMPTY_STRING;
                    txtInput.Focus();
                }
            }
            else
            {
                if (lbxDestination.Items.Count == 1 && lbxDestination.SelectedIndex != 0)
                {

                    lbxDestination.Items.RemoveAt(0);

                    txtInput.Text = EMPTY_STRING;
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


    private void SetActivePanel(int activePanelNumber)
    {
        switch (activePanelNumber)
        {
            case 1:
                Session[PANEL1_SESSION] = COLLAPSE_ACTIVE_KEYWORD;
                Session[PANEL2_SESSION] = EMPTY_STRING;
                Session[PANEL3_SESSION] = EMPTY_STRING;
                break;
            case 2:
                Session[PANEL1_SESSION] = EMPTY_STRING;
                Session[PANEL2_SESSION] = COLLAPSE_ACTIVE_KEYWORD;
                Session[PANEL3_SESSION] = EMPTY_STRING;
                break;
            case 3:
                Session[PANEL1_SESSION] = EMPTY_STRING;
                Session[PANEL2_SESSION] = EMPTY_STRING;
                Session[PANEL3_SESSION] = COLLAPSE_ACTIVE_KEYWORD;
                break;
            default:
                Session[PANEL1_SESSION] = COLLAPSE_ACTIVE_KEYWORD;
                Session[PANEL2_SESSION] = EMPTY_STRING;
                Session[PANEL3_SESSION] = EMPTY_STRING;
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
                paths.Add("eng_TimeTable");
            }
            if (cbxGroenkloof.Checked == true)
            {
                paths.Add("groenkloof_TimeTable");
            }
            if (cbxHatfield.Checked == true)
            {
                paths.Add("hatfield_TimeTable");
            }
            if (cbxMamelodi.Checked == true)
            {
                paths.Add("mamelodi_TimeTable");
            }
            if (cbxTheology.Checked == true)
            {
                paths.Add("theology_TimeTable");
            }
            if (paths.Count < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please be sure to select at least one campus!')", true);
                SetActivePanel(1);
                return;
            }

            Session[MODULES_SESSION] = GetModules(paths, "");
            Session["Paths"] = paths;
            lbxSource.DataSource = (List<string>)Session[MODULES_SESSION];

            if (lbxSource.Items.Count < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('We couldn't find a module with that code. Please try again.')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
}