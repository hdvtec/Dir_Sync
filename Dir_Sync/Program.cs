using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;

namespace Dir_Sync
{
    internal class Program
    {
        static string _originDirectory, _destinationDirectory, _logFile;
        static int _repetitionPeriod;
        private static System.Timers.Timer _repetitionTimer;

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
                //Validate if user Inputs valid
                if (!ValidateUserInputs(args))
                {
                    Console.WriteLine("Press enter to close...");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("\nPress the Enter key to exit the application...\n");

                //--- Log application start time
                LogEvent(("The application started at " + DateTime.Now.ToString("HH:mm:ss")), _logFile);

                //--- Display interval of execution
                TimeSpan timeSpanRunInterval = TimeSpan.FromSeconds(_repetitionPeriod);
                LogEvent("Synchronization will run every " +
                    string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                        timeSpanRunInterval.Hours,
                        timeSpanRunInterval.Minutes,
                        timeSpanRunInterval.Seconds), _logFile);

                //--- Synchronize folders at application start
                SynchronizeFolders();

                //--- Start timer to run periodically
                SetTimer();

                Console.ReadLine();
                _repetitionTimer.Stop();
                _repetitionTimer.Dispose();

            }
            catch (Exception ex)
            {
                LogEvent(DateTime.Now.ToString() + " - " + ex.Message, _logFile);
                LogEvent("------- Synchronization ended at " + DateTime.Now.ToString(), _logFile);
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
            
        }

        /// <summary>
        /// Validate user inputs
        /// </summary>
        /// <param name="userInput">Arguments provided by the user</param>
        /// <returns>True if arguments are valid.</returns>
        private static bool ValidateUserInputs(string[] userInput)
        {
            //--- Validate parameter quantity, Repetition Period and Log File
            try
            {
                Console.WriteLine(DateTime.Now.ToString() + " - Validating number of parameters\n");

                //--- Check Number of provided arguments
                if (!(userInput.Length == 4))
                    throw new Exception("Invalid number of parameters.");

                //--- Read arguments into variables
                _originDirectory = userInput[0];
                _destinationDirectory = userInput[1];
                _logFile = userInput[2];

                //--- Validate Repetition Period
                Console.WriteLine(DateTime.Now.ToString() + " - Validating Repetition Period\n");

                if (!int.TryParse(userInput[3], out _repetitionPeriod))
                    throw new Exception("Invalid repetition period.");
                if(!(_repetitionPeriod > 0))
                    throw new Exception("Repetition period has to be higher than 0.");

                //--- Validate Log File
                Console.WriteLine(DateTime.Now.ToString() + " - Validating LogFile\n");

                if (!ValidateDirectory(_logFile))
                    throw new Exception("Invalid Log Directory");

                if(!Directory.Exists(Path.GetDirectoryName(_logFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_logFile));
                    if (!Directory.Exists(Path.GetDirectoryName(_logFile)))
                        throw new Exception("Cannot create log Directory.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Validation failed : " + ex.Message);
                return false;
            }

            //--- Validate Origin and Destination directories
            try
            {
                //--- Validate Origin directory
                LogEvent("Validating origin directory.", _logFile);

                if (!ValidateDirectory(_originDirectory))
                    throw new Exception("Invalid origin directory.");

                if (!Directory.Exists(_originDirectory))
                    throw new Exception("Origin directory not found.");

                LogEvent("Origin directory verified.", _logFile);

                //--- Validate Destination directory
                LogEvent("Validating destination directory.", _logFile);

                if (!ValidateDirectory(_destinationDirectory))
                    throw new Exception("Invalid destination directory.");

                if (!Directory.Exists(_destinationDirectory))
                {
                    Directory.CreateDirectory(_destinationDirectory);
                    if (!Directory.Exists(_destinationDirectory))
                        throw new Exception("Cannot create destination Directory.");
                }

                LogEvent("Destination directory verified.", _logFile);

            }
            catch (Exception ex)
            {
                LogEvent("Validation failed : " + ex.Message, _logFile);
                throw;
            }

            //--- If all verification pass return true
            return true;
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            _repetitionTimer = new System.Timers.Timer(_repetitionPeriod * 1000);
            // Hook up the Elapsed event for the timer. 
            _repetitionTimer.Elapsed += OnTimedEvent;
            _repetitionTimer.AutoReset = true;
            _repetitionTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            SynchronizeFolders();
        }

        /// <summary>
        /// Folder Synchronization
        /// </summary>
        private static void SynchronizeFolders()
        {
            try
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

                var originDirContents = GetContent(_originDirectory);
                var destinationDirContents = GetContent(_destinationDirectory);

                var dirsToRemove = destinationDirContents.dirs.Where(x => !originDirContents.dirs.Contains(x));
                var dirsToAdd = originDirContents.dirs.Where(x => !destinationDirContents.dirs.Contains(x));
                var filesToRemove = destinationDirContents.files.Where(x => !originDirContents.files.Contains(x));
                var filesToAdd = originDirContents.files.Where(x => !destinationDirContents.files.Contains(x));

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
            }
            catch (Exception ex)
            {
                LogEvent(DateTime.Now.ToString() + " - " + ex.ToString(), _logFile);
                LogEvent("------- Synchronization ended at " + DateTime.Now.ToString(), _logFile);
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
            string[] result = new string[originalArray.Length + 1];

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
                sw.WriteLine(logText);
        }

        /// <summary>
        /// Validate if string is a possible directory
        /// </summary>
        /// <param name="directory">String to validate</param>
        /// <returns></returns>
        private static bool ValidateDirectory (string directory)
        {
            Regex regexDirectory = new Regex(@"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$");
            return regexDirectory.IsMatch(directory);
        }
    }
}
