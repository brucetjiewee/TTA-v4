using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


    [Serializable]
    public class EventBase
    {
        public EventBase()
        {

        }

        public string Name { get; set; }
        public string Where { get; set; }
        //application prefereses
        public Color BkColour { get; set; }
        public Color FgColour { get; set; }
        public Font TextFont { get; set; }
    }

    public class PersonalEvent:EventBase
    {
        public string Description {get; set;}
        public int length { get; set; }
    }

