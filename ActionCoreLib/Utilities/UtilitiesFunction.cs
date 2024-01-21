using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ActionCoreLib.Utilities
{
    public class UtilitiesFunction
    {
        public static long GetUnixTimestampInSeconds()
        {
            DateTime now = DateTime.UtcNow;
            DateTime unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = now - unixEpoch;
            long timestamp = (long)timeSpan.TotalSeconds;

            return timestamp;
        }

        public static void BackupDatabase(string path)
        {
            string backupPath = path + ".bak";
            if (System.IO.File.Exists(path))
            {
           
                System.IO.File.Copy(path, backupPath, true);
            }
        }
        public static void RestoreDatabase(string path)
        {
            string backupPath = path + ".bak";
            if (System.IO.File.Exists(backupPath))
            {
                System.IO.File.Copy(backupPath, path, true);
            }
        }
    }
}
