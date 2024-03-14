using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observability.ConsoleApp
{
    internal class ServiceTwo
    {
        internal async Task<int> WriteToFile(string text)
        {
            using var activity = ActivitySourceProvider.Source.StartActivity();

            await File.WriteAllTextAsync("myfile.txt", text);

            return (await File.ReadAllTextAsync("myFile.txt")).Length;

        }
    }
}
