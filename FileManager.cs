using System;
using pctesting.DBService;
using System.IO;

namespace pctesting
{
    class FileManager
    {
        static string comp, user;
        public FileManager(string c, string u)
        {
            comp = c;
            user = u;
        }

        private void initWatcher(FileSystemWatcher watcher)
        {
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.InternalBufferSize = 10000;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        public void watch()
        {
            string system = Environment.GetFolderPath(Environment.SpecialFolder.System);
            string[] drives = Directory.GetLogicalDrives();
            string sysRoot = Path.GetPathRoot(system);
            FileSystemWatcher watcher;
            foreach (string driveStr in drives)
            {
                DriveInfo drive = new DriveInfo(driveStr);
                if (!drive.Name.Equals(sysRoot) && drive.DriveType == DriveType.Fixed && drive.IsReady)
                {
                    watcher = new FileSystemWatcher(@drive.Name, "*.*");
                    initWatcher(watcher);
                }
            }
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            watcher = new FileSystemWatcher(@desktopPath, "*.*");
            initWatcher(watcher);
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            DBService.DataServiceClient client = new DataServiceClient();
            client.saveFileDataToDB(e.Name, e.FullPath, (int)DateTime.Now.Ticks/10000, e.ChangeType.ToString(), comp, user);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            DBService.DataServiceClient client = new DataServiceClient();
            client.saveFileDataToDB(e.Name, e.FullPath, (int)DateTime.Now.Ticks/10000, e.ChangeType.ToString(), comp, user);
        }
    }
}
