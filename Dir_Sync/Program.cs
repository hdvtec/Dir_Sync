using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;


namespace Dir_Sync
{
    internal class Program
    {
        /// <summary>
        /// Main Program
        /// </summary>
        /// <param name="args">
        /// [0] - Origin Directory
        /// [1] - Destination Directory
        /// [2] - Log File
        /// [3] - Period to repeat (seconds)
        /// </param>
        static void Main(string[] args)
        {
            //Run Peridically
            //Exception Test/Handle

            try
            {
                //--- Check If Log Can Be created
                string logPath = Path.GetDirectoryName(args[2]);
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                    if (!Directory.Exists(logPath))
                    {
                        Console.WriteLine("Unable to create log file path.");
                        Console.WriteLine("Press enter to close...");
                        Console.ReadLine();
                    }
                }

                //Periodically run should be done in Windows Task Schedule
                while (true)
                {
                    var result1 = GetContent(args[0]);
                    var result2 = GetContent(args[1]);

                    var dirsToRemove = result2.dirs.Where(x => !result1.dirs.Contains(x));
                    var dirsToAdd = result1.dirs.Where(x => !result2.dirs.Contains(x));
                    var filesToRemove = result2.files.Where(x => !result1.files.Contains(x));
                    var filesToAdd = result1.files.Where(x => !result2.files.Contains(x));
                    var filesToCompare = result1.files.Where(x => result2.files.Contains(x));

                    foreach (var fileToRemove in filesToRemove)
                    {
                        Console.WriteLine(DateTime.Now.ToString() +  " - Deleting : " + Path.Combine(args[1], fileToRemove.file));
                        using (StreamWriter sw = new StreamWriter(args[2], true))
                            sw.WriteLine(DateTime.Now.ToString() + " - Deleting : " + Path.Combine(args[1], fileToRemove.file));
                        File.Delete(Path.Combine(args[1], fileToRemove.file));
                    }

                    foreach (var dirToRemove in dirsToRemove)
                    {
                        Console.WriteLine(DateTime.Now.ToString() + " - Deleting : " + Path.Combine(args[1], dirToRemove));
                        using (StreamWriter sw = new StreamWriter(args[2], true))
                            sw.WriteLine(DateTime.Now.ToString() + " - Deleting : " + Path.Combine(args[1], dirToRemove));
                        Directory.Delete(Path.Combine(args[1], dirToRemove));
                    }

                    foreach (var dirToAdd in dirsToAdd)
                    {
                        Console.WriteLine(DateTime.Now.ToString() + " - Adding : " + Path.Combine(args[1], dirToAdd));
                        using (StreamWriter sw = new StreamWriter(args[2], true))
                            sw.WriteLine(DateTime.Now.ToString() + " - Adding : " + Path.Combine(args[1], dirToAdd));
                        Directory.CreateDirectory(Path.Combine(args[1], dirToAdd));
                    }

                    foreach (var fileToAdd in filesToAdd)
                    {
                        Console.WriteLine(DateTime.Now.ToString() + " - Adding : " + Path.Combine(args[1], fileToAdd.file));
                        using (StreamWriter sw = new StreamWriter(args[2], true))
                            sw.WriteLine(DateTime.Now.ToString() + " - Adding : " + Path.Combine(args[1], fileToAdd.file));
                        File.Copy(Path.Combine(args[0], fileToAdd.file), Path.Combine(args[1], fileToAdd.file));
                    }

                    System.Threading.Thread.Sleep(Convert.ToInt32(args[3]) * 1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
            
        }

        public static (HashSet<string> dirs, HashSet<(string file, long tick)> files) GetContent(string dir)
        {
            string CleanPath(string path) => path.Substring(dir.Length).TrimStart('/', '\\');

            // replace this with a hashing method if you need
            long GetUnique(string path) => new FileInfo(path).LastWriteTime.Ticks;

            var directories = Directory.GetDirectories(dir, "*.*", SearchOption.AllDirectories);
            directories = AddElementAtBegin(dir, directories);

            var dirHash = directories.Select(CleanPath).ToHashSet();

            // this could be paralleled if need be (if using a hash) 
            var fileHash = directories.SelectMany(Directory.EnumerateFiles)
               .Select(file => (name: CleanPath(file), ticks: GetUnique(file)))
               .ToHashSet();

            return (dirHash, fileHash);
        }

        private static string[] AddElementAtBegin(string dir, string[] directories)
        {
            int newLength = directories.Length + 1;

            string[] result = new string[newLength];

            result[0] = dir;
            
            for (int i = 0; i < directories.Length; i++)
                result[i+1] = directories[i];

            return result;
        }
    }
}
