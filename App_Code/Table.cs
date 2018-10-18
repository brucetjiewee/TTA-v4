using System;
//using System.Windows.Forms;
using System.Drawing;
using System.IO;
//using ComponentFactory.Krypton.Toolkit;



[Serializable]
public class Table
{
    internal void AppendList<T>(ref T[] list, T item)
    {
        //T[] temp = list;
        Array.Resize(ref list, list.Length + 1);
        list[list.Length - 1] = item;
        //return temp;
        //list = temp;
    }

    public string defaultDBTable = "Hatfield Campus Lecture TimeTable";
    public string[] listDBTables = { "Hatfield Campus Lecture TimeTable" };

    //a combined struct for both content and format looks
    [Serializable]
    public struct sCell
    {
        public string sCode { get; set; }
        public string sTable { get; set; }
        public string sType { get; set; }
        public Color BkColour { get; set; }
        public Color FgColour { get; set; }
        public Font TextFont { get; set; }
        public Modules iModule;
        public int iGroupIndex;
        public int iSetIndex;
        /// <summary>
        /// using references to change the flag
        /// </summary>
        public bool Flag
        {
            get
            {
                return iModule.Group[iGroupIndex].Sets[iSetIndex].ItemFlag;
            }
            set
            {
                iModule.Group[iGroupIndex].Sets[iSetIndex].ItemFlag = value;
            }
        }
        /// <summary>
        /// "personal" for personal events. "tuks" for tuks modules
        /// </summary>
        public string EventType { get; set; }
    }

    /// <summary>
    /// the amount of columns that should be on the TimeTable
    /// </summary>
    public static int xLen = 8;//6 days in a week. 1 for the time
                               /// <summary>
                               /// the amount of rows that should be on the TimeTable
                               /// </summary>
    public static int yLen = 16;//12 periods for the latest
    public sCell[,] sTable = new sCell[xLen, yLen];

    public int GetxLen() { return xLen; }

    public int GetyLen() { return yLen; }

    public Table()
    {
        Clear();
    }

    public void DefualtLooksCell(Color CellDefualt, int x, int y)
    {
        sTable[x, y].BkColour = CellDefualt;
        sTable[x, y].FgColour = Color.Black;
        sTable[x, y].TextFont = null;
    }

    public void InitialiseCell(int x, int y)
    {
        DefualtLooksCell(Color.White, x, y);
        sTable[x, y].sCode = null;
        sTable[x, y].sTable = null;
        sTable[x, y].sType = null;
        try
        {
            sTable[x, y].Flag = false;
        }
        catch { };
    }
    //------------------------------------------------------------------------------------------------------------------------//

    public string[] TimeHead = { "", "07:30", "08:30", "09:30", "10:30", "11:30", "12:30", "13:30", "14:30", "15:30", "16:30", "17:30", "18:30", "19:30", "20:30", "21:30", "22:30", "23:30" };
    public string[] TimeEndHead = { "", "07:20", "08:20", "09:20", "10:20", "11:20", "12:20", "13:20", "14:20", "15:20", "16:20", "17:20", "18:20", "19:20", "20:20", "21:20", "22:20", "23:20" };
    public string[] DayHead = { "", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
    ////////////////////////////////////////////////////////////////////////
    //class internal data gathering
    /// <summary>
    /// this is to get the y coordinates from the time given of the day
    /// </summary>
    /// <param name="sTime"></param>
    /// <returns></returns>
    public int GetY(DateTime sTime)
    {

        int y = -1;
        string ssTime = sTime.TimeOfDay.ToString();
        int iTime = 0;
        try
        {
            iTime = Convert.ToInt32(ssTime.Split(':')[0]);
            //this will place the first section of the time string into an integer
            y = iTime - 7;
            //since the first class starts at 7, the first slot on the TimeTable will be 7

            //some modules will start on a full hour therefore need to 
            if (sTime.Minute == 0)
                y--;
        }
        catch { y = -1; };

        return y;
    }

    /// <summary>
    /// this is to get the x coordinates from the given day of the week
    /// </summary>
    /// <param name="sDay"></param>
    /// <returns></returns>
    public int GetX(string sDay)
    {
        //initialise
        int x = -1;

        //the x value is set according to the day of the week
        switch (sDay)
        {
            case "Monday":
                x = 1;
                break;
            case "Tuesday":
                x = 2;
                break;
            case "Wednesday":
                x = 3;
                break;
            case "Thursday":
                x = 4;
                break;
            case "Friday":
                x = 5;
                break;
            case "Saturday":
                x = 6;
                break;
            case "Sunday":
                x = 7;
                break;
            default:
                x = -1;
                break;
        }

        return x;
    }

    /// <summary>
    /// this is to get the amount of periods this certain module last for.
    /// some last for more than one period
    /// 
    /// this method collects both starting and ending time from the database and then 
    /// subtracts the ending from the start to get the amount of periods
    /// </summary>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    /// <returns></returns>
    public int GetNumPeriods(DateTime Start, DateTime End)
    {
        int HashPeriod;
        string sStart = Start.TimeOfDay.ToString();
        string[] both = sStart.Split(':');
        int iStart = Convert.ToInt32(both[0]);
        if (Start.Minute == 0)
            iStart--;

        string sEnd = End.TimeOfDay.ToString();
        string[] iboth = sEnd.Split(':');
        int iEnd = Convert.ToInt32(iboth[0]);

        HashPeriod = iEnd - iStart;
        return HashPeriod;
    }


    /////////////////////////////////////////////////////////////////////////
    //single operations
    /// <summary>
    /// this is used to initialising the table or to clear the table
    /// </summary>
    public void Clear()
    {
        for (int j = 0; j < xLen; j++)
        {
            for (int k = 0; k < yLen; k++)
            {
                InitialiseCell(j, k);
            }
        }
    }

    /// <summary>
    /// this is used to set a certain module into the time table
    /// the reason for containing the whole entire class is so that 
    /// people can reference each item accordingly
    /// 
    /// this is also going to be used in manual mode
    /// </summary>
    /// <param name="Module"></param>the object module that is being delt with currently
    /// <param name="x"></param>the column
    /// <param name="y"></param>the row
    public void AddItem(Modules sModule, int GroupIndex, int SetIndex, ref Table inTable)
    {
        DateTime Start = sModule.Group[GroupIndex].Sets[SetIndex].StartTime;
        DateTime End = sModule.Group[GroupIndex].Sets[SetIndex].EndTime;


        int NumPer = GetNumPeriods(Start, End);
        int x = GetX(sModule.Group[GroupIndex].Sets[SetIndex].Day);
        for (int i = 0; i < NumPer; i++)
        {
            int y = GetY(sModule.Group[GroupIndex].Sets[SetIndex].StartTime.AddHours(i));
            if (sTable[x, y].sTable != (sModule.Name + " (" + sModule.Group[GroupIndex].Sets[SetIndex].type[0] + ")" + "#" + sModule.Group[GroupIndex].Sets[SetIndex].venue) && (sTable[x, y].sTable != null))//check for clashes
            {
                //if (KryptonMessageBox.Show(sModule.Name + "G" + (GroupIndex + 1).ToString() + "(" + sModule.Group[GroupIndex].Sets[SetIndex].type + ") Clashes on " + sModule.Group[GroupIndex].Sets[i].Day + " " + TimeHead[y] +
                //                " with\n" + sTable[x, y].sTable + ". \nWould you like to replace it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                //{
                //    #region replace clash item
                //    if (sTable[x, y].EventType != "personal")
                //    {
                //        Deselect(x, y);//need to first deselect the other flag before selecting the new flag
                //    }
                //    string sline = sModule.Name;
                //    //Program.frmMainInstance.gb.TimeTable.sTable[x, y].EventType = "tuks";
                //    sTable[x, y].sTable = sline + " (" + sModule.Group[GroupIndex].Sets[SetIndex].type[0] + ")" + "#" + sModule.Group[GroupIndex].Sets[SetIndex].venue;
                //    sTable[x, y].sCode = sline;
                //    sTable[x, y].sType = sModule.Group[GroupIndex].Sets[SetIndex].type[0].ToString();
                //    sTable[x, y].BkColour = sModule.BkColour;
                //    sTable[x, y].FgColour = sModule.FgColour;
                //    sTable[x, y].TextFont = sModule.TextFont;
                //    sTable[x, y].iModule = sModule;
                //    sTable[x, y].iGroupIndex = GroupIndex;
                //    sTable[x, y].iSetIndex = SetIndex;
                //    sModule.Group[GroupIndex].Sets[SetIndex].ItemFlag = true;
                //    #endregion

                //}
                //else
                //{
                //    #region remove
                //    sModule.Group[GroupIndex].Sets[SetIndex].ItemFlag = false;
                //    #endregion
                //}
            }
            else
            {
                #region add without clash
                //if there is no clash then just add the item
                string sline = sModule.Name;
                inTable.sTable[x, y].EventType = "tuks"; //*2
                sTable[x, y].sTable = sline + " (" + sModule.Group[GroupIndex].Sets[SetIndex].type[0] + ")" + "#" + sModule.Group[GroupIndex].Sets[SetIndex].venue;
                sTable[x, y].sCode = sline;
                sTable[x, y].sType = sModule.Group[GroupIndex].Sets[SetIndex].type[0].ToString();
                sTable[x, y].BkColour = sModule.BkColour;
                sTable[x, y].FgColour = sModule.FgColour;
                sTable[x, y].TextFont = sModule.TextFont;
                sTable[x, y].iModule = sModule;
                sTable[x, y].iGroupIndex = GroupIndex;
                sTable[x, y].iSetIndex = SetIndex;
                sModule.Group[GroupIndex].Sets[SetIndex].ItemFlag = true;
                #endregion
            }
        }
        sModule.UpdateGroupCheck();

    }


    /// <summary>
    /// this removes the flag in the user modules to show that this item is not active anymore
    /// </summary>
    /// <param name="x">column</param>
    /// <param name="y">row</param>
    public void Deselect(int x, int y)
    {
        //since now that the flag is a property that sets the flag directly then there is no need for that long set of code
        try
        {
            sTable[x, y].Flag = false;
        }
        catch { }
        DelItem(x, y);
    }

    /// <summary>
    /// this is to delete a certain module from the time table
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void DelItem(int x, int y)
    {
        sCell Empty = new sCell();
        sTable[x, y] = Empty;
        InitialiseCell(x, y);
    }

    /// <summary>
    /// totally removes all items on the time table regarding a certain module
    /// </summary>
    /// <param name="cCode">module code</param>
    public void DelModule(string cCode)
    {
        for (int x = 0; x < xLen; x++)
        {
            for (int y = 0; y < yLen; y++)
            {

                if (sTable[x, y].sCode == cCode)
                {
                    sTable[x, y].Flag = false;
                    DelItem(x, y);
                }
            }
        }
    }


    /// <summary>
    /// writing the table, modules and their properties to a file.
    /// </summary>
    /// <param name="FilePathAndName">text file name</param>
    public void WriteFile(string FilePathAndName)
    {
        FileInfo fi = new FileInfo(@FilePathAndName);

        using (StreamWriter writer = new StreamWriter(fi.Open(FileMode.Create)))
        {
            //  writer.WriteLine(Program.frmMainInstance.gb.TableName);
            var cvt = new FontConverter();
            //for (int i = 0; i < Program.frmMainInstance.gb.UserModules.Length; i++)
            //{
            //    // Modules Current = Program.frmMainInstance.gb.UserModules[i];
            //    //for (int j = 0; j < Current.SelectedGroupIndex.Length; j++)
            //    {
            //        string sline = "";
            //        ColorConverter clv = new ColorConverter();

            //        //    string BKCOLOUR = clv.ConvertToString(Current.BkColour);
            //        //   string FGCOLOUR = clv.ConvertToString(Current.FgColour);
            //        //    sline += Current.Name + '~' +
            //        //   BKCOLOUR + '~' + FGCOLOUR + '~' + cvt.ConvertToString(Current.TextFont);//Current.TextFont;

            //        //writer.WriteLine(sline);
            //        //for (int j = 0; j < Program.frmMainInstance.gb.UserModules[i].Group.Length; j++)
            //        //{
            //        //    if (Program.frmMainInstance.gb.UserModules[i].Group[j].GroupFlag == true)
            //        //    {
            //        //        for (int k = 0; k < Program.frmMainInstance.gb.UserModules[i].Group[j].Sets.Length; k++)
            //        //        {

            //        //            writer.WriteLine(j.ToString() + "+" + k.ToString());
            //        //        }
            //        //    }
            //        //}
            //        writer.WriteLine("null");
            //    }
            //}

            //write table to file
            //for (int j = 0; j < xLen; j++)
            //{
            //    string sline = "";
            //    for (int k = 0; k < yLen; k++)
            //    {
            //        sline += sTable[j, k].sTable + ",";

            //    }
            //    writer.WriteLine(sline);
            //}

            //writer.WriteLine("");
            //write modules to file

            writer.Close();
        }

    }


}


