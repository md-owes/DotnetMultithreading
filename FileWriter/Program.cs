using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileWriter
{
    internal class Program
    {
        static Mutex mutex = new Mutex(false, "FileMutex");
        static void Main(string[] args)
        {
            // Wait for Process B to acquire the mutex
            mutex.WaitOne();

            try
            {
                // Write to the shared file
                using (StreamWriter writer = new StreamWriter("..\\..\\..\\shared.txt", true))
                {
                    Thread.Sleep(15000);
                    writer.WriteLine($"\nHello from Process FileWriter => {Process.GetCurrentProcess().Id}");
                }
            }
            finally
            {
                // Release the mutex
                mutex.ReleaseMutex();
            }

            Console.WriteLine("FileWriter has written to the file.");
            Console.ReadLine();
        }
    }
}
