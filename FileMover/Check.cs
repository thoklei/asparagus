using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace DirCheck
{
    class Check
    {
        private FileSystemWatcher watcher;
        private string destinationFolder = @"C:\Users\Richard\Desktop\Folder1";
        private string sourceFolder = @"C:\Users\Richard\Desktop\Folder2";

        public Check()
        {
            watcher = new FileSystemWatcher(sourceFolder);
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            //watcher.NotifyFilter = NotifyFilters.LastWrite;;

        }

        public void Start()
        {
            watcher.Created += OnChanged;
            watcher.Changed += OnChanged;

            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
        }



        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            foreach (string file in Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories))
            {
                Console.WriteLine(file +"  >  " + Path.Combine(destinationFolder , Path.GetFileName(file)) );
                File.Move(file, Path.Combine(destinationFolder, Path.GetFileName(file)));
            }
        
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }
    }
}