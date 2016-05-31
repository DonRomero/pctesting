using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pctesting
{
    class GetFrames
    {
        static public List<GeneralFrame> DivideOnGeneralFrame(string UserName)
        {
            List < GeneralFrame > GenFrame= new List<GeneralFrame>();
            List<UserInfoFrame> Frame=GetFrames.DivideOnFrame(UserName);
            double GeneralInternetTime = 0;
            double GeneralFileTime = 0;
            foreach(UserInfoFrame UI in Frame)
            {

                if (UI.TypeActivity == "Использовал интернет")
                {
                    GeneralInternetTime += Convert.ToInt64(UI.GeneralTime);
                }
                else
                    GeneralFileTime += Convert.ToInt64(UI.GeneralTime);        
            }
            string IntPercent;
            string FilePercent;
            if(GeneralInternetTime==0)
            {
                IntPercent = "0%";
            }
            else
            {
            IntPercent = Math.Round((GeneralInternetTime / (GeneralInternetTime + GeneralFileTime) * 100),4).ToString() + "%";
            }
            if(GeneralFileTime==0)
            {
                FilePercent = "0%";
            }
            else FilePercent= Math.Round((GeneralFileTime / (GeneralInternetTime + GeneralFileTime) * 100),4).ToString() + "%";
            GenFrame.Add(new GeneralFrame(UserName, "Использование интернета", IntPercent));
            GenFrame.Add(new GeneralFrame(UserName, "Работа с файлами", FilePercent));
            return GenFrame;
        }
        static public List<UserInfoFrame> DivideOnFrame(string UserName)
        {
            List<UserInfoFrame> frames = new List<UserInfoFrame>();
            DBService.DataServiceClient client = new DBService.DataServiceClient();
            string[][] InternetData = client.FindInternetActivity(UserName);
            string[][] FileData=client.FindFileActivity(UserName);
            foreach(string [] s in InternetData)
            {
                frames.Add(new UserInfoFrame(s));
            }
            foreach (string[] s in FileData)
            {
                frames.Add(new UserInfoFrame(s));
            }
            return frames;
        }
    }
    class UserInfoFrame
    {
        string User;
        string Activity;
        string GenTime;
        string begTime;
        string endTime;
        public UserInfoFrame(string[] frameData)
        {
            User = frameData[0];
            Activity = frameData[1];
            GenTime = frameData[2];
            begTime = frameData[3];
            endTime = frameData[4];
        }
        public string UserName
        {
            get { return User; }
            set { User = value; }
        }
        public string TypeActivity
        {
            get { return Activity; }
            set { Activity = value; }
        }
        public string GeneralTime
        {
            get { return GenTime; }
            set { GenTime = value; }
        }
        public string BeginTime
        {
            get { return begTime; }
            set { begTime = value; }
        }
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
    }
}
