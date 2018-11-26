using System;
using System.Drawing;
using System.Text.RegularExpressions;//used for regex


    [Serializable]
    public class Modules:EventBase
    {
        /// <summary>
        /// this procedure will check through the array to see if the item 
        /// is actually in existence within the array or not
        /// </summary>
        /// <param name="Sline"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        static bool CheckArrayItem(string Sline, string[] List)
        {
            bool flag = false;
            foreach (string value in List)
            {
                if (value == Sline)
                {
                    flag = true;
                    break;
                }
            }
            return flag;

        }

        //static string[] Times = { "07:30", "08:30", "09:30", "10:30", "11:30", "12:30", "13:30", "14:30", "15:30", "16:30", "17:30", "18:30", "19:30" };
        static string[] types = { "lectures", "discussions", "practicals", "tutorials", "seminars" };
        static string[] DaysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        static string[] Languages = { "Afrikaans", "English", "Both" };

        [Serializable]
        public struct LectureSet
        {

            public LectureSin[] Sets;
            //---------------------------------//
            public void AddItem()//somehow the methods that are made within the structs have no interference with the ones out side
            {
                try
                { Array.Resize(ref Sets, Sets.Length + 1); }
                catch
                { Sets = new LectureSin[1]; };
            }

            public void DelItem(int Index)
            {
                for (int i = Index; i < Sets.Length - 1; i++)
                {
                    Sets[i] = Sets[i + 1]; //moving all the items one space up
                }

                Array.Resize(ref Sets, Sets.Length - 1);//finally resizing it one index lower
            }
            //---------------------------------//
            [Serializable]
            public struct LectureSin//each item in the group
            {
                //----------------------------------------//
                public bool ItemFlag { get; set; }
                private string _Language;
                public string Language
                {
                    get { return _Language; }
                    set
                    {
                        if (CheckArrayItem(value, Languages) == true)
                        { _Language = value; }
                    }
                }
                //----------------------------------------//
                public string venue { get; set; }
                //----------------------------------------//
                private DateTime _StartTime;
                public DateTime StartTime
                {
                    get { return _StartTime; }
                    set
                    {
                        //if (CheckArrayItem(value, Times) == true)
                        //{ _StartTime = value; }//this will make sure it is a valid time to be entered
                        _StartTime = value;
                    }
                }
                //----------------------------------------//
                private DateTime _EndTime;
                public DateTime EndTime
                {
                    get { return _EndTime; }
                    set
                    {
                        //if (CheckArrayItem(value, Times) == true)
                        //{ _EndTime = value; }//this will make sure it is a valid time to be entered
                        _EndTime = value;
                    }
                }
                //----------------------------------------//
                private string _type;
                public string type
                {
                    get { return _type; }
                    set { _type = value; }

                }
                //----------------------------//
                private string _Day;
                public string Day
                {
                    get { return _Day; }
                    set
                    {
                        if (CheckArrayItem(value, DaysOfWeek) == true)
                        {
                            _Day = value;
                        }
                    }
                }

                private int _Year;
                public string Year
                {
                    get
                    {
                        return _Year.ToString();
                    }
                    set
                    {
                        if (Regex.IsMatch(value, "[0-9]"))
                        {
                            //add to modules
                            _Year = Convert.ToInt32(value);
                        }
                    }
                }

                private string _PeriodOfPres;
                public string PeriodOfPres
                {
                    get { return _PeriodOfPres; }
                    set { _PeriodOfPres = value; }
                }

                private string _Others;
                public string Others
                {
                    get { return _Others; }
                    set { _Others = value; }
                }
            }
            //---------------------------------//


            public bool GroupFlag { get; set; }
        }

        public LectureSet[] Group = new LectureSet[0];

        public void AddItem()
        {
            Array.Resize(ref Group, Group.Length + 1);
            //Group[Group.Length - 1] = value;  //the use of group.length -1 is to get the last item
        }

        public void DelItem(int Index)
        {
            for (int i = Index; i < Group.Length - 1; i++)
            {
                Group[i] = Group[i + 1]; //moving all the items one space up
            }

            Array.Resize(ref Group, Group.Length - 1);//finally resizing it one index lower
        }

        public void UpdateGroupCheck()
        {
            //through all the sets to see if there is anything that needs to be added to the group flags
            for (int i = 0; i < Group.Length; i++)
            {
                Group[i].GroupFlag = false;
                if (Group[i].Sets != null)
                    for (int j = 0; j < Group[i].Sets.Length; j++)
                    {
                        if (Group[i].Sets[j].ItemFlag == true)
                        {
                            Group[i].GroupFlag = true;
                            break;
                        }
                    }
            }
        }

        //public string Name { get; set; }

        public int SelectedGroupIndex { get; set; }


    }

