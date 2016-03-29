using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLib
{
    /// <summary>
    /// Utility for logging events from a program to a text file.
    /// </summary>
    public class TextLogger
    {
        //List of entries to the log
        private List<String> Entries = new List<String>();
        //path to a directory within the project folder
        private const String path = @"Log.txt";
        /// <summary>
        /// Adds the provided entry to the Entries list
        /// </summary>
        /// <param name="entry">Entry to be added</param>
        public void AddEntry(String entry)
        {
            String Now = DateTime.Now.ToString();
            Entries.Add(Now + " - " + entry);
        }
        /// <summary>
        /// Writes the list of entries to a text file at path
        /// </summary>
        public void WriteEntriesToFile()
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path, true))
            {
                foreach(String entry in Entries)
                {
                    file.WriteLine(entry);
                }
            }
        }


    }
}
