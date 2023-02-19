using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRTFixer
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string original = "02:20:26,009 --> 02:20:27,886";
            string fixedTime = FixTime(2.55, original);
            Console.WriteLine(original);
            Console.WriteLine(fixedTime);

            string path = ConfigurationManager.AppSettings["SourcePath"];
            List<string> file = File.ReadAllLines(path).ToList();
            //file.ForEach(f => Console.WriteLine(f));
            double sec = double.Parse(ConfigurationManager.AppSettings["Seconds"], CultureInfo.InvariantCulture);

            List<string> fileUpdated = new List<string>();

            file.ForEach(f => fileUpdated.Add(FixTime(sec, f)));


            string targetPath = ConfigurationManager.AppSettings["TargetPath"];

            File.WriteAllLines(targetPath, fileUpdated.ToArray());

        }
        private static string FixTime(double seconds, string line)
        {
            string[] parts = line.Split(' ');
            if (parts.Length == 1 || parts[1] != "-->")
                return line;

            IFormatProvider fo;
            TimeSpan pardes = TimeSpan.Parse(parts[0]);

            string s = seconds.ToString("0.000", CultureInfo.InvariantCulture);
            string[] partss = s.Split('.');
            int seconds2 = int.Parse(partss[0]);
            int miliconds2 = int.Parse(partss[1]);
            bool add = seconds >= 0 ? true : false;

            TimeSpan newDatetime1;
            TimeSpan newDatetime2;
            if (add)
            {
                newDatetime1 = TimeSpan.Parse(parts[0]) + new TimeSpan(0, 0, 0, Math.Abs(seconds2), miliconds2);
                newDatetime2 = TimeSpan.Parse(parts[2]) + new TimeSpan(0, 0, 0, Math.Abs(seconds2), miliconds2);
            }
            else
            {
                newDatetime1 = TimeSpan.Parse(parts[0]) - new TimeSpan(0, 0, 0, Math.Abs(seconds2), miliconds2);
                newDatetime2 = TimeSpan.Parse(parts[2]) - new TimeSpan(0, 0, 0, Math.Abs(seconds2), miliconds2);
            }

            return $"{newDatetime1.ToString(@"hh\:mm\:ss\,fff")} --> {newDatetime2.ToString(@"hh\:mm\:ss\,fff")}";
        }
    }
}
