using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataService" in both code and config file together.
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        void saveFileDataToDB(string name, string path, int time, string type, int compID, int userID);

        [OperationContract]
        void saveTrafficDataToDB(string URL, int time, int compID, int userID);

        [OperationContract]
        string login(string name, string password, string compName);

        [OperationContract]
        void SaveActivityToDB(DateTime GeneralTime, DateTime ActivityTime, DateTime NotActivityTime, int compID, int userID);

        [OperationContract]
        void SaveProcessesToDB(string Name, DateTime StartTime, DateTime ExitTime, DateTime GeneralTime, int compID, int userID);
    }
}
