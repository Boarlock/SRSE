using System;
using System.Diagnostics;
using System.IO;
using EasyHook;

namespace SRSE
{
    class Program
    {
        static void Main(string[] args)
        {
            string gameDir = AppDomain.CurrentDomain.BaseDirectory;

            string exePath = Path.Combine(
                    gameDir,
                    "Sins of a Solar Empire Rebellion.exe"
                );

            string dllPath = Path.Combine(
                gameDir,
                "SRSE.dll"
            );

            Console.WriteLine("Starting Sins...");

            Process sins = Process.Start(exePath);

            if (sins == null)
            {
                Console.WriteLine("Failed to start Sins.");
                return;
            }

            Console.WriteLine("Sins PID: " + sins.Id);

            // Give the process time to initialize.
            System.Threading.Thread.Sleep(5000);

            Console.WriteLine("Injecting SRSE...");

            try
            {
                RemoteHooking.Inject(
                    sins.Id,
                    InjectionOptions.Default,
                    dllPath,
                    dllPath
                );

                Console.WriteLine("Injection succeeded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Injection failed:");
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
