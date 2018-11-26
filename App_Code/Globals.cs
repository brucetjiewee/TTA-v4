using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
//using System.Windows.Forms;//used for various things like application.openforms

using System.Runtime.Serialization.Formatters.Binary;//used for deep copy
using System.Data.OleDb;//used for the database connection
using System.IO;
using System.Xml;//used for deep copying as well
using System.Drawing;
using static Modules.LectureSet;

/// <summary>
/// Summary description for Globals
/// </summary>
public class Globals
{
    private const string YEAR = "Year";
    private const string SEMESTER1 = "Semester 1";
    private const string SEMESTER2 = "Semester 2";
    private const string QUARTER1 = "Quarter 1";
    private const string QUARTER2 = "Quarter 2";
    private const string QUARTER3 = "Quarter 3";
    private const string QUARTER4 = "Quarter 4";
    private const string N_A = "N/A";
    private const string BOTH = "Both";
    private const string LANG_AFRIKAANS = "Afrikaans";
    private const string LANG_ENGLISH = "English";
    private const string LANG_EITHER = "Either";
    private const string BOTH_SHORT = "B";
    private const string AFRIKAANS_SHORT = "A";
    private const string ENGLISH_SHORT = "E";
    private const string SEMESTER1_SHORT = "S1";
    private const string SEMESTER2_SHORT = "S2";
    private const string AFR_QUARTER1_SHORT = "K1";
    private const string AFR_QUARTER2_SHORT = "K2";
    private const string AFR_QUARTER3_SHORT = "K3";
    private const string AFR_QUARTER4_SHORT = "K4";
    private const string QUARTER1_SHORT = "Q1";
    private const string QUARTER2_SHORT = "Q2";
    private const string QUARTER3_SHORT = "Q3";
    private const string QUARTER4_SHORT = "Q4";
    private const string YEAR_SHORT_TYPE1 = "J";
    private const string YEAR_SHORT_TYPE2 = "Y";
    private const string YEAR_SHORT_TYPE3 = "J1";
    private const string YEAR_SHORT_TYPE4 = "Y1";
    private const string YEAR_SHORT_TYPE5 = "J/Y";
    private const string YEAR_SHORT_TYPE6 = "Y/J";
    
    #region database
    /// <summary>
    /// the global path that will be used for the database.
    /// </summary>
    //public string ConString = "provider=microsoft.jet.oledb.4.0;data source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Chylls Suite\\TimeTable.mdb";
    /// <summary>
    /// the list of tables the user has selected to use. some people might need classes from both Groenkloof and from Hatfield
    /// </summary>
    public string[] TableName;// = TimeTable.listDBTables;
                              /// <summary>
                              /// the database connection that will be used globally in different forms
                              /// </summary>
    public OleDbConnection con = new OleDbConnection();
    
    public DateTime UpdateDate;
    #endregion

    #region Modules
    /// <summary>
    /// the modules that the user is currently holding. these are the modules that are added by the user or is a module that the user currently is working with.
    /// these modules are only from the database of TUKS.
    /// </summary>
    public Modules[] UserModules = new Modules[0];
    /// <summary>
    /// the modules that the user has pinned to the TimeTable. Don't want them to be moved by the generator
    /// </summary>
    public Modules[] FixedModList = new Modules[0];
    
    /// <summary>
    /// adds a new empty module onto the users list of modules
    /// </summary>
    public void IncUserModules()
    {
        Array.Resize(ref UserModules, UserModules.Length + 1);
        UserModules[UserModules.Length - 1] = new Modules();
    }

    /// <summary>
    /// returns the full string of the language based upon the shortened version/abbreviation of the language
    /// </summary>
    /// <param name="sLang">Language abbreviation</param>
    /// <returns></returns>
    private static string GetFullLanguageString(string sLang)
    {
        switch (sLang)
        {
            case BOTH_SHORT:
                sLang = BOTH;
                break;
            case AFRIKAANS_SHORT:
                sLang = LANG_AFRIKAANS;
                break;
            case ENGLISH_SHORT:
                sLang = LANG_ENGLISH;
                break;
        }
        return sLang;
    }

    public void AddNewModule(string MCode, Module inMod)
    {
        //adding the item to the module class time
        //this will take a couple of loops
        IncUserModules();//adding module
        int iIndex = UserModules.Length - 1;//getting latest module length
        Modules newMod = UserModules[iIndex];
        newMod.Name = inMod.Lectures[0].ModCode;//inserting module name
                                                             //UserModules[iIndex].SelectedGroupIndex = -1;//determining selected group
                                                             //making command for calculations
        bool emptyFlag;
        int currentGIndex = -1;
        foreach (Lecture inLecture in inMod.Lectures)
        {
            emptyFlag = false;
            //checking if there is a column that is empty
            if (inLecture.Campus == null) //WIP 
            {
                emptyFlag = true;
                break;
            }

            //if there isn't any data missing then continue. or it will give errors
            //#TuksAdmin
            if (emptyFlag != true)
            {
                int GroupIndex = getGroupIndexFromModuleLecture(inLecture);

                if (GroupIndex > currentGIndex)
                {
                    newMod.AddItem();//adding group
                                                  /*because some groups start from a number that isn't 1 
                                                   * so we have to make provision that it doesnt bomb out
                                                   * #TuksAdmin
                                                   */
                    while (newMod.Group.Length <= GroupIndex)
                    { newMod.AddItem(); }

                    currentGIndex = GroupIndex;
                }

                newMod.Group[GroupIndex].AddItem();
                //referencing the group
                Modules.LectureSet group = newMod.Group[GroupIndex];
                int SetsIndex = group.Sets.Length - 1;
                LectureSin set = group.Sets[SetsIndex];//referencing the selected set in the group
                set.Year = inLecture.Year.ToString();//set year
                string sLang = inLecture.Lang;

                sLang = GetFullLanguageString(sLang);

                set.Language = sLang.Trim();//set language
                set.type = inLecture.Type.Trim();//set type

                set.PeriodOfPres = inLecture.TimePeriod.ToString().Trim();//set period of presentation
                                                                          //string day = ;
                set.Day = inLecture.Day.Trim();//set day             
                DateTime StartTime = Convert.ToDateTime(inLecture.StartTime.Trim());
                set.StartTime = StartTime;//set start time
                DateTime EndTime = Convert.ToDateTime(inLecture.EndTime.Trim());
                set.EndTime = EndTime;//set end time
                set.venue = inLecture.Venue.ToString().Trim();//set venue
                set.Others = "'";//set others
                group.GroupFlag = false;
                set.ItemFlag = false;
            }
        }
    }

    private static int getGroupIndexFromModuleLecture(Lecture inLecture)
    {
        string sgroupindex = Constants.EMPTY_STRING;
        foreach (char value in inLecture.Group)
        {//collect all possible integer values from the group
            if (char.IsNumber(value))
                sgroupindex += value;
        }
        int GroupIndex = Convert.ToInt32(sgroupindex) - 1;//convert that string into int
        return GroupIndex;
    }

    #endregion

    #region Table 

    /// <summary>
    /// the globally used TimeTable. this is the one that is shown to the user.
    /// </summary>
    public Table TimeTable = new Table();
    
    #endregion
    
    #region generator

    public void DisplayOutcome(int IndexCounter)
    {
        if (PossibleOutComes.Count > 0)//if there is actually something left in the outcome then show
        {

            TimeTable.Clear();
            //   Application.DoEvents();

            #region displaying all the fixed modules onto the TimeTable
            foreach (Modules fix in FixedModList)
            {
                int modIndex = 0;
                foreach (Modules User in UserModules)
                {
                    if (fix.Name == User.Name)
                        break;
                    modIndex++;
                }

                int groIndex = 0;
                foreach (Modules.LectureSet Gro in fix.Group)
                {
                    int setIndex = 0;
                    foreach (Modules.LectureSet.LectureSin Set in Gro.Sets)
                    {
                        if (Set.ItemFlag)
                            TimeTable.AddItem(UserModules[modIndex], groIndex, setIndex, ref this.TimeTable);//**1
                        setIndex++;
                    }
                    groIndex++;
                }
            }
            #endregion

            string ModCode;
            int UserModCounter;
            int ToUseGroupIndex;
            int groupindex;
            int setindex;
            int i;
            int iset;
            for (int imodloop = 0; imodloop < PossibleOutComes[IndexCounter].PossibleOutcomes.Count; imodloop++)
            {
                //referencing the module
                ModCode = ModulesToBeUsed[imodloop].Name;
                UserModCounter = -1;
                for (i = 0; i < UserModules.Length; i++)
                {
                    if (UserModules[i].Name == ModCode)
                    {
                        UserModCounter = i;
                        break;
                    }
                }

                ToUseGroupIndex = PossibleOutComes[IndexCounter].PossibleOutcomes[imodloop];
                groupindex = RefUseModIndex[imodloop].GroupList[ToUseGroupIndex].GroupIndex;
                for (iset = 0; iset < RefUseModIndex[imodloop].GroupList[ToUseGroupIndex].SetIndex.Length; iset++)
                {

                    setindex = RefUseModIndex[imodloop].GroupList[ToUseGroupIndex].SetIndex[iset];
                    TimeTable.AddItem(UserModules[UserModCounter], groupindex, setindex, ref this.TimeTable);//**1
                }
                UserModules[UserModCounter].UpdateGroupCheck();
            }
        }

    }

    #region COllect Modules and sets
    #region Criteria Declaration
    //input data
    /// <summary>
    /// Language Criteria
    /// </summary>
    public string sLanguage = Constants.EMPTY_STRING;

    /// <summary>
    /// Period of presentation criteria
    /// </summary>
    public string PeriodOfPres = Constants.EMPTY_STRING;

    #endregion

    /// <summary>
    /// used to verify period of presentation criteria from user with items in the list
    /// </summary>
    /// <param name="stype">item in list of modules</param>
    /// <param name="typeCriteria">user defined specification</param>
    /// <returns></returns>
    bool testPeriod(string speriod, string periodCriteria)
    {
        #region Convert System Abb to long
        string tempType = Constants.EMPTY_STRING;
        switch (speriod)
        {
            case SEMESTER1_SHORT:
                tempType = SEMESTER1;
                break;
            case SEMESTER2_SHORT:
                tempType = SEMESTER2;
                break;
            case AFR_QUARTER1_SHORT:
                tempType = QUARTER1;
                break;
            case AFR_QUARTER2_SHORT:
                tempType = QUARTER2;
                break;
            case AFR_QUARTER3_SHORT:
                tempType = QUARTER3;
                break;
            case AFR_QUARTER4_SHORT:
                tempType = QUARTER4;
                break;
            case QUARTER1_SHORT:
                tempType = QUARTER1;
                break;
            case QUARTER2_SHORT:
                tempType = QUARTER2;
                break;
            case QUARTER3_SHORT:
                tempType = QUARTER3;
                break;
            case QUARTER4_SHORT:
                tempType = QUARTER4;
                break;
            case YEAR_SHORT_TYPE1:
                tempType = YEAR;
                break;
            case YEAR_SHORT_TYPE2:
                tempType = YEAR;
                break;
            case YEAR_SHORT_TYPE3:
                tempType = YEAR;
                break;
            case YEAR_SHORT_TYPE4:
                tempType = YEAR;
                break;
            case YEAR_SHORT_TYPE5:
                tempType = YEAR;
                break;
            case YEAR_SHORT_TYPE6:
                tempType = YEAR;
                break;
            default:
                tempType = YEAR;
                break;
        }
        #endregion

        bool temp = false;
        switch (periodCriteria)
        {
            case SEMESTER1:
                if (tempType == SEMESTER1 || tempType == QUARTER1 || tempType == QUARTER2 || tempType == YEAR) temp = true;
                break;
            case SEMESTER2:
                if (tempType == SEMESTER2 || tempType == QUARTER3 || tempType == QUARTER4 || tempType == YEAR) temp = true;
                break;
            case QUARTER1:
                if ((tempType == QUARTER1 || tempType == SEMESTER1 || tempType == YEAR)) temp = true;
                break;
            case QUARTER2:
                if (tempType == QUARTER2 || tempType == SEMESTER1 || tempType == YEAR) temp = true;
                break;
            case QUARTER3:
                if (tempType == QUARTER3 || tempType == SEMESTER2 || tempType == YEAR) temp = true;
                break;
            case QUARTER4:
                if (tempType == QUARTER4 || tempType == SEMESTER2 || tempType == YEAR) temp = true;
                break;
            case N_A:
                temp = true;
                break;
            case YEAR:
                temp = true;
                break;
        }
        return temp;
    }

    /// <summary>
    /// after the shifting of the modules from the normal list to the tobeused list. the group index changes. this is used to store the old indexs so that the original group can be found.
    /// </summary>
    public ReferenceIndex[] RefUseModIndex = new ReferenceIndex[0];

    public struct ReferenceIndex
    {
        public struct Group
        {
            public int GroupIndex;
            public int[] SetIndex;
        }
        //public int[] SetIndex;
        public Group[] GroupList;
    }

    /// <summary>
    /// these are the modules that would run through the generator
    /// </summary>
    public Modules[] ModulesToBeUsed = new Modules[0];

    /// <summary>
    /// collects modules with criterias and that fit to those criterias. It also sorts all modules according to the least modules first to save time
    /// </summary>
    /// <param name="ModList"></param>
    /// <param name="PeriodCriteria"></param>
    /// <param name="LangCriteria"></param>
    /// <returns></returns>
    public Modules[] Get2UseModules(Modules[] ModList, string PeriodCriteria, string LangCriteria)
    {

        RefUseModIndex = new ReferenceIndex[0];//refresh the list for a new generate
        Modules[] temp = new Modules[0];
        //filter period and language criteria

        #region loop all items

        for (int iModLoop = 0; iModLoop < ModList.Length; iModLoop++)
        {
            //checking if current module exists in the fixed modules
            bool FixedFlag = false;
            foreach (Modules value in FixedModList)
            {
                if (value.Name == ModList[iModLoop].Name)
                {
                    FixedFlag = true;
                    break;
                }
            }

            if (!FixedFlag)
            {
                //int CurrentTypeCounter = TypeCntrPerMod[iModLoop];  //collecting the counter for the current type //**
                string[] ListOfTypes = GetTypeList(ModList[iModLoop]); //collecting the various types within THIS module
                bool ReadAddflag = false;

                for (int itypeLoop = 0; itypeLoop < ListOfTypes.Length; itypeLoop++)
                {
                    bool modAddedFlag = false;
                    for (int iGroupLoop = 0; iGroupLoop < ModList[iModLoop].Group.Length; iGroupLoop++)
                    {
                        string CType = ListOfTypes[itypeLoop];

                        #region Test to see if there is any to be added and flag true
                        //testing to see if there is any to be added and flag true
                        if (ModList[iModLoop].Group[iGroupLoop].Sets != null)//if the set is null then there is nothing to collect and will cause a bomb out on the looping
                        {
                            for (int iSetLoop = 0; iSetLoop < ModList[iModLoop].Group[iGroupLoop].Sets.Length; iSetLoop++)
                            {
                                string slang = ModList[iModLoop].Group[iGroupLoop].Sets[iSetLoop].Language;
                                string stype = ModList[iModLoop].Group[iGroupLoop].Sets[iSetLoop].type.Substring(0, 1);
                                string sPeriod = ModList[iModLoop].Group[iGroupLoop].Sets[iSetLoop].PeriodOfPres;
                                if ((testPeriod(sPeriod, PeriodCriteria) == true) &&
                                    (slang == LangCriteria || slang == BOTH || LangCriteria == LANG_EITHER) &&
                                    (stype == CType))
                                {

                                    //create the module if it isn't already there, since we working with the last item every time, there will be no need to loop through every item
                                    if (temp.Length > 0)
                                    {
                                        #region get already stored type
                                        string qtype = Constants.EMPTY_STRING;
                                        try
                                        {
                                            qtype = temp[temp.Length - 1].Group[0].Sets[0].type;
                                        }
                                        catch { }
                                        #endregion

                                        if ((temp[temp.Length - 1].Name != ModList[iModLoop].Name) ||
                                            (qtype.Substring(0, 1) != CType))//if the module already exist in the temp list then don't increase it
                                        {//as well as the previous type

                                            Array.Resize(ref temp, temp.Length + 1);//add module
                                            Array.Resize(ref RefUseModIndex, temp.Length);//add module reference
                                        }
                                    }
                                    else
                                    {
                                        Array.Resize(ref temp, temp.Length + 1);//add module
                                        Array.Resize(ref RefUseModIndex, temp.Length);//add module reference
                                    }

                                    #region lazy copying of modules
                                    if (modAddedFlag == false)
                                    {
                                        //i need to perform a deep copy or it will not work out
                                        temp[temp.Length - 1] = DeepCopyModule(ModList[iModLoop]);//copy the whole module through so that I don't need to code so much

                                        Array.Resize(ref temp[temp.Length - 1].Group, 0);//add group
                                                                                         //Array.Resize(ref ReferenceGroupIndex[ReferenceGroupIndex.Length - 1], 0);
                                        modAddedFlag = true;
                                    }
                                    #endregion

                                    temp[temp.Length - 1].AddItem();//add group
                                    Array.Resize(ref RefUseModIndex[temp.Length - 1].GroupList, temp[temp.Length - 1].Group.Length);

                                    RefUseModIndex[temp.Length - 1].GroupList[temp[temp.Length - 1].Group.Length - 1].GroupIndex = iGroupLoop;//storing original group index
                                                                                                                                              //#endregion

                                    ReadAddflag = true;
                                    break;
                                }
                            }
                        }
                        #endregion

                        #region Add Sets Into List
                        //once the new empty module has been created, we loop through everything to get all the relevant sets into that empty module
                        if (ReadAddflag == true)
                        {
                            for (int iSetLoop = 0; iSetLoop < ModList[iModLoop].Group[iGroupLoop].Sets.Length; iSetLoop++)
                            {
                                string slang = ModList[iModLoop].Group[iGroupLoop].Sets[iSetLoop].Language;
                                string stype = ModList[iModLoop].Group[iGroupLoop].Sets[iSetLoop].type.Substring(0, 1);
                                string sPeriod = ModList[iModLoop].Group[iGroupLoop].Sets[iSetLoop].PeriodOfPres;
                                if ((testPeriod(sPeriod, PeriodCriteria) == true) && (slang == LangCriteria || slang == BOTH || LangCriteria == LANG_EITHER) && (stype == CType))
                                {
                                    int modUsed = temp.Length - 1;
                                    int groupUsed = temp[temp.Length - 1].Group.Length - 1;
                                    temp[modUsed].Group[groupUsed].AddItem();
                                    int setUsed = temp[modUsed].Group[groupUsed].Sets.Length - 1;

                                    temp[modUsed].Group[groupUsed].Sets[setUsed] = ModList[iModLoop].Group[iGroupLoop].Sets[iSetLoop];

                                    #region reference set index
                                    Array.Resize(ref RefUseModIndex[temp.Length - 1].GroupList[temp[temp.Length - 1].Group.Length - 1].SetIndex, temp[temp.Length - 1].Group[temp[temp.Length - 1].Group.Length - 1].Sets.Length);

                                    RefUseModIndex[temp.Length - 1].GroupList[temp[temp.Length - 1].Group.Length - 1].SetIndex[temp[temp.Length - 1].Group[temp[temp.Length - 1].Group.Length - 1].Sets.Length - 1] = iSetLoop;
                                    #endregion
                                }
                            }
                            ReadAddflag = false;//after implementing, set the flag to false again
                        }
                        #endregion
                    }
                    
                }
            }

        }

        #endregion

        #region Reordering modules
        List<Modules> sortList = temp.ToList<Modules>();
        List<ReferenceIndex> sortRefList = RefUseModIndex.ToList<ReferenceIndex>();
        sortList.Sort(delegate (Modules p1, Modules p2)
        {
            return p1.Group.Length.CompareTo(p2.Group.Length);
        });
        sortRefList.Sort(delegate (ReferenceIndex p1, ReferenceIndex p2)
        {
            return p1.GroupList.Length.CompareTo(p2.GroupList.Length);
        });
        temp = sortList.ToArray<Modules>();
        RefUseModIndex = sortRefList.ToArray<ReferenceIndex>();
        #endregion

        return temp;
    }

    /// <summary>
    /// collects the various types stored in the database with regards to one module
    /// </summary>
    /// <param name="currentmodule"></param>
    /// <returns></returns>
    public static string[] GetTypeList(Modules currentmodule)
    {
        bool flag;
        string previous;
        string[] temp = new string[0];
        int j;
        int looptemp;
        for (int i = 0; i < currentmodule.Group.Length; i++)
        {
            if (currentmodule.Group[i].Sets != null)
            {
                previous = "~";//placing a random letter there so that the first test will not bomb out. and knowing that it doesn't exist in any of them
                for (j = 0; j < currentmodule.Group[i].Sets.Length; j++)
                {
                    if (currentmodule.Group[i].Sets[j].type[0] != previous[0])
                    {
                        flag = false;
                        for (looptemp = 0; looptemp < temp.Length; looptemp++)
                        {
                            if (temp[looptemp] == currentmodule.Group[i].Sets[j].type[0].ToString())
                                flag = true;
                        }

                        if (flag == false)
                        {
                            Array.Resize(ref temp, temp.Length + 1);
                            previous = currentmodule.Group[i].Sets[j].type[0].ToString();
                            temp[temp.Length - 1] = currentmodule.Group[i].Sets[j].type[0].ToString();
                        }
                    }
                }
            }
        }

        return temp;
    }

    #endregion

    #region outcome

    //public int[] AllPossibility = null;
    public int[] OneOutcome = new int[0];

    [Serializable]
    public struct PossibleOutcome
    {
        public List<int> PossibleOutcomes;
        public int[] SortEarlyPoints;
        public int[] SortLatePoints;
        public int[] SortClassPoints;
        public int index;

        public int TotE()
        {
            int temp = 0;
            foreach (int value in SortEarlyPoints)
            {
                temp += value;
            }
            return temp;
        }
        public int TotL()
        {
            int temp = 0;
            foreach (int value in SortLatePoints)
            {
                temp += value;
            }
            return temp;
        }
        public int TotC()
        {
            int temp = 0;
            foreach (int value in SortClassPoints)
            {
                temp += value;
            }
            return temp;
        }
        public int TotLW()
        {
            int itemtot = 0;
            for (int i = 3; i < 6; i++)//Wednesday to Friday
            {
                itemtot += SortClassPoints[i];
            }
            return itemtot;
        }
        public int TotEW()
        {
            int itemtot = 0;
            for (int i = 1; i < 4; i++)//Monday to Wednesday
            {
                itemtot += SortClassPoints[i];
            }
            return itemtot;
        }
        public int TotLWends()
        {
            int itemtot = 0;
            itemtot += SortClassPoints[1];
            itemtot += SortClassPoints[5];
            return itemtot;
        }
    }

    public List<PossibleOutcome> PossibleOutComes = new List<PossibleOutcome>();

    /// <summary>
    /// used to keep track of the current outcome that is being displayed to the user
    /// </summary>
    public int OutComeCounter = 0;

    public void SortOutcomes(int BoxSelectedIndex)
    {
        #region sort 2
        PossibleOutcome[] tempListOutcome2 = PossibleOutComes.ToArray();
        switch (BoxSelectedIndex)
        {
            case 0:
                #region Late Sort
                Array.Sort(tempListOutcome2, delegate (PossibleOutcome bla1, PossibleOutcome bla2)
                {
                    return bla1.TotL().CompareTo(bla2.TotL());
                });

                #endregion
                break;
            case 1:
                #region Early sort

                Array.Sort(tempListOutcome2, delegate (PossibleOutcome bla1, PossibleOutcome bla2)
                {
                    return bla1.TotE().CompareTo(bla2.TotE());
                });

                #endregion
                break;
            case 2:
                #region Late in week
                Array.Sort(tempListOutcome2, delegate (PossibleOutcome bla1, PossibleOutcome bla2)
                {
                    return bla2.TotLW().CompareTo(bla1.TotLW());
                });
                #endregion
                break;
            case 3:
                #region Early in week
                Array.Sort(tempListOutcome2, delegate (PossibleOutcome bla1, PossibleOutcome bla2)
                {
                    return bla1.TotEW().CompareTo(bla2.TotEW());
                });
                #endregion
                break;
            case 4:
                #region Longer Weekends (monday, friday)
                Array.Sort(tempListOutcome2, delegate (PossibleOutcome bla1, PossibleOutcome bla2)
                {
                    return bla1.TotLWends().CompareTo(bla2.TotLWends());
                });
                #endregion
                break;
        }
        PossibleOutComes = tempListOutcome2.ToList();
        #endregion
    }

    #endregion

    #region Generator Methods

    /// <summary>
    /// adding an item to the back of the array list. mainly used for set index
    /// </summary>
    /// <param name="list"></param>
    /// <param name="item"></param>
    public void AppArray<T>(ref T[] list, T item)
    {
        //T[] temp = list;
        Array.Resize(ref list, list.Length + 1);
        list[list.Length - 1] = item;
        //return temp;
        //list = temp;
    }

    /// <summary>
    /// created to use all its preset methods
    /// </summary>
    static Table temptable = new Table();

    /// <summary>
    /// a 2D array of booleans used to test through the recursive formula to see if items can fit on or not
    /// </summary>
    public bool[,] bTable = new bool[8, 16];

    /// <summary>
    /// Takes the group items and test though all criterias and sees if it will be possible to be implemented onto the bool table
    /// </summary>
    /// <param name="CurrentModule">Current Module being tested</param>
    /// <param name="groupindex">The group Index (integer)</param>
    /// <returns>true = can be implemented, false = cannot be implemented</returns>
    public bool isPossible(Modules CurrentModule, int groupindex)
    {
        int counter = 0; //this keeps track of the amount of items possible
        bool flag = true;
        //Table temptable = new Table();//this is a temporary table made just for some methods that are stored within it
        //created global
        DateTime Start;
        DateTime End;
        int ix;
        int iy;
        int j;
        int NumPer;
        #region loop through items
        for (int i = 0; i < CurrentModule.Group[groupindex].Sets.Length; i++)
        {

            #region Clash
            Start = CurrentModule.Group[groupindex].Sets[i].StartTime;
            End = CurrentModule.Group[groupindex].Sets[i].EndTime;
            ix = temptable.GetX(CurrentModule.Group[groupindex].Sets[i].Day);
            NumPer = temptable.GetNumPeriods(Start, End);
            //clashes
            for (j = 0; j < NumPer; j++)
            {
                iy = temptable.GetY(CurrentModule.Group[groupindex].Sets[i].StartTime) + j;

                if (bTable[ix, iy] == true)//if it is taken
                {
                    flag = false;

                    break;
                }
            }
            #endregion
            //if it successfully runs through the procedure without breaking then show that there is an extra item added
            counter++;

            if (flag == false)//if there is one set that doesn't fit on the TimeTable then not the whole group will fit. so end it
                break;
        }
        #endregion

        //if nothing has been added then we should change the flag to false because its obvious that this group can't not work
        if (counter == 0)
            flag = false;

        return flag;
    }

    /// <summary>
    /// after we establish that it can be implemented on the table, we are ready to implement it to the table for other modules to be tested
    /// </summary>
    /// <param name="thistable">2D array of booleans</param>
    /// <param name="CurrentModule">The current module being tested</param>
    /// <param name="groupindex">The group index being tested</param>
    public void AddToBoolTable(ref bool[,] thistable, Modules CurrentModule, int groupindex)
    {
        DateTime Start;
        DateTime End;
        int NumPer;
        int ix;
        int j;
        int iy;
        #region loop through items
        for (int i = 0; i < CurrentModule.Group[groupindex].Sets.Length; i++)
        {
            #region implement
            Start = CurrentModule.Group[groupindex].Sets[i].StartTime;
            End = CurrentModule.Group[groupindex].Sets[i].EndTime;

            NumPer = temptable.GetNumPeriods(Start, End);
            ix = temptable.GetX(CurrentModule.Group[groupindex].Sets[i].Day);

            //clashes
            for (j = 0; j < NumPer; j++)
            {
                iy = temptable.GetY(CurrentModule.Group[groupindex].Sets[i].StartTime) + j;
                thistable[ix, iy] = true;
            }
            #endregion
        }
        #endregion
    }

    /// <summary>
    /// This function is used when the tree fails and items need to be removed from the bool table
    /// </summary>
    /// <param name="thistable">2D array of booleans</param>
    /// <param name="CurrentModule">The current module being tested</param>
    /// <param name="groupindex">The group index being tested</param>
    public void DelFromBoolTable(ref bool[,] thistable, Modules CurrentModule, int groupindex, int setindex)
    {
        //Table temptable = new Table();//this is a temporary table made just for some methods that are stored within it            

        DateTime Start = CurrentModule.Group[groupindex].Sets[setindex].StartTime;
        DateTime End = CurrentModule.Group[groupindex].Sets[setindex].EndTime;

        int NumPer = temptable.GetNumPeriods(Start, End);
        int ix = temptable.GetX(CurrentModule.Group[groupindex].Sets[setindex].Day);
        int iy;
        for (int j = 0; j < NumPer; j++)
        {
            iy = temptable.GetY(CurrentModule.Group[groupindex].Sets[setindex].StartTime) + j;
            thistable[ix, iy] = false;
        }
    }

    /// <summary>
    /// used when the generator cannot find a solution for the user.
    /// </summary>
    public struct SidMod
    {
        /// <summary>
        /// used when the generator cannot find the user a solution. one module is moved here and re-tested
        /// </summary>/
        public Modules sideModule;
        public ReferenceIndex sideModuleReference;
        Modules temp;
        ReferenceIndex tempref;
        public void SwopMod(ref Modules ModToBeSwopped, ref ReferenceIndex RefToBeSwopped)
        {
            temp = DeepCopyModule(sideModule);
            tempref = sideModuleReference;
            sideModule = DeepCopyModule(ModToBeSwopped);
            sideModuleReference = RefToBeSwopped;
            ModToBeSwopped = DeepCopyModule(temp);
            RefToBeSwopped = tempref;
        }
        public int ModIndex;
    }

    #endregion
    #region deep copy

    public Modules[] DeepCopyModules(Modules[] ModList)
    {
        Modules[] ModCopy = new Modules[0];
        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, ModList);
            stream.Position = 0;
            ModCopy = (Modules[])formatter.Deserialize(stream);
        }
        return ModCopy;
    }

    /// <summary>
    /// c# only creates shallow copies two items to save space. but there are places that i need to work with two totally different items. and thats where i would use this method.
    /// this method only makes a deep copy of one single module
    /// </summary>
    /// <param name="ModList"></param>
    /// <returns></returns>
    public static Modules DeepCopyModule(Modules ModList)
    {
        Modules ModCopy = new Modules();
        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, ModList);
            stream.Position = 0;
            ModCopy = (Modules)formatter.Deserialize(stream);
        }
        return ModCopy;
    }
    #endregion

    public void FixedBoolTable(ref bool[,] BoolTable, Modules[] FList)
    {
        DateTime Start;
        DateTime End;
        int NumPer;
        int x;
        int y;
        for (int iMod = 0; iMod < FList.Length; iMod++)
        {
            for (int iGro = 0; iGro < FList[iMod].Group.Length; iGro++)
            {
                for (int iSet = 0; iSet < FList[iMod].Group[iGro].Sets.Length; iSet++)
                {
                    if (FList[iMod].Group[iGro].Sets[iSet].ItemFlag)
                    {
                        Start = FList[iMod].Group[iGro].Sets[iSet].StartTime;
                        End = FList[iMod].Group[iGro].Sets[iSet].EndTime;


                        NumPer = TimeTable.GetNumPeriods(Start, End);
                        x = TimeTable.GetX(FList[iMod].Group[iGro].Sets[iSet].Day);
                        for (int i = 0; i < NumPer; i++)
                        {
                            y = TimeTable.GetY(FList[iMod].Group[iGro].Sets[iSet].StartTime.AddHours(i));
                            BoolTable[x, y] = true;
                        }
                    }
                }
            }
        }
    }

    public int[] TypeCntrPerMod = new int[0];

    public bool[] FilledIndex = new bool[0];
    public bool FillFlag;

    /// <summary>
    /// Used to keep track of the current module that is running live in the recursive function
    /// </summary>
    public int ModuleCounter = 0;
    public bool EndFlag = false;
    public bool possibleFlag = false;
    public int LowerOutcomeGroupIndex = 0;

    /// <summary>
    /// this is the main generator method. Recursive function that runs through each module and each group and tries to find a set of modules that works
    /// </summary>
    /// <param name="ListModules">List of user modules</param>
    /// <param name="thistable">The TimeTable to build onto</param>
    public void Generator(Modules[] ListModules)
    {

        int CurrentModCounter = ModuleCounter, CurrentGroCounter = 0;
        //int[] SetImplemented = new int[1];
        //bool ImplementedGroupFlag = false;

        if (ListModules.Length == 0)
        {
            EndFlag = true;
        }
        else
            if (ModuleCounter >= ListModules.Length &&
                CurrentGroCounter >= ListModules[ListModules.Length - 1].Group.Length)
        {
            EndFlag = true;
        }
        else
            if (ModuleCounter < ListModules.Length)//if it hasn't reached the last empty loop
        {
            int i;
            #region Not at last empty loop
            //int CurrentTypeCounter = TypeCntrPerMod[ModuleCounter];
            if ((FillFlag == true) && (FilledIndex[ModuleCounter] == true))
            {
                #region Filled modules
                ModuleCounter++;
                if (ModuleCounter < ListModules.Length)
                {//testing to see if its the last item again, just to make sure
                    //wait.current = ListModules[ModuleCounter - 1].Name + " Skipped";
                    AppArray(ref OneOutcome, CurrentGroCounter);//**
                    //OneOutcome.Group - CurrentGroCounter;
                    //SinglePossibility[ModuleCounter] = CurrentGroCounter;
                    #region Test if end of tree
                    if (possibleFlag != true)
                    {
                        //test to see if all groups up till this module has reached the end
                        //this is possible because all group indices's have been stored in the possible array of integers
                        EndFlag = true;
                        for (i = 0; i < OneOutcome.Length; i++)//true until proven guilt principle used here
                        {
                            if ((OneOutcome[i] < ListModules[i].Group.Length - 1))
                            {
                                EndFlag = false;
                                break;
                            }
                        }
                    }
                    #endregion
                    Generator(ListModules);//loop on                    
                }
                #endregion
            }
            else
            {
                #region Full Generate
                //string[] ListOfTypes = GetTypeList(ListModules[ModuleCounter]);

                for (int j = 0; j < ListModules[ModuleCounter].Group.Length; j++)
                {

                    #region Collecting CType from modules in list
                    string CType = ListModules[ModuleCounter].Group[j].Sets[0].type.Substring(0, 1);
                    #endregion

                    if (ListModules[ModuleCounter].Group[j].Sets != null)//some groups of some modules are null
                    {
                        CurrentGroCounter = j;
                        bool possibleflag = isPossible(ListModules[ModuleCounter], CurrentGroCounter);
                        if (possibleflag == true)
                        {

                            AddToBoolTable(ref bTable, ListModules[ModuleCounter], j);//add to the boolean table for further testing

                            ModuleCounter++;

                            AppArray(ref OneOutcome, CurrentGroCounter);//appending group onto the array

                            Generator(ListModules);//loop on

                            //this will make sure that the counter is at the right interval
                            ModuleCounter = CurrentModCounter;//instead of decreasing it, just implement the one stored in the beginning

                            if (EndFlag == true)
                                break;//break out of the last loop if it has reached the end already. End of branch

                            //testing to see if it has reached the end needs to be checked before the last break out of the last recursion
                            #region Test if end of tree
                            if ((possibleFlag != true))
                            {
                                //test to see if all groups up till this module has reached the end
                                //this is possible because all group indices's have been stored in the possible array of integers
                                EndFlag = true;
                                for (i = 0; i < OneOutcome.Length; i++)//true until proven guilt principle used here
                                {
                                    if ((OneOutcome[i] < ListModules[i].Group.Length - 1))
                                    {
                                        EndFlag = false;
                                        break;
                                    }
                                }

                                if (EndFlag == true)//if the one that it broke out of before hasn't reached the end then it should continue
                                    if (ModuleCounter < ListModules.Length - 1)
                                        if (LowerOutcomeGroupIndex < ListModules[ModuleCounter + 1].Group.Length)
                                            EndFlag = false;
                            }



                            #endregion

                            if (EndFlag == true)
                                break;
                            else
                            {
                                //TypeCntrPerMod[ModuleCounter] = CurrentTypeCounter;
                                //if (possibleFlag == false)//if it originally is not implemented then tell the user that the branch did fail
                                                          //wait.current = waitline + " Tree Failed";

                                    //cleaning up previous items from bool table
                                    //ListModules[ModuleCounter].UpdateGroupCheck();
                                    //ImplementedGroupFlag = false;
                                    for (i = 0; i < ListModules[ModuleCounter].Group[CurrentGroCounter].Sets.Length; i++)
                                    {
                                        DelFromBoolTable(ref bTable, ListModules[ModuleCounter], j, i);//clear it from the table so it can continue with the test
                                    }
                                LowerOutcomeGroupIndex = OneOutcome[OneOutcome.Length - 1];
                                Array.Resize(ref OneOutcome, OneOutcome.Length - 1);//removing that index from the possible index

                            }
                        }
                        else
                        {
                            //int waitGroup = ReferenceGroupIndex[ModuleCounter][j];
                            //wait.current = ListModules[ModuleCounter].Name + " Group " + (waitGroup + 1) + " Failed";
                        }

                        if (possibleFlag == true)//this has to be done after the removal of the implementations as "j" is used there
                        {
                            possibleFlag = false; //if it was implemented then clear the implementation
                                                  //j--;//decrease the counter as it is used in the group counter and it should carry on from where it left off
                        }
                    }
                }
                #endregion
            }

            if ((FillFlag == true) && (EndFlag != true))//from the fill
            {
                ModuleCounter = CurrentModCounter;
            }
            #endregion
        }

        //EndFlag = ImplementedGroupFlag;//if the last group has been implemented/if the first group could not be implemented
        #region Add to class when successful
        if (ModuleCounter >= ListModules.Length)
        {
            if (!EndFlag)
            {
                #region Adding to Success Array
                //wait.current = "Adding solution to List";
                //after it reached the end, add it to the possible list
                PossibleOutcome Outcome = new PossibleOutcome();//this stores one possible outcome
                #region possible outcome declaration
                Outcome.PossibleOutcomes = new List<int>();
                Outcome.SortEarlyPoints = new int[Table.xLen];
                Outcome.SortLatePoints = new int[Table.xLen];
                Outcome.SortClassPoints = new int[Table.xLen];
                #endregion
                Outcome.PossibleOutcomes = OneOutcome.ToList<int>();
                ///because the btable is still in use when the outcome is stored, all the reference will still be accurate therefore data
                ///from the btable can still be used and retrieved.

                #region adding sort points
                for (int i = 1; i < Table.xLen; i++)//for every day of the week
                {
                    bool EarlyEndFlag = false;//once it hits a class then this flag goes on and we stop adding points to morning 
                    bool LateEndFlag = false; //same as above just with all the endings of the bottom

                    //initialise the integers
                    Outcome.SortEarlyPoints[i] = 0;
                    Outcome.SortLatePoints[i] = 0;
                    Outcome.SortClassPoints[i] = 0;

                    for (int j = 1; j < Table.yLen; j++)//for every hour of the day
                    {
                        #region collect all points for empty mornings
                        if (!EarlyEndFlag)
                        {
                            if (bTable[i, j])//if it hits its first class then it should stop adding points
                            {
                                EarlyEndFlag = true;//now it shows that it has reached its first class
                            }
                            else
                            {//if there is no class then increase the counter
                                Outcome.SortEarlyPoints[i]++;
                            }
                        }
                        #endregion

                        #region collect all points for empty evenings
                        ///this one is a bit tricky, the way I am going to do this is by
                        ///increasing the counr after every empty slot after a class.
                        ///once it hits a class then it will reset the counter to 0 so once it gets to the last class
                        ///it will no longer reset the counter. ;)
                        if (!LateEndFlag)
                        {
                            if (bTable[i, j])//if there is a class on the TimeTable then reset the counter to 0
                            {
                                Outcome.SortLatePoints[i] = 0;
                            }
                            else
                            {//if there is no clas on the TimeTable then increase the counter
                                Outcome.SortLatePoints[i]++;
                            }
                        }
                        #endregion

                        #region collect all points for lectures during a day
                        if (bTable[i, j])//if there is something in the boolean table then that means there is a class there
                        {
                            Outcome.SortClassPoints[i]++;
                        }
                        #endregion

                    }
                }

                //check that if there is no class on that specific day then the early sort point should be 0
                for (int i = 0; i < Table.xLen; i++)
                {
                    if (Outcome.SortClassPoints[i] == 0)
                    {//if there is no classes on that day
                        Outcome.SortEarlyPoints[i] = 0;
                        Outcome.SortLatePoints[i] = 0;
                    }
                }
                #endregion
                Outcome.index = outcomecounter;
                outcomecounter++;
                PossibleOutComes.Add(Outcome);

                possibleFlag = true;
                #endregion
            }

            //EndFlag = false;


        }

        #endregion


    }
    public int outcomecounter = 0;
    //public GenWait wait = new GenWait();

    #endregion

    #region CellButton
    public Modules ClickedMod;
    public int ClickedGroIndex;
    public int ClickedSetIndex;
    //public ToolStripMenuItem[] RunTimeSetClicks = new ToolStripMenuItem[0];
    #endregion

    #region personal events

    //EventBase userEvent = new PersonalEvent();
    public PersonalEvent[] eventArray = new PersonalEvent[200];
    public int EventCount = 0;

    #endregion
}