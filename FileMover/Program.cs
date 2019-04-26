//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Topshelf;

//namespace FileService
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string path = string.Empty;
//            int fileAmount = 10;


//            HostFactory.New(x =>
//            {

//                x.RunAsPrompt();
//                // x.RunAsLocalSystem();


//                x.EnablePauseAndContinue();
//                x.EnableShutdown();

//                x.SetServiceName("DirCheck");
//                x.SetDisplayName("DirectoryCheck");
//                x.SetInstanceName("DirCheck");
//                x.SetDescription("Check directory and move files if amount >100");

//                x.Service<Check>(sc =>
//                {
//                    sc.ConstructUsing(() => new Check());

//                    // the start and stop methods for the service
//                    sc.WhenStarted(s => s.Start());
//                    sc.WhenStopped(s => s.Stop());

//                    //// optional pause/continue methods if used
//                    //sc.WhenPaused(s => s.Pause());
//                    //sc.WhenContinued(s => s.Continue());

//                    //// optional, when shutdown is supported
//                    //sc.WhenShutdown(s => s.Shutdown());

//                });

//                x.StartAutomatically(); // Start the service automatically
//                x.StartAutomaticallyDelayed(); // Automatic (Delayed) -- only available on .NET 4.0 or later
//                //x.StartManually(); // Start the service manually
//                //x.Disabled(); // install the service as disabled

//                x.OnException(ex =>
//                {
//                    // Do something with the exception
//                        // Log it
//                });

//                x.AddCommandLineDefinition("path", v => path = v);
//                x.AddCommandLineDefinition("fileAmount", v => fileAmount = Int32.Parse(v));


//                x.EnableServiceRecovery(r =>
//                {
//                    //you can have up to three of these
//                    r.RestartComputer(5, "message");
//                    r.RestartService(0);
//                    //the last one will act for all subsequent failures
//                    r.RunProgram(1, "notepad.exe"); // run a program
//                    //should this be true for crashed or non-zero exits
//                    r.OnCrashOnly();

//                    //number of days until the error count resets
//                    r.SetResetPeriod(1);
//                });

//            });
//        }
//    }

//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace DirCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<Check>(s =>
                {
                    s.ConstructUsing(check => new Check());
                    s.WhenStarted(check => check.Start());
                    s.WhenStopped(check => check.Stop());
                });

                x.RunAsLocalSystem();
                x.SetServiceName("DirCheck");
                x.SetDisplayName("Directory Check");
                x.SetDescription("Check directory and move files");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
