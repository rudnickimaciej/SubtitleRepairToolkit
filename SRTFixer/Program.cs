using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SRTFixer
{
    internal class Program
    {
        private const string _sourcePathParam = "SourcePath";
        private const string _targetPathParam = "TargetPath";
        private const string _timeParam = "Time";
        static void Main()
        {
            string sourcePath = ConfigurationManager.AppSettings[_sourcePathParam];
            string targetPath = ConfigurationManager.AppSettings[_targetPathParam];
            double time = double.Parse(ConfigurationManager.AppSettings[_timeParam], CultureInfo.InvariantCulture);

            List<string> file = File.ReadAllLines(sourcePath).ToList();

            List<string> fileUpdated = new List<string>();

            file.ForEach(f => fileUpdated.Add(FixTime(time, f)));

            File.WriteAllLines(targetPath, fileUpdated.ToArray());
        }
        private static string FixTime(double seconds, string line)
        {
            string[] parts = line.Split(' ');
            if (parts.Length == 1 || parts[1] != "-->")
                return line;

            string s = seconds.ToString("0.000", CultureInfo.InvariantCulture);
            string[] partss = s.Split('.');
            int secondsParsed = int.Parse(partss[0]);
            int milisecondsParsed = int.Parse(partss[1]);
            bool add = seconds >= 0 ? true : false;

            TimeSpan newDatetime1;
            TimeSpan newDatetime2;
            if (add)
            {
                newDatetime1 = TimeSpan.Parse(parts[0]) + new TimeSpan(0, 0, 0, Math.Abs(secondsParsed), milisecondsParsed);
                newDatetime2 = TimeSpan.Parse(parts[2]) + new TimeSpan(0, 0, 0, Math.Abs(secondsParsed), milisecondsParsed);
            }
            else
            {
                newDatetime1 = TimeSpan.Parse(parts[0]) - new TimeSpan(0, 0, 0, Math.Abs(secondsParsed), milisecondsParsed);
                newDatetime2 = TimeSpan.Parse(parts[2]) - new TimeSpan(0, 0, 0, Math.Abs(secondsParsed), milisecondsParsed);
            }

            return $"{newDatetime1.ToString(@"hh\:mm\:ss\,fff")} --> {newDatetime2.ToString(@"hh\:mm\:ss\,fff")}";
        }
    }
}
