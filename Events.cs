using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GrowtopiaSpoofer
{
    class Events
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, long lpAddress,
      long dwSize, uint flAllocationType, uint flProtect);


        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr Handle, long Address, byte[] buffer, int Size, int BytesWritten = 0);
        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr Handle, long Address, Int32 buffer, int Size, int BytesWritten = 0);
        [DllImport("kernel32.dll")]
        public static extern int ReadProcessMemory(IntPtr Handle, long Address, byte[] buffer, int Size, int BytesRead = 0);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualProtectEx(IntPtr hProcess, long lpAddress,
          int dwSize, uint flNewProtect, out uint lpflOldProtect);
        static long client = 0;

        public static string version = "";
        public const  int
       PAGE_READWRITE = 0x40,
    PROCESS_VM_OPERATION = 0x0008,
    PROCESS_VM_READ = 0x0010,
    PROCESS_VM_WRITE = 0x0020;

        static void Patch(long Address, byte[] enable, byte[] Disable)
        {

            Process gt = Process.GetProcessesByName("Growtopia")[0];
            IntPtr handler = OpenProcess(0x0FFFFF, true, gt.Id);
            var baseadd = gt.MainModule.BaseAddress.ToInt64();
            client = baseadd;
            uint kek;
        
            VirtualProtectEx(handler, client + 0x0, gt.MainModule.ModuleMemorySize, PAGE_READWRITE,
             out kek);
            VirtualAllocEx(handler, client + Address,enable.Length,0x40, 0x40);

           WriteProcessMemory(handler, client + Address, enable, enable.Length, 0);
        }
        public static void GetGameVersion()
        {

            Process gt = Process.GetProcessesByName("Growtopia")[0];
            IntPtr handler = OpenProcess(0x001F0FFF, true, gt.Id);
            var baseadd = gt.MainModule.BaseAddress.ToInt64();
            client = baseadd;
            byte[] game_version = new byte[50];
            uint kek;
            VirtualProtectEx(handler, client + 0x44A240, 50, PAGE_READWRITE,
           out kek);
            ReadProcessMemory(handler, 0x44A242, game_version, 50);
            float gttt = System.BitConverter.ToSingle(game_version, 0);
            Console.WriteLine("[CLIENT] game_version|" + gttt);
            version = gttt.ToString();



        }
        public static void Getfz()
        {

            Process gt = Process.GetProcessesByName("Growtopia")[0];
            IntPtr handler = OpenProcess(0x001F0FFF, true, gt.Id);
            var baseadd = gt.MainModule.BaseAddress.ToInt64();
            client = baseadd;
            byte[] fz = new byte[50];
            ReadProcessMemory(handler, 0x7FF7CA9A16CF, fz, 50);

            Console.WriteLine("[CLIENT] fz|" + BitConverter.ToInt32(fz, 0));




        }
        public static void Getrid()
        {

            Process gt = Process.GetProcessesByName("Growtopia")[0];
            IntPtr handler = OpenProcess(0x001F0FFF, true, gt.Id);
            var baseadd = gt.MainModule.BaseAddress.ToInt64();
            client = baseadd;
            byte[] rid = new byte[50];
            ReadProcessMemory(handler, 0x7FF7CA9A1468, rid, 50);

            Console.WriteLine("[CLIENT] rid|" + BitConverter.ToInt64(rid, 0));




        }
        public static void GetConsoleMessages()
        {

            Process gt = Process.GetProcessesByName("Growtopia")[0];
            IntPtr handler = OpenProcess(0x001F0FFF, true, gt.Id);
            var baseadd = gt.MainModule.BaseAddress.ToInt64();
            client = baseadd;
            byte[] ocm = new byte[50];
            ReadProcessMemory(handler, 0x26D068C5A28, ocm, 100);

            Console.WriteLine("[CLIENT] OnConsoleMessage|" + ASCIIEncoding.ASCII.GetString(ocm));




        }

        public static string oldversion = "";
        public static void GetLocalVersion()
        {
            //to be implemented!
            Console.WriteLine("[GrowtopiaSpoofer] Spoofing user info...");
            Console.WriteLine("[CLIENT] cbits|");
            Console.WriteLine("[CLIENT] uid|");
            Console.WriteLine("[CLIENT] netid|");
            if (System.IO.File.ReadAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Growtopia\Growtopia.exe").Contains("3.02"))
            {
                oldversion = "3,02";
                Patch(0x1DF09F, new byte[] { 0x74 }, new byte[] { 0x74 });

                Patch(0x44A240, new byte[] { 0x50, 0xFA, 0x54, 0x40, 0x4B, 0x6A, 0xD0, 0x41, 0x00, 0x00, 0x70, 0x42, 0x5D, 0x5C, 0x7E, 0x42, 0x4E, 0xA2, 0x81, 0x42, 0xF7, 0x24, 0x85, 0x42, 0xCD, 0x3B, 0x8A, 0x42, 0x49, 0x0E, 0x8F, 0x42, 0x00, 0x00, 0x90, 0x42, 0x42, 0x60, 0xE3,0x42, 0x27, 0xB1, 0xFB, 0x42, 0xE5, 0xB0, 0x01, 0x43, 0xBE, 0x7F }, new byte[] { 0xF3, 0x0F, 0x10, 0x0D, 0x18, 0xDD, 0x3A, 0x00 });
                GetGameVersion();
                Getfz();
                Getrid();


            }
            if (System.IO.File.ReadAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Growtopia\Growtopia.exe").Contains("3.022"))
            {
                Patch(0x1E005E, new byte[] { 0x74 }, new byte[] { 0x74 });
                
                //3.022 >> 3.32 Long term Spoof :)
                
                Patch(0x44B9A0, new byte[] { 0xE1, 0x7A, 0x54, 0x40, 0x2C, 0x65, 0x57, 0x41, 0x1C, 0x7C, 0x9A, 0x41, 0x96, 0xA1, 0x9A, 0x41, 0xD3, 0xCD, 0xDC, 0x41, 0x6B, 0xDA, 0x0F, 0x42, 0xCA, 0x54, 0x12, 0x42, 0x00, 0x00, 0x34, 0x42, 0xF0, 0x67, 0x36, 0x42, 0x01, 0x5E, 0x58, 0x42, 0x00, 0x00, 0x70, 0x42, 0x00, 0x00, 0x90, 0x42, 0x6D, 0x65 }, new byte[] { 0xF3, 0x0F, 0x10, 0x0D, 0x18, 0xDD, 0x3A, 0x00 });
            

                Console.WriteLine("[GrowtopiaSpoofer] GT Version detected 3.022");
            }
            if (Events.version == Events.oldversion)
            {
                Console.WriteLine("[GrowtopiaSpoofer] sad to tell but failed on spoofing the version :(");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("[GrowtopiaSpoofer] event|OnSendToServer");
                Console.ForegroundColor = ConsoleColor.Gray;
                wait_commands = true;
         
            }
        

        }
        public static bool wait_commands = false;
        public static bool wait_rO = false;
        public static void waitRealCommand()
        {
            Process gt = Process.GetProcessesByName("Growtopia")[0];
            IntPtr handler = OpenProcess(0x0FFFFF, true, gt.Id);
            var baseadd = gt.MainModule.BaseAddress.ToInt64();
            while (wait_rO == true)
            {
                client = baseadd;
                byte[] ocm = new byte[20];

                ReadProcessMemory(handler, 0x5447C8, ocm, 20);

                if (ASCIIEncoding.ASCII.GetString(ocm).Contains("/goblins"))
                {
                    WriteProcessMemory(handler, client + 0x2ADC41, new byte[] { 0x74 }, 1);
                }
                if (ASCIIEncoding.ASCII.GetString(ocm).Contains("/nick"))
                {
                    String name = ASCIIEncoding.ASCII.GetString(ocm).Replace("/nick","");

                    WriteProcessMemory(handler, client + 0x46D828, ASCIIEncoding.ASCII.GetBytes(name), ASCIIEncoding.ASCII.GetBytes(name).Length);
                    WriteProcessMemory(handler, client + 0x2ADC41, new byte[] { 0x74 }, 1);
                 
                    }
                if (ASCIIEncoding.ASCII.GetString(ocm).Contains("/set"))
                {
                    WriteProcessMemory(handler, client + 0x2ADC41, new byte[] { 0x74 }, 1);
                    Console.WriteLine("[GrowtopiaSpoofer] your nickname has been changed!");
                    wait_rO = false;
                    wait_commands = true;
                    GetCommands();
                }
            }

        }
        public static void GetCommands()
        {
            
            Process gt = Process.GetProcessesByName("Growtopia")[0];
            IntPtr handler = OpenProcess(0x0FFFFF, true, gt.Id);
            var baseadd = gt.MainModule.BaseAddress.ToInt64();
            while (wait_commands == true)
            {
            
                client = baseadd;
                byte[] ocm = new byte[20];
   
                ReadProcessMemory(handler, 0x5447C8, ocm, 20);
               
                if (ASCIIEncoding.ASCII.GetString(ocm).Contains("/spoofer"))
                {
                    Console.WriteLine("/spoofer commands: /legens /goblins");
                    wait_commands = false;
                    wait_rO = true;
                    waitRealCommand();

                }
             
            }
               
             
            }
          


        }
    }

