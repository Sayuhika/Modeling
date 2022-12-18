using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Argon_IdealGasModel
{
    internal class OutputDataCollector : IDisposable
    {
        private StreamWriter sw;
        static string defaultPath = Environment.CurrentDirectory + "\\Data\\50k_statsData.csv";
        public OutputDataCollector(string path = "")
        {
            if (path == "") { path = defaultPath; }
            sw = new StreamWriter(path, true, Encoding.Default);
        }

        public void WriteStats(int N,double T, double T_c, double P_v, double P_i)
        {
            sw.WriteLine($"{N};{T};{T_c};{P_v};{P_i};");
        }

        public void WriteSeparator() => sw.WriteLine();
        public void WriteHeader() => sw.WriteLine($"N;T;T_c;P_v;P_i;");

        public void Dispose()
        {
            sw.Close();
        }
    }
}
