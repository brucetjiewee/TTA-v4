using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserInputs
/// </summary>
public class UserInputs
{
    public UserInputs(List<string> modules, List<string> campuses,string time,string language)
    {
        mModules = modules;
        mCampuses = campuses;
        mTime = time;
        mLanguage = language;
    }

    private List<string> mModules;
    private List<string> mCampuses;
    private string mTime;
    private string mLanguage;

    public List<string> Modules
    {
        get { return mModules; }
        set { mModules = value; }
    }

    public List<string> Campuses
    {
        get { return mCampuses; }
        set { mCampuses = value; }
    }

    public string Time
    {
        get { return mTime; }
        set { mTime = value; }
    }

    public string Language
    {
        get { return mLanguage; }
        set { mLanguage = value; }
    }
}
   