using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace ConsoleIntercepting
{
    public delegate void LogMessage(string Data);

    public class Runner
    {
        public string ProcessPath { get; set; }

        public event LogMessage LogSend;
        
        private ProcessStartInfo Info = null;
        private Process Proc = null;
        private Thread t = null;

        public Runner(string processpath)
        {
            ProcessPath = processpath; //устанавливаем значение свойства ProcessPath
            Info = new ProcessStartInfo(ProcessPath);
            Info.RedirectStandardError = true; //перехват STDERR
            Info.RedirectStandardOutput = true; //перехват STDOUT
            Info.UseShellExecute = false; //иначе перехват работать не будет, см. MSDN
            Info.CreateNoWindow = true; //не запускать процесс в новом окне
                                        //это скроет консоль запускаемой программы
        }

        private void StartProcess()
        {
            try
            {
                Proc = Process.Start(Info);
            }
            catch (Exception ex)
            {
                LogSend("INTERNAL ERROR: "+ex.Message);
                return;
            }

            string ConOut = "";

            do
            {
                ConOut = Proc.StandardOutput.ReadLine();
                if (ConOut != null)
                {
                    LogSend(ConOut);
                }                

            } while (ConOut != null);
        }

        public void Start()
        {
            t = new Thread(StartProcess);
            t.Start();
        }

        public void Stop()
        {
            if (Proc != null)
            {
                Proc.Kill();
                Proc = null;
            }

            if (t != null)
            {
                t.Abort();
                t = null;
            }
        }
    }
}
