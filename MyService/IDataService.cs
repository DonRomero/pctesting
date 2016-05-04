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
        void saveFileDataToDB(string name, string path, long time, string type, string comp, string user);

        [OperationContract]
        void saveTrafficDataToDB(string URL, long time, string comp, string user);

        [OperationContract]
        string login(string name, string password, string compMAC, string compName);

        [OperationContract]
        void SaveActivityToDB(DateTime GeneralTime, DateTime ActivityTime, DateTime NotActivityTime, string comp, string user);

        [OperationContract]
        void SaveProcessesToDB(string Name, DateTime StartTime, DateTime ExitTime, TimeSpan GeneralTime, string comp, string user);

        [OperationContract]
        bool makeReport();

        [OperationContract]
        bool addUser(string name, string password);

        [OperationContract]
        List<string> getUsers();
    }
}
