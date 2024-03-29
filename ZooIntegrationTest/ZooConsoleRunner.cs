using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ZooIntegrationTest
{
    public class ZooConsoleRunner
    {
        private Process ZooConsoleApp { get; set; }

        public void StartZooConsoleApp()
        {
            var path = Path.Combine("..", "..", "..", "..", "Zoo");
            path = Path.GetFullPath(path);
            Console.WriteLine(path);

            var psi = new ProcessStartInfo
            {
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                Arguments = $"run -p {path}",
                FileName = "/usr/local/share/dotnet/dotnet"
            };

            ZooConsoleApp = Process.Start(psi);
        }

        public void KillZooConsoleApp()
        {
            if (!ZooConsoleApp.HasExited)
            {
                ZooConsoleApp.CloseMainWindow();
            }
        }

        public async Task<string> ReadLineAsync()
        {
            return await ZooConsoleApp.StandardOutput.ReadLineAsync();
        }

        public async Task WriteLineAsync(string lineToWrite)
        {
            await ZooConsoleApp.StandardInput.WriteLineAsync(lineToWrite);
        }
    }
}