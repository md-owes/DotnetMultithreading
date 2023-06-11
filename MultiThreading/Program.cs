using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultiThreading
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Basics();
            //States();
            //LockTechnique();
            //MonitorTechnique();
            //MutexTechnique();
            //SemaphoreTechnique();
            //ThreadPooling();
            //AsynchronousMethod().ConfigureAwait(false); Console.WriteLine("This will execute first if not awaited and last if awaited.");
            Tpl();
            Console.ReadKey();
        }

        #region Basics

        static void Basics()
        {
            // Create and start two threads
            Thread pehlaDhaaga = new Thread(KaamKaro);
            Thread doosraDhaaga = new Thread(KaamKaro);

            pehlaDhaaga.Start("PehlaDhaaga");
            doosraDhaaga.Start("DoosraDhaaga");

            // Wait for both threads to complete
            pehlaDhaaga.Join();
            doosraDhaaga.Join();

            Console.WriteLine("Dhaagon ka kaam ho gaya.");
            Console.ReadLine();
        }

        static void KaamKaro(object threadName)
        {
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"Thread {threadName}: Iteration {i}");
                Thread.Sleep(1000); // Simulate some work being done
            }
        }

        #endregion

        #region States

        static void States()
        {
            // Create a new thread
            Thread thread1 = new Thread(DoWork);
            thread1.Name = "Thread1";
            // Create a new thread
            Thread thread2 = new Thread(DoWork);
            thread2.Name = "Thread2";
            // Create a new thread
            Thread thread3 = new Thread(DoWork);
            thread3.Name = "Thread3";

            PrintThreadState(thread1, thread2);

            // Start the thread
            thread1.Start();
            thread2.Start();

            // Get and display the initial state of the thread
            PrintThreadState(thread1, thread2);

            // Sleep for a brief period to allow the thread to execute
            Thread.Sleep(500);

            // Get and display the updated state of the thread
            PrintThreadState(thread1, thread2);

            // Wait for the thread to complete
            thread1.Abort();

            // Get and display the final state of the thread
            PrintThreadState(thread1, thread2);

            // Wait for the thread to complete
            thread2.Join();

            // Get and display the final state of the thread
            PrintThreadState(thread1, thread2);

            // Get and display the final state of the thread
            Console.WriteLine("Program completed successfully");

            Console.ReadLine();
        }

        private static void PrintThreadState(Thread thread1, Thread thread2)
        {
            // Get and display the initial state of the thread
            Console.WriteLine($"Thread {thread1.Name} state: {thread1.ThreadState}");
            Console.WriteLine($"Thread {thread2.Name} state: {thread2.ThreadState}");
        }

        static void DoWork()
        {
            // Simulate some work being done
            Thread.Sleep(5000);
        }

        #endregion

        #region Synchronization

        #region LockTechnique

        static int sharedResource = 0;
        static object lockObject = new object();

        static void LockTechnique()
        {
            // Create two threads that increment the shared resource
            Thread thread1 = new Thread(IncrementSharedResource);
            Thread thread2 = new Thread(IncrementSharedResource);

            // Start the threads
            thread1.Start();
            thread2.Start();

            // Wait for the threads to complete
            thread1.Join();
            thread2.Join();

            // Display the final value of the shared resource
            Console.WriteLine($"Final value of the shared resource: {sharedResource}");

            Console.ReadLine();
        }

        static void IncrementSharedResource()
        {
            for (int i = 0; i < 100000; i++)
            {
                // Acquire the lock before accessing the shared resource
                lock (lockObject)
                {
                    sharedResource++;
                }
            }
        }

        #endregion

        #region MonitorTechnique

        static int sharedResourceMonitor = 0;
        static object monitorObject = new object();

        static void MonitorTechnique()
        {
            // Create two threads that increment the shared resource
            Thread thread1 = new Thread(IncrementSharedResourceMonitor);
            Thread thread2 = new Thread(IncrementSharedResourceMonitor);

            // Start the threads
            thread1.Start();
            thread2.Start();

            // Wait for the threads to complete
            thread1.Join();
            thread2.Join();

            // Display the final value of the shared resource
            Console.WriteLine($"Final value of the shared resource: {sharedResourceMonitor}");

            Console.ReadLine();
        }

        static void IncrementSharedResourceMonitor()
        {
            for (int i = 0; i < 100000; i++)
            {
                // Acquire the lock using Monitor.Enter
                Monitor.Enter(monitorObject);
                try
                {
                    sharedResourceMonitor++;
                }
                finally
                {
                    // Release the lock using Monitor.Exit in a finally block
                    Monitor.Exit(monitorObject);
                }
                //sharedResourceMonitor++;
            }
        }

        #endregion

        #region MutexTechnique

        static Mutex mutex = new Mutex(false, "FileMutex");

        static void MutexTechnique()
        {
            // Wait for Process A to release the mutex
            mutex.WaitOne();

            try
            {
                // Read from the shared file
                using (StreamReader reader = new StreamReader("..\\..\\..\\shared.txt"))
                {
                    string content = reader.ReadToEnd();
                    Console.WriteLine("Content of the shared file:");
                    Console.WriteLine(content);
                }
            }
            finally
            {
                // Release the mutex
                mutex.ReleaseMutex();
            }

            Console.WriteLine("MutexTechnique has read from the file.");
            Console.ReadLine();
        }

        #endregion

        #region SemaphoreTechnique

        static Semaphore semaphore = new Semaphore(2, 2); // Set the initial count and maximum count of the semaphore

        static void SemaphoreTechnique()
        {
            // Create four threads
            Thread thread1 = new Thread(PerformTask);
            Thread thread2 = new Thread(PerformTask);
            Thread thread3 = new Thread(PerformTask);
            Thread thread4 = new Thread(PerformTask);
            Thread thread5 = new Thread(PerformTask);
            Thread thread6 = new Thread(PerformTask);
            Thread thread7 = new Thread(PerformTask);
            Thread thread8 = new Thread(PerformTask);
            Thread thread9 = new Thread(PerformTask);

            // Start the threads
            thread1.Start("Thread 1");
            thread2.Start("Thread 2");
            thread3.Start("Thread 3");
            thread4.Start("Thread 4");
            thread5.Start("Thread 5");
            thread6.Start("Thread 6");
            thread7.Start("Thread 7");
            thread8.Start("Thread 8");
            thread9.Start("Thread 9");

            // Wait for the threads to complete
            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();
            thread5.Join();
            thread6.Join();
            thread7.Join();
            thread8.Join();
            thread9.Join();

            Console.WriteLine("All threads completed their tasks.");
            Console.ReadLine();
        }

        static void PerformTask(object threadName)
        {
            Console.WriteLine($"{threadName} is waiting .........");

            semaphore.WaitOne(); // Acquire the semaphore

            Console.WriteLine($"{threadName} has acquired #########");

            // Simulate some work
            Thread.Sleep(5000);

            Console.WriteLine($"{threadName} has completed =========");

            semaphore.Release(); // Release the semaphore
        }

        #endregion

        #endregion

        #region ThreadPooling

        static void ThreadPooling()
        {
            // Queue work items to the thread pool
            ThreadPool.QueueUserWorkItem(SomeWork, "Task 1");
            ThreadPool.QueueUserWorkItem(SomeWork, "Task 2");
            ThreadPool.QueueUserWorkItem(SomeWork, "Task 3");
            ThreadPool.QueueUserWorkItem(SomeWork, "Task 4");

            Console.WriteLine("Tasks have been queued to the thread pool.");
            Console.ReadLine();
        }

        static void SomeWork(object taskName)
        {
            Console.WriteLine($"Task '{taskName}' is executing on thread {Thread.CurrentThread.ManagedThreadId}");

            // Simulate some work
            Thread.Sleep(2000);

            Console.WriteLine($"Task '{taskName}' has completed.");
        }

        #endregion

        #region Asynchronous

        public static async Task AsynchronousMethod()
        {
            // Create an instance of HttpClient to make an asynchronous HTTP request
            using (var client = new HttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://api-football-beta.p.rapidapi.com/teams?season=2019&league=39"),
                        Headers =
                        {
                            { "X-RapidAPI-Key", "4e81fa4cb7mshe04e99818795419p1878cajsnf874427fa066" },
                            { "X-RapidAPI-Host", "api-football-beta.p.rapidapi.com" },
                        },
                    };
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var body = await response.Content.ReadAsStringAsync();
                        var jsonBody = JObject.Parse(body);
                        StringBuilder b = new StringBuilder();
                        for (int i = 0; i < 20; i++)
                        {
                            b.AppendLine(
                                $"TeamName: {jsonBody["response"][i]["team"]["name"]} and TeamStadium: {jsonBody["response"][i]["venue"]["name"]}");
                        }

                        string formattedBody = b.ToString();
                        Console.WriteLine(formattedBody);
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occurred during the asynchronous operations
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            Console.WriteLine("Program execution completed.");
        }

        #endregion

        #region Tpl

        static void Tpl()
        {
            // Example 1: Creation, execution, waiting and completion of threads using Tpl
            ThreadsUsingTpl();
            Console.WriteLine("All tasks have completed.");

            // Example 2: Running parallel tasks
            Parallel.Invoke(
                () => Console.WriteLine("Task 1 executed."),
                () => Console.WriteLine("Task 2 executed."),
                () => Console.WriteLine("Task 3 executed.")
            );

            // Example 3: Parallel For loop
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine($"For loop iteration {i} executed by thread {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(100); // Simulating work
            });

            // Example 4: Asynchronous Task
            Task<int> asyncTask = Task.Run(() =>
            {
                Console.WriteLine("Asynchronous task started.");
                Thread.Sleep(2000); // Simulating work
                Console.WriteLine("Asynchronous task completed.");
                return 42;
            });

            // Do other work while the asynchronous task is running

            int result = asyncTask.Result; // Blocking call to get the task result
            Console.WriteLine($"Asynchronous task result: {result}");

            // Example 5: Parallel.ForEach loop
            int[] numbers = { 10, 20, 30, 40, 50 };
            Parallel.ForEach(numbers, number =>
            {
                Console.WriteLine($"Parallel.ForEach: Processing number {number} on thread {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(100); // Simulating work
            });
        }

        private static void ThreadsUsingTpl()
        {
            // Create and start a new task
            Task task1 = Task.Run(() =>
            {
                Console.WriteLine("Task 1 is running.");
                Thread.Sleep(2000);
                Console.WriteLine("Task 1 has completed.");
            });

            // Create and start another task
            Task task2 = Task.Run(() =>
            {
                Console.WriteLine("Task 2 is running.");
                Thread.Sleep(1000);
                Console.WriteLine("Task 2 has completed.");
            });

            // Wait for both tasks to complete
            Task.WaitAll(task1, task2);

            // Continue with another task after the previous tasks have completed
            Task task3 = Task.Run(() =>
            {
                Console.WriteLine("Task 3 is running.");
                Thread.Sleep(1500);
                Console.WriteLine("Task 3 has completed.");
            });

            // Wait for the third task to complete
            task3.Wait();
        }

        #endregion
    }
}
