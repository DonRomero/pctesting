using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace MyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataService" in both code and config file together.
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        void saveFileDataToDB(string name, string path, string ext, long time, string type, string comp, string user);

        [OperationContract]
        void saveTrafficDataToDB(string URL, long time, string comp, string user);

        [OperationContract]
        string login(string name, string password, string compMAC, string compName);

        [OperationContract]
        void SaveActivityToDB(TimeSpan AllTime, TimeSpan ActivityTime, TimeSpan NotActivityTime, string comp, string user);

        [OperationContract]
        void SaveProcessesToDB(string Name, DateTime StartTime, DateTime ExitTime, TimeSpan GeneralTime, string comp, string user);

        [OperationContract]
        bool makeReport();

        [OperationContract]
        bool addUser(string name, string password);

        [OperationContract]
        List<string> getUsers();

        [OperationContract]
        void SaveTestCharacteristic(DateTime time, string RAM, string freeRAM, string CPU, string VideoRAM, string comp, string user);
    }
}
