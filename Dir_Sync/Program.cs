using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;


namespace Dir_Sync
{
    internal class Program
    {
        static string _originDirectory, _destinationDirectory, _logFile;
        static int _repetionPeriod;
        /// <summary>
        /// Main Program
        /// 
        /// Behaviour may very according to permissions to access directories/files
        /// </summary>
        /// <param name="args">
        /// [0] - Origin Directory
        /// [1] - Destination Directory
        /// [2] - Log File
        /// [3] - Period to repeat (seconds)
        /// </param>
        static void Main(string[] args)
        {
            try
            {
                //--- Read arguments into variables
                _originDirectory = args[0];
                _destinationDirectory = args[1];
                _logFile = args[2];
                _repetionPeriod = Convert.ToInt32(args[3]);

                //--- Display interval of execution
                TimeSpan timeSpanRunInterval = TimeSpan.FromSeconds(_repetionPeriod);
                LogEvent("Synchronization will run every " +
                    string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                        timeSpanRunInterval.Hours,
                        timeSpanRunInterval.Minutes,
                        timeSpanRunInterval.Seconds), _logFile);

                //Periodically run should be done in Windows Task Schedule
                while (true)
                {
                    //--- Log Header
                    LogEvent("------- Synchronization started at " + DateTime.Now.ToString(), _logFile);

                    //--- Check If Log Can Be created
                    string logPath = Path.GetDirectoryName(_logFile);
                    if (!Directory.Exists(logPath))
                    {
                        Directory.CreateDirectory(logPath);
                        if (!Directory.Exists(logPath))
                        {
                            Console.WriteLine("Unable to create log file path.");
                            Console.WriteLine("Press enter to close...");
                            Console.ReadLine();
                            return;
                        }
                        else
                            LogEvent(DateTime.Now.ToString() + " - Log directory created", _logFile);
                    }

                    var result1 = GetContent(_originDirectory);
                    var result2 = GetContent(_destinationDirectory);

                    var dirsToRemove = result2.dirs.Where(x => !result1.dirs.Contains(x));
                    var dirsToAdd = result1.dirs.Where(x => !result2.dirs.Contains(x));
                    var filesToRemove = result2.files.Where(x => !result1.files.Contains(x));
                    var filesToAdd = result1.files.Where(x => !result2.files.Contains(x));
                    var filesToCompare = result1.files.Where(x => result2.files.Contains(x));

                    //--- Remove Files
                    foreach (var fileToRemove in filesToRemove)
                    {
                        try
                        {
                            LogEvent(DateTime.Now.ToString() + " - Deleting : " + Path.Combine(_destinationDirectory, fileToRemove.file), _logFile);
                            File.Delete(Path.Combine(_destinationDirectory, fileToRemove.file));
                        }
                        catch (Exception ex)
                        {
                            LogEvent(DateTime.Now.ToString() + " - " + ex.ToString(), _logFile);
                        }
                    }

                    //--- Remove Directories
                    foreach (var dirToRemove in dirsToRemove)
                    {
                        try
                        {
                            LogEvent(DateTime.Now.ToString() + " - Deleting : " + Path.Combine(_destinationDirectory, dirToRemove), _logFile);
                            Directory.Delete(Path.Combine(_destinationDirectory, dirToRemove));
                        }
                        catch (Exception ex)
                        {
                            LogEvent(DateTime.Now.ToString() + " - " + ex.ToString(), _logFile);
                        }
                    }

                    //--- Create Directories
                    foreach (var dirToAdd in dirsToAdd)
                    {
                        try
                        {
                            LogEvent(DateTime.Now.ToString() + " - Adding : " + Path.Combine(_destinationDirectory, dirToAdd), _logFile);
                            Directory.CreateDirectory(Path.Combine(_destinationDirectory, dirToAdd));
                        }
                        catch (Exception ex)
                        {
                            LogEvent(DateTime.Now.ToString() + " - " + ex.ToString(), _logFile);
                        }
                    }

                    //--- Copy files
                    foreach (var fileToAdd in filesToAdd)
                    {
                        try
                        {
                            LogEvent(DateTime.Now.ToString() + " - Adding : " + Path.Combine(_destinationDirectory, fileToAdd.file), _logFile);
                            File.Copy(Path.Combine(_originDirectory, fileToAdd.file), Path.Combine(_destinationDirectory, fileToAdd.file));
                        }
                        catch (Exception ex)
                        {
                            LogEvent(DateTime.Now.ToString() + " - " + ex.ToString(), _logFile);
                        }
                    }

                    //--- Log Footer
                    LogEvent("------- Synchronization ended at " + DateTime.Now.ToString(), _logFile);

                    System.Threading.Thread.Sleep(Convert.ToInt32(_repetionPeriod) * 1000);
                }
            }
            catch (Exception ex)
            {
                LogEvent(DateTime.Now.ToString() + " - " + ex.ToString(), _logFile);
                LogEvent("------- Synchronization ended at " + DateTime.Now.ToString(), _logFile);
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
            
        }

        /// <summary>
        /// Hash files
        /// </summary>
        /// <param name="dir"></param>
        /// <returns>
        /// Hashed directories and files list
        /// </returns>
        public static (HashSet<string> dirs, HashSet<(string file, long tick)> files) GetContent(string dir)
        {
            string CleanPath(string path) => path.Substring(dir.Length).TrimStart('/', '\\');

            long GetUnique(string path) => new FileInfo(path).LastWriteTime.Ticks;

            var directories = Directory.GetDirectories(dir, "*.*", SearchOption.AllDirectories);
            directories = AddElementAtBegin(dir, directories);

            var dirHash = directories.Select(CleanPath).ToHashSet();

            var fileHash = directories.SelectMany(Directory.EnumerateFiles)
               .Select(file => (name: CleanPath(file), ticks: GetUnique(file)))
               .ToHashSet();

            return (dirHash, fileHash);
        }

        /// <summary>
        /// Add element at the beginning of an array
        /// </summary>
        /// <param name="elementToAdd">New array element</param>
        /// <param name="originalArray">Original array</param>
        /// <returns>
        /// Array with new element at position 0
        /// </returns>
        private static string[] AddElementAtBegin(string elementToAdd, string[] originalArray)
        {
            int newLength = originalArray.Length + 1;

            string[] result = new string[newLength];

            result[0] = elementToAdd;
            
            for (int i = 0; i < originalArray.Length; i++)
                result[i+1] = originalArray[i];

            return result;
        }

        /// <summary>
        /// Log Event into Console and Log File
        /// </summary>
        /// <param name="logText">Text to log</param>
        /// <param name="logFile">Log File</param>
        private static void LogEvent(string logText, string logFile)
        {
            Console.WriteLine(logText);
            using (StreamWriter sw = new StreamWriter(logFile, true))
                sw.WriteLine(DateTime.Now.ToString() + " - " + logText);
        }
    }
}
