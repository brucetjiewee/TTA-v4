using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClassList
/// </summary>
public class Module 
{
    public Module()
    {
       


    }
    private List<Lecture> mLectures = new List<Lecture>();
    public List<Lecture> Lectures { get {return mLectures; } set {mLectures = value; } }
}