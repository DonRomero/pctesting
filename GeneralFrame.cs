using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pctesting
{
    class GeneralFrame
    {
        string Username;
        string Typeactivity;
        string percent;
        public GeneralFrame(string Username, string Typeactivity, string percent)
        {
            this.Username = Username;
            this.Typeactivity = Typeactivity;
            this.percent = percent;
        }
        public string UserName
        {
            get { return Username; }
            set { Username = value; }
        }
        public string TypeActivity
        {
            get { return Typeactivity; }
            set { Typeactivity = value; }
        }
        public string Percent
        {
            get { return percent; }
            set { percent = value; }
        }
    }
}
