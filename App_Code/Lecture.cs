using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModuleBlueprint
/// </summary>
public class Lecture
{
    private string mCampus;
    private int mYear;
    private string mModCode;
    private string mGroup;
    private string mLang;
    private string mType;
    private string mTimePeriod;
    private string mDay;
    private string mStartTime;
    private string mEndTime;
    private string mVenue;

    public string Campus
    {
        get {return mCampus; }
        set { mCampus = value; }
    }

    public int Year
    {
        get { return mYear; }
        set { mYear = value; }
    }

    public string ModCode
    {
        get { return mModCode; }
        set { mModCode = value; }
    }

    public string Group
    {
        get { return mGroup; }
        set { mGroup = value; }
    }

    public string Lang
    {
        get { return mLang; }
        set { mLang = value; }
    }

    public string Type
    {
        get { return mType; }
        set { mType = value; }
    }

    public string TimePeriod
    {
        get { return mTimePeriod; }
        set { mTimePeriod = value; }
    }

    public string Day
    {
        get { return mDay; }
        set { mDay = value; }
    }

    public string StartTime
    {
        get { return mStartTime; }
        set { mStartTime = value; }
    }

    public string EndTime
    {
        get { return mEndTime; }
        set { mEndTime = value; }
    }

    public string Venue
    {
        get { return mVenue; }
        set { mVenue = value; }
    }



    public Lecture(string inputline)
    {
        string[] temp = new string[7];
        string[] group = new string[5];


        temp = inputline.Split(',');
        group = temp[1].Split('/');


        mCampus = temp[0];
        mYear = Convert.ToInt32(group[0]);
        mModCode = group[1];
        mGroup = group[2];
        mLang = group[3];
        mType = group[4];
        mTimePeriod = temp[2];
        mDay = temp[3];
        mStartTime = temp[4];
        mEndTime = temp[5];
        mVenue = temp[6];
      
    }

}